using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private Shop shop;
    [SerializeField] private BedController bedController;
    [SerializeField] private AtmController atmController;

    [Header("Buttons")] 
    [SerializeField] private Button atmButton;
    [SerializeField] private Button bedButton;


    public static event Action PlayerSleep; 

    private void Awake()
    {
        atmButton.onClick.AddListener(OpenAtm);
        bedButton.onClick.AddListener(IncreaseBalance);
    }

    private void OnEnable()
    {
        ShopkeeperButton.OpenShop += OnOpenShop;
    }

    private void OnDisable()
    {
        ShopkeeperButton.OpenShop -= OnOpenShop;
    }

    private void OnOpenShop(int shopkeeperIndex)
    {
        atmController.CloseAtmPanel();
        shop.OpenShopInventory(shopkeeperIndex);
    }
    
    private void OpenAtm()
    {
        atmController.OpenAtmPanel();
    }

    private void IncreaseBalance()
    {
        PopupManager.Instance.AddToQueue("Balance Increased");
        PlayerSleep?.Invoke();
    }
}
