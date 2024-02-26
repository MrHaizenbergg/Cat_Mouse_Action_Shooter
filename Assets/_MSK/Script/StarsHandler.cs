using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarsHandler : MonoBehaviour
{
    public static StarsHandler instance;

    public Text endLevelText;

    public GameObject[] starsMenuInactive;
    public GameObject[] starsMenuActive;

    private int Wasted_bullets;
    private int starsForLevel;

    private void Awake()
    {
        instance = this;
    }

    public void SaveStarProgress(int Scene)
    {
        Wasted_bullets = PlayerGun.Instance.AllShotBullets;

        if (Wasted_bullets >= 5)
        {
            if (Language.Instance.currentLanguage == "en")
            {
                endLevelText.text = "Perfect!";
            }
            else if (Language.Instance.currentLanguage == "ru")
            {
                endLevelText.text = "Отлично!";
            }
            else
            {
                endLevelText.text = "Perfect!";
            }

            starsMenuActive[0].SetActive(true);
            starsMenuActive[1].SetActive(true);
            starsMenuActive[2].SetActive(true);
            starsForLevel = 3;
            Debug.Log(Wasted_bullets + " 3 Stars");
        }
        else if (Wasted_bullets >= 3)
        {
            if (Language.Instance.currentLanguage == "en")
            {
                endLevelText.text = "Well!";
            }
            else if (Language.Instance.currentLanguage == "ru")
            {
                endLevelText.text = "Хорошо!";
            }
            else
            {
                endLevelText.text = "Well!";
            }

            starsMenuActive[0].SetActive(true);
            starsMenuActive[1].SetActive(true);
            starsForLevel = 2;         
            Debug.Log(Wasted_bullets + " 2 Stars");
        }
        else if (Wasted_bullets >= 0)
        {

            if (Language.Instance.currentLanguage == "en")
            {
                endLevelText.text = "Level Passed!";
            }
            else if (Language.Instance.currentLanguage == "ru")
            {
                endLevelText.text = "Уровень Пройден!";
            }
            else
            {
                endLevelText.text = "Level Passed!";
            }

            starsMenuActive[0].SetActive(true);
            starsForLevel = 1;        
        }

        //PlayerPrefs.SetInt("SavLevel" + Scene, Wasted_bullets);
        //PlayerPrefs.SetInt("SavStars" + Scene, starsForLevel);

        SaveManager.instance.levelAchievedStars[Scene] = Wasted_bullets;
        SaveManager.instance.Stars[Scene] = starsForLevel;
        SaveManager.instance.AllStars[Scene] = starsForLevel;

        SaveManager.instance.Save();

        //PlayerPrefs.Save();
        Debug.Log("SavLevel" + Scene);

        Debug.Log(starsForLevel);
        Debug.Log(Wasted_bullets);
    }
}
