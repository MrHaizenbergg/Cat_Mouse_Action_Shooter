using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
class PlayerData
{
    public int unlockSeasons;
    public int currentCatSkin;
    public int money;
    public int levelHighest;
    public int levelPlaying;
    public bool[] catsUnlocked;
    public int[] levelAchievedStars;
    public int[] Stars;
    public int[] AllStars;
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    //[DllImport("__Internal")]
    //private static extern void SaveExtern(string data);

    //[DllImport("__Internal")]
    //private static extern void LoadExtern();

    public static int levelPlaying = -1;
    public static int levelHighest
    {
        get { return PlayerPrefs.GetInt("LevelHighest", 1); }
        set { PlayerPrefs.SetInt("LevelHighest", value); }
    }

    public int unlockSeasons;
    public int currentCatSkin;
    public int money;
    public bool[] catsUnlocked = new bool[4] { true, false, false, false };
    public int[] levelAchievedStars = new int[21];
    public int[] Stars = new int[21];
    public int[] AllStars = new int[21];

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
            Destroy(gameObject);


        //levelHighest = 1;
        //levelPlaying = -1;

        Load();
    }

    //private void Update()
    //{
    //    if (load && Input.anyKey)
    //    {
    //        LoadExtern();
    //        HomeMenu.Instance.ShowUIAfterLoad();
    //        load= false;
    //        Debug.Log("LoadExternUpdate");
    //    }
    //}


    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)binaryFormatter.Deserialize(file);

            if (data != null)
            {
                unlockSeasons = data.unlockSeasons;
                levelHighest = data.levelHighest;
                levelPlaying = data.levelPlaying;
                money = data.money;
                currentCatSkin = data.currentCatSkin;
                catsUnlocked = data.catsUnlocked;
                levelAchievedStars = data.levelAchievedStars;
                Stars = data.Stars;
                AllStars = data.AllStars;

                if (data.catsUnlocked == null)
                    catsUnlocked = new bool[4] { true, false, false, false };
                if (data.levelAchievedStars == null)
                    levelAchievedStars = new int[21];
                if (data.Stars == null)
                    Stars = new int[21];
                if (data.AllStars == null)
                    AllStars = new int[21];
                if (data.levelHighest == 0)
                    levelHighest = 1;
                if (data.levelPlaying == 0)
                    levelPlaying = -1;
            }

            Debug.Log(data.money);
            Debug.Log(data.currentCatSkin);
            Debug.Log(data.catsUnlocked);
            Debug.Log(data.levelHighest + "LevelHig");
            Debug.Log(data.levelPlaying + "LevelPlaying");
            Debug.Log(data.levelAchievedStars);
            Debug.Log(data.Stars);
            Debug.Log("Load");

            file.Close();
        }
    }

    public void ClearData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
    
        unlockSeasons = 0;
        levelHighest = 1;
        levelPlaying = -1;
        currentCatSkin = 0;
        money = 0;
        catsUnlocked = new bool[4] { true, false, false, false };
        levelAchievedStars = new int[21];
        Stars = new int[21];
        AllStars = new int[21];

        data.unlockSeasons = 0;
        data.levelHighest = 1;
        data.levelPlaying = -1;
        data.currentCatSkin = 0;
        data.money = 0;
        data.catsUnlocked = new bool[4] { true, false, false, false };
        data.levelAchievedStars = new int[21];
        data.Stars = new int[21];
        data.AllStars = new int[21];

        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        Debug.Log("Save");
        Debug.Log(money);
        Debug.Log(currentCatSkin);
        Debug.Log(catsUnlocked);
        Debug.Log(levelHighest);
        Debug.Log(levelPlaying);
        Debug.Log(levelAchievedStars);
        Debug.Log(Stars);

        data.unlockSeasons = unlockSeasons;
        data.money = money;
        data.currentCatSkin = currentCatSkin;
        data.catsUnlocked = catsUnlocked;
        data.levelHighest = levelHighest;
        data.levelPlaying = levelPlaying;
        data.levelAchievedStars = levelAchievedStars;
        data.Stars = Stars;
        data.AllStars = AllStars;
        
        binaryFormatter.Serialize(file, data);
        file.Close();
    }
}