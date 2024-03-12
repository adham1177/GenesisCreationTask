using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private ItemView itemPrefab;
    [SerializeField] private Transform content;

    private readonly List<ItemView> _itemViews = new();
    public void UpdateInventoryView(List<Item> items, EItemOperation itemOperation, Action<Item> operationAction)
    {
        var index = 0;
        for (; index < items.Count; index++)
        {
            var item = items[index];
            if (_itemViews.Count > index)
            {
                _itemViews[index].Init(item, operationAction, itemOperation);
                _itemViews[index].gameObject.SetActive(true);
            }
            else
            {
                var itemView = Instantiate(itemPrefab, content);
                itemView.Init(item, operationAction, itemOperation);
                _itemViews.Add(itemView);
            }
        }

        for (var i = index; i < _itemViews.Count; i++)
        {
            _itemViews[i].gameObject.SetActive(false);
        }
    }
    
}
