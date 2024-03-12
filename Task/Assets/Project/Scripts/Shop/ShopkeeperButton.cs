using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopkeeperButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private int shopkeeperIndex;

    public static event Action<int> OpenShop;

    private void Awake()
    {
        button.onClick.AddListener(()=>
        {
            OpenShop?.Invoke(shopkeeperIndex);
        });
    }
    
    
}
