using UnityEngine;
using YG;

public class MoneyAdd : MonoBehaviour
{
    [SerializeField] GameObject AdvRewardedBtn;

    public void AddCoins(int value)
    {
        SaveManager.instance.money += value;
        SaveManager.instance.Save();
    }
}