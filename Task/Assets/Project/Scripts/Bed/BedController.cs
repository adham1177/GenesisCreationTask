using TMPro;
using UnityEngine;

public class BedController : MonoBehaviour
{
    [Header("BedPanel")]
    [SerializeField] private GameObject bedPanel;
    [SerializeField] private GameObject backgroundDim;

    private float _timerValue;
    

    public void OpenBedPanel()
    {
        
    }

    private void CloseBedPanel()
    {
        backgroundDim.SetActive(false);
        bedPanel.SetActive(false);
    }
    
    
}
