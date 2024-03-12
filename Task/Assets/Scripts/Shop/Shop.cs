using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("ShopkeepersData")] 
    [SerializeField] private List<Shopkeeper> shopkeepers;

    [Header("ShopPanel")] 
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI shopkeeperName;
    [SerializeField] private InventoryView shopkeeperInventoryView;
    [SerializeField] private InventoryView playerInventoryView;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject backgroundDim;


    private int _selectedIndex;

    public static Func<Item, bool> TryBuyItem;
    public static Func<Item, bool> TrySellItem;

    private void Awake()
    {
        exitButton.onClick.AddListener(CloseShopInventory);
    }

    private void OnEnable()
    {
        PlayerController.PlayerInventoryUpdated += OnPlayerInventoryUpdated;
    }

    private void OnDisable()
    {
        PlayerController.PlayerInventoryUpdated -= OnPlayerInventoryUpdated;
    }

    private void OnPlayerInventoryUpdated(List<Item> items)
    {
        playerInventoryView.UpdateInventoryView(items, EItemOperation.Sell, SellItem);
    }

    public void OpenShopInventory(int shopKeeperIndex)
    {
        _selectedIndex = shopKeeperIndex;
        shopkeeperName.text = shopkeepers[_selectedIndex].Name;
        shopkeeperInventoryView.UpdateInventoryView(shopkeepers[_selectedIndex].Items, EItemOperation.Buy, BuyItem);
        shopPanel.gameObject.SetActive(true);
        backgroundDim.gameObject.SetActive(true);
    }

    private void CloseShopInventory()
    {
        shopPanel.gameObject.SetActive(false);
        backgroundDim.gameObject.SetActive(false);
    }


    private void BuyItem(Item item)
    {
        var res = TryBuyItem.Invoke(item);
        if (!res)
        {
            PopupManager.Instance.AddToQueue("Not Enough Coins");
            return;
        }
        shopkeepers[_selectedIndex].Items.Remove(item);
        shopkeeperInventoryView.UpdateInventoryView(shopkeepers[_selectedIndex].Items, EItemOperation.Buy, BuyItem);
    }

    private void SellItem(Item item)
    {
        var res = TrySellItem.Invoke(item);
        if (!res)
        {
            PopupManager.Instance.AddToQueue("Player Don't have this item");
            return;
        }
        shopkeepers[_selectedIndex].Items.Add(item);
        shopkeeperInventoryView.UpdateInventoryView(shopkeepers[_selectedIndex].Items, EItemOperation.Buy, BuyItem);
    }
}