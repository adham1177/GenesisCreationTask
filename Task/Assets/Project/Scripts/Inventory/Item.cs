using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Item
{
    [SerializeField] private string name;
    private Sprite sprite;
    [SerializeField] private int buyPrice;
    [SerializeField] private int sellPrice;

    public string Name => name;

    public Sprite Sprite => sprite;

    public int BuyPrice => buyPrice;

    public int SellPrice => sellPrice;

    public Item(string name, Sprite sprite, int buyPrice, int sellPrice)
    {
        this.name = name;
        this.sprite = sprite;
        this.buyPrice = buyPrice;
        this.sellPrice = sellPrice;
    }
}