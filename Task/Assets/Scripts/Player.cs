using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    private int _playerCash;

    public int PlayerCash => _playerCash;

    public Player(int playerCash)
    {
        _playerCash = playerCash;
    }

    public void BuyItem(int price)
    {
        _playerCash -= price;
        Debug.Log($"Player Cash Decremented {_playerCash}");
    }
}
