using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;

    private int money;

    private void Start()
    {
        money = FindObjectOfType<GameManager>().money;

        moneyText.text = "Money: " + money.ToString();
    }
}
