using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class HomeMenu : MonoBehaviour
{
    public static HomeMenu Instance;
    public GameObject UI;
    public GameObject LevelUI;
    public GameObject LoadingUI;
    public GameObject SettingsUI;

    [Header("Sound and Music")]
    public Image soundImage;
    public Image musicImage;
    public Sprite soundImageOn, soundImageOff, musicImageOn, musicImageOff;
    public Text starsCounterText;
    public Text allStarsCounterText;
    private int allStars;
    private int allStarsSeasons;

    public void Awake()
    {
        Instance = this;
        UI.SetActive(true);
        LevelUI.SetActive(false);
        LoadingUI.SetActive(false);
        SettingsUI.SetActive(false);

        //Time.timeScale = 1;

        if (soundImage)
            soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;
        if (musicImage)
            musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;
        if (!GlobalValue.isSound)
            SoundManager.SoundVolume = 0;
        if (!GlobalValue.isMusic)
            SoundManager.MusicVolume = 0;
    }

    private void Start()
    {
        SoundManager.PlayGameMusic();

        for (int i = 0; i < SaveManager.instance.AllStars.Length; i++)
        {
            allStarsSeasons += SaveManager.instance.AllStars[i];
            YandexGame.NewLeaderboardScores("AllStars", allStarsSeasons);
        }
        Debug.Log(allStars + " Start AllStars");

        for (int i = 0; i < SaveManager.instance.Stars.Length; i++)
        {
            allStars += SaveManager.instance.Stars[i];
        }
        Debug.Log(allStars + " Start Stars");

        starsCounterText.text = allStars.ToString();
    }

    //public void ShowUIAfterLoad()
    //{
    //    for (int i = 0; i < SaveManager.instance.Stars.Length; i++)
    //    {
    //        allStars += SaveManager.instance.Stars[i];
    //    }

    //    starsCounterText.text = allStars.ToString();
    //}

    public void ShowLevelUI(bool open)
    {
        SoundManager.Click();
        LevelUI.SetActive(open);

        //for (int i = 0; i < 30; i++)
        //{
        //    allStars += PlayerPrefs.GetInt("SavStars" + i, 0);
        //}

        //for (int i = 0; i < SaveManager.instance.Stars.Length; i++)
        //{
        //    allStars += SaveManager.instance.Stars[i];
        //}

        Debug.Log(allStars + " ShowLevelUIStars");
    }

    public void ShowSettings(bool open)
    {
        SoundManager.Click();
        SettingsUI.SetActive(open);
    }

    public void LoadLevel()
    {
        LoadingUI.SetActive(true);
        SceneManager.LoadSceneAsync(SaveManager.levelPlaying);
    }

    #region Music and Sound
    public void TurnSound()
    {
        GlobalValue.isSound = !GlobalValue.isSound;
        soundImage.sprite = GlobalValue.isSound ? soundImageOn : soundImageOff;

        SoundManager.SoundVolume = GlobalValue.isSound ? 1 : 0;
        SoundManager.Click();
    }

    public void TurnMusic()
    {
        GlobalValue.isMusic = !GlobalValue.isMusic;
        musicImage.sprite = GlobalValue.isMusic ? musicImageOn : musicImageOff;

        SoundManager.MusicVolume = GlobalValue.isMusic ? SoundManager.Instance.musicsGameVolume : 0;
        SoundManager.Click();
    }
    #endregion
}