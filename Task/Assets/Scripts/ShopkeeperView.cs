using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperView : MonoBehaviour
{
    [SerializeField] private ItemView itemPrefab;

    public event Action<Item> TryBuyItem;

    private readonly List<ItemView> _itemViews = new();
    public void UpdateShopkeeperView(List<Item> items)
    {
        var index = 0;
        for (; index < items.Count; index++)
        {
            var item = items[index];
            if (_itemViews.Count > index)
            {
                _itemViews[index].Init(item, ButtonAction);
                _itemViews[index].gameObject.SetActive(true);
            }
            else
            {
                var itemView = Instantiate(itemPrefab, transform);
                itemView.Init(item, ButtonAction);
                _itemViews.Add(itemView);
            }
        }

        for (var i = index; i < _itemViews.Count; i++)
        {
            _itemViews[i].gameObject.SetActive(false);
        }
    }

    private void ButtonAction(Item item)
    {
        TryBuyItem?.Invoke(item);
    }
}
