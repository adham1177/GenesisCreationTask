using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Shopkeeper: MonoBehaviour
{
    private readonly List<Item> _items = new()
    {
        new Item("item1", null, 100, 0),
        new Item("item2", null, 50, 0),
        new Item("item3", null, 200, 0)
    };

    [SerializeField] private ShopkeeperView shopkeeperView;

    public static Func<Item, bool> TryBuyItem;

    private void OnEnable()
    {
        shopkeeperView.TryBuyItem += OnTryBuyItem;
    }

    private void OnDisable()
    {
        shopkeeperView.TryBuyItem -= OnTryBuyItem;
    }

    private void Start()
    {
        shopkeeperView.UpdateShopkeeperView(_items);
    }
    
    private void OnTryBuyItem(Item item)
    {
        var res =  TryBuyItem.Invoke(item);
        if (!res) return;
        _items.Remove(item);
        shopkeeperView.UpdateShopkeeperView(_items);
    }
    
}
