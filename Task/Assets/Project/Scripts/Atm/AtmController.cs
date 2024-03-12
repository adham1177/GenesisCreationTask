using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AtmController : MonoBehaviour
{

    [Header("ArmPanel")]
    [SerializeField] private GameObject atmPanel;
    [SerializeField] private GameObject backgroundDim;

    [SerializeField] private Button exitButton;
    
    [Header("Balance")] 
    [SerializeField] private TextMeshProUGUI playerBalance;

    [Header("Feedback")] 
    [SerializeField] private TextMeshProUGUI feedBackMessage;
    
    [Header("Withdraw")] 
    [SerializeField] private Button withdrawButton;
    [SerializeField] private TMP_InputField withdrawAmount;
    
    [Header("Deposit")] 
    [SerializeField] private Button depositButton;
    [SerializeField] private TMP_InputField depositAmount;


    public static Func<int, bool> TryWithdraw; 
    public static Func<int, bool> TryDeposit;
    private void Awake()
    {
        withdrawAmount.onValueChanged.AddListener((text) =>
        {
            CheckAmount(withdrawButton, text);
        });
        
        withdrawButton.onClick.AddListener(() =>
        {
            Withdraw(Convert.ToInt32(withdrawAmount.text));
        });
        
        depositAmount.onValueChanged.AddListener((text) =>
        {
            CheckAmount(depositButton, text);
        });
        
        depositButton.onClick.AddListener(() =>
        {
            Deposit(Convert.ToInt32(depositAmount.text));
        });
        
        exitButton.onClick.AddListener(CloseAtmPanel);
    }

    private void OnEnable()
    {
        PlayerController.AtmBalanceUpdated += OnAtmBalanceUpdated;
        withdrawAmount.text = "";
        depositAmount.text = "";
        feedBackMessage.text = "";
        CheckAmount(withdrawButton, withdrawAmount.text);
        CheckAmount(depositButton, depositAmount.text);
    }

    private void OnDisable()
    {
        PlayerController.AtmBalanceUpdated -= OnAtmBalanceUpdated;
    }


    private void CheckAmount(Selectable button, string amount)
    {
        button.interactable = amount.Length > 0;
    }
    
    private void OnAtmBalanceUpdated(float amount)
    {
        playerBalance.text = amount.ToString("F1");
    }

    private void Withdraw(int amount)
    {
        var res = TryWithdraw.Invoke(amount);
        if (res)
        {
            feedBackMessage.text = "Operation Successful";
            feedBackMessage.color = Color.green;
        }
        else
        {
            feedBackMessage.text = "Insufficient balance";
            feedBackMessage.color = Color.red;
        }

        withdrawAmount.text = "";
    }

    private void Deposit(int amount)
    {
        var res = TryDeposit.Invoke(amount);
        if (res)
        {
            feedBackMessage.text = "Operation Successful";
            feedBackMessage.color = Color.green;
        }
        else
        {
            feedBackMessage.text = "Not enough coins";
            feedBackMessage.color = Color.red;
        }

        depositAmount.text = "";
    }

    public void OpenAtmPanel()
    {
        atmPanel.SetActive(true);
        backgroundDim.SetActive(true);
        atmPanel.transform.DOScale(Vector3.one, 0.3f).From(Vector3.zero).SetEase(Ease.OutBack);
    }

    public async void CloseAtmPanel()
    {
        await atmPanel.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).AsyncWaitForCompletion();
        atmPanel.SetActive(false);
        backgroundDim.SetActive(false);
    }
}
