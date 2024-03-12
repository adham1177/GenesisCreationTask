using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Shopkeeper
{
    [SerializeField] private string name;
    [SerializeField] private List<Item> items;

    public string Name => name;
    public List<Item> Items => items;

    public Shopkeeper(List<Item> items)
    {
        this.items = items;
    }
}
