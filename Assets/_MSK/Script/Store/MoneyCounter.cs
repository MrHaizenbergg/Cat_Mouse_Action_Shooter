using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    private Text textMoney;

    private void Awake()
    {
        textMoney = GetComponent<Text>();
    }

    private void Update()
    {
        textMoney.text = SaveManager.instance.money + "$";
    }
}