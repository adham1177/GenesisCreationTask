using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI coins;

   private void OnEnable()
   {
      PlayerController.CoinsUpdated += OnCoinsUpdated;
   }

   private void OnCoinsUpdated(float newBalance)
   {
      coins.text = newBalance.ToString("F1");
   }
}
