using System;
using System.Collections.Generic;
using DG.Tweening;
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
    private List<Item> _playerCachedInventory = new();
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
        _playerCachedInventory = items;
        playerInventoryView.UpdateInventoryView(_playerCachedInventory, EItemOperation.Sell, SellItem);
    }

    public void OpenShopInventory(int shopKeeperIndex)
    {
        _selectedIndex = shopKeeperIndex;
        shopkeeperName.text = shopkeepers[_selectedIndex].Name;
        shopkeeperInventoryView.UpdateInventoryView(shopkeepers[_selectedIndex].Items, EItemOperation.Buy, BuyItem);
        playerInventoryView.UpdateInventoryView(_playerCachedInventory, EItemOperation.Sell, SellItem);
        shopPanel.gameObject.SetActive(true);
        backgroundDim.gameObject.SetActive(true);
        shopPanel.transform.DOScale(Vector3.one, 0.3f).From(Vector3.zero).SetEase(Ease.OutBack);
    }

    private async void CloseShopInventory()
    {
        await shopPanel.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).AsyncWaitForCompletion();
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