using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI coins;

   private void OnEnable()
   {
      PlayerController.CoinsUpdated += OnCoinsUpdated;
   }

   private void OnCoinsUpdated(int newBalance)
   {
      coins.text = newBalance.ToString();
   }
}
