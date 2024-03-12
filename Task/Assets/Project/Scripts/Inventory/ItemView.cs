using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void Init(Item item, Action<Item> buttonAction, EItemOperation itemOperation)
    {
        actionButton.onClick.RemoveAllListeners();
        itemName.text = item.Name;
        price.text = GetOperationPrice(item, itemOperation).ToString();
        buttonText.text = GetOperationButtonText(itemOperation);
        actionButton.onClick.AddListener(() =>
        {
            buttonAction?.Invoke(item);
        });
    }

    private int GetOperationPrice(Item item, EItemOperation itemOperation)
    {
        return itemOperation switch
        {
            EItemOperation.Buy => item.BuyPrice,
            EItemOperation.Sell => item.SellPrice,
            _ => throw new ArgumentOutOfRangeException(nameof(itemOperation), itemOperation, null)
        };
    }

    private string GetOperationButtonText(EItemOperation itemOperation)
    {
        switch (itemOperation)
        {
            case EItemOperation.Buy:
                return "Buy";
            case EItemOperation.Sell:
                return "Sell";
            default:
                throw new ArgumentOutOfRangeException(nameof(itemOperation), itemOperation, null);
        }
    }

    private void OnDisable()
    {
        actionButton.onClick.RemoveAllListeners();
    }
}

public enum EItemOperation
{
    Buy,
    Sell
}
