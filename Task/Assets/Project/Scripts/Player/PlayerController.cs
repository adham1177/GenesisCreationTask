using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;

    public static event Action<float> CoinsUpdated;
    public static event Action<float> AtmBalanceUpdated; 
    

    public static event Action<List<Item>> PlayerInventoryUpdated; 
    
    private void OnEnable()
    {
        Shop.TryBuyItem += OnTryBuyItem;
        Shop.TrySellItem += OnTrySellItem;
        AtmController.TryWithdraw += OnTryWithdraw;
        AtmController.TryDeposit += OnTryDeposit;
        UIManager.PlayerSleep += OnPlayerSleep;
    }

    private void OnDisable()
    {
        Shop.TryBuyItem -= OnTryBuyItem;
        Shop.TrySellItem -= OnTrySellItem;
        AtmController.TryWithdraw -= OnTryWithdraw;
        AtmController.TryDeposit -= OnTryDeposit;
        UIManager.PlayerSleep -= OnPlayerSleep;
    }

    private void Start()
    {
        _player = new Player(1000, 3000);
        CoinsUpdated?.Invoke(_player.Coins);
        AtmBalanceUpdated?.Invoke(_player.AtmBalance);
    }
    
    
    private bool OnTryBuyItem(Item item)
    {
        if (item.BuyPrice > _player.Coins) return false;
        _player.BuyItem(item.BuyPrice);
        _player.Items.Add(item);
        CoinsUpdated?.Invoke(_player.Coins);
        PlayerInventoryUpdated?.Invoke(_player.Items);
        return true;

    }
    private bool OnTrySellItem(Item item)
    {
        if (!_player.Items.Remove(item)) return false;
        _player.SellIem(item.SellPrice);
        CoinsUpdated?.Invoke(_player.Coins);
        PlayerInventoryUpdated?.Invoke(_player.Items);
        return true;
    }
    
    private bool OnTryWithdraw(int amount)
    {
        if (!_player.Withdraw(amount)) return false;
        
        CoinsUpdated?.Invoke(_player.Coins);
        AtmBalanceUpdated?.Invoke(_player.AtmBalance);
        
        return true;

    }

    private bool OnTryDeposit(int amount)
    {
        if (!_player.Deposit(amount)) return false;
        
        CoinsUpdated?.Invoke(_player.Coins);
        AtmBalanceUpdated?.Invoke(_player.AtmBalance);
        
        return true;
    }
    
    private void OnPlayerSleep()
    {
        _player.Sleep();
        AtmBalanceUpdated?.Invoke(_player.AtmBalance);
    }
    
    
}
