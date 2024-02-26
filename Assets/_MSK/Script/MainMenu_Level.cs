using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_Level : MonoBehaviour
{
    int levelNumber = 1;
    public Text TextLevel;
    public GameObject Locked;
    public GameObject[] starsInactive;
    public GameObject[] starsActive;
    private int AllBullets;
    private int Wasted_Bullets;

    public GameObject backgroundNormal, backgroundInActive;

    void Start()
    {
        levelNumber = int.Parse(gameObject.name);

        foreach (var item in starsInactive)
        {
            item.gameObject.SetActive(false);
        }
        backgroundNormal.SetActive(true);
        backgroundInActive.SetActive(false);

        //var levelReached = GlobalValue.LevelHighest;
        var levelReached = SaveManager.levelHighest;


        if ((levelNumber <= levelReached))
        {
            TextLevel.text = levelNumber.ToString();
            Locked.SetActive(false);

            StarsAchieved(levelNumber);

            var openLevel = levelReached + 1 >= levelNumber /*int.Parse(gameObject.name)*/;

            Locked.SetActive(!openLevel);

            bool isInActive = levelNumber == levelReached;

            backgroundNormal.SetActive(!isInActive);
            backgroundInActive.SetActive(isInActive);

            GetComponent<Button>().interactable = openLevel;
        }
        else
        {
            TextLevel.gameObject.SetActive(false);
            Locked.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
    }

    private void StarsAchieved(int Level)
    {
        if (levelNumber == Level)
        {
            if (SaveManager.instance.levelAchievedStars[Level] >= 5)
            {
                starsActive[0].SetActive(true);
                starsActive[1].SetActive(true);
                starsActive[2].SetActive(true);
                Debug.Log(SaveManager.instance.levelAchievedStars[Level]+ "Level 3 Stars");
            }
            else if (SaveManager.instance.levelAchievedStars[Level] >= 3)
            {
                starsActive[0].SetActive(true);
                starsActive[1].SetActive(true);
                Debug.Log(SaveManager.instance.levelAchievedStars[Level] + "Level 2 Stars");
            }
            else if (SaveManager.instance.Stars[Level] == 1)
            {
                starsActive[0].SetActive(true);
                Debug.Log(SaveManager.instance.levelAchievedStars[Level] + "Level 1 Stars");
            }
        }
    }

    //private void StarsAchieved(int Level)
    //{
    //    if (levelNumber == Level)
    //    {
    //        if (PlayerPrefs.GetInt("SavLevel"+ Level, 0) >= 5)
    //        {
    //            starsActive[0].SetActive(true);
    //            starsActive[1].SetActive(true);
    //            starsActive[2].SetActive(true);
    //            Debug.Log(PlayerPrefs.GetInt("SavLevel"+Level, 0) + " 3 Stars");
    //        }
    //        else if (PlayerPrefs.GetInt("SavLevel"+Level, 0) >= 3)
    //        {
    //            starsActive[0].SetActive(true);
    //            starsActive[1].SetActive(true);
    //            Debug.Log(PlayerPrefs.GetInt("SavLevel"+Level, 0)+ " 2 Stars");
    //        }
    //        else if(PlayerPrefs.HasKey("SavLevel"+Level))
    //            starsActive[0].SetActive(true);
    //    }
    //}

    public void LoadScene()
    {
        //GlobalValue.levelPlaying = levelNumber;
        SaveManager.levelPlaying = levelNumber;
        SoundManager.Click();
        HomeMenu.Instance.LoadLevel();
    }
}