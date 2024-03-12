using System.Collections.Generic;

public class Player
{
    private float _coins;

    private float _atmBalance;


    private List<Item> _items;

    public float Coins => _coins;

    public float AtmBalance => _atmBalance;

    public List<Item> Items => _items;

    public Player(int coins, int atmBalance)
    {
        _coins = coins;
        _atmBalance = atmBalance;
        _items = new List<Item>();
    }

    public void BuyItem(int price)
    {
        _coins -= price;
    }

    public void SellIem(int price)
    {
        _coins += price;
    }

    public void Sleep()
    {
        _atmBalance += 0.1f * _atmBalance;
    }

    public bool Withdraw(int amount)
    {
        if (amount > _atmBalance) return false;

        _atmBalance -= amount;
        _coins += amount;

        return true;
    }

    public bool Deposit(int amount)
    {
        if (amount > _coins) return false;

        _coins -= amount;
        _atmBalance += amount;

        return true;
    }
}