using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private GameObject popupWindow;

    private readonly Queue<string> _popupQueue = new();

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
        CheckQueue();
    }

    private Task ShowPopup(string text)
    {
        popupText.text = text;
        popupWindow.SetActive(true);
        return Task.CompletedTask;
    } 

    private async void CheckQueue()
    {
        while (_popupQueue.Count > 0)
        {
            await ShowPopup(_popupQueue.Dequeue()); 
            await Task.Delay(1000);
        }
        popupWindow.SetActive(false);
    }
}
