using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> _items;

    public List<Item> Items => _items;

    public Inventory(List<Item> items)
    {
        _items = items;
    }
}
