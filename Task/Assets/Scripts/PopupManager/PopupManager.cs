using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private GameObject popupWindow;
    [SerializeField] private float animationDuration = 1;

    private readonly Queue<string> _popupQueue = new();
    private bool _isQueueWorking;

    public static PopupManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
        }
    }

    public void AddToQueue(string text)
    {
        _popupQueue.Enqueue(text);
        if (!_isQueueWorking)
            CheckQueue();
    }

    private async Task ShowPopup(string text)
    {
        popupText.text = text;
        popupWindow.SetActive(true);
        await popupWindow.transform.DOScale(Vector3.one, animationDuration).From(Vector3.zero).SetEase(Ease.OutBack).AsyncWaitForCompletion();
    } 

    private async void CheckQueue()
    {
        _isQueueWorking = true;
        while (_popupQueue.Count > 0)
        {
            await ShowPopup(_popupQueue.Dequeue()); 
        }
        popupWindow.SetActive(false);
        _isQueueWorking = false;
    }
}
