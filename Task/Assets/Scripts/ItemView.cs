using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI price;

    public void Init(Item item, Action<Item> buttonAction)
    {
        actionButton.onClick.RemoveAllListeners();
        itemName.text = item.Name;
        price.text = item.BuyPrice.ToString();
        actionButton.onClick.AddListener(() =>
        {
            buttonAction?.Invoke(item);
        });
    }

    private void OnDisable()
    {
        actionButton.onClick.RemoveAllListeners();
    }
}
