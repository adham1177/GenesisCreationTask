using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string _name;
    private Sprite _sprite;
    private int _buyPrice;
    private int _sellPrice;

    public string Name => _name;

    public Sprite Sprite => _sprite;

    public int BuyPrice => _buyPrice;

    public int SellPrice => _sellPrice;

    public Item(string name, Sprite sprite, int buyPrice, int sellPrice)
    {
        _name = name;
        _sprite = sprite;
        _buyPrice = buyPrice;
        _sellPrice = sellPrice;
    }
}
