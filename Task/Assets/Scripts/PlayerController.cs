using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;

    public static event Action<int> CoinsUpdated;
    private void OnEnable()
    {
        Shopkeeper.TryBuyItem += TryBuyItem;
    }

    private void OnDisable()
    {
        Shopkeeper.TryBuyItem -= TryBuyItem;
    }

    private void Start()
    {
        _player = new Player(1000);
        CoinsUpdated?.Invoke(_player.PlayerCash);
    }

    private bool TryBuyItem(Item item)
    {
        if (item.BuyPrice > _player.PlayerCash) return false;
        _player.BuyItem(item.BuyPrice);
        CoinsUpdated?.Invoke(_player.PlayerCash);
        return true;

    }
}
