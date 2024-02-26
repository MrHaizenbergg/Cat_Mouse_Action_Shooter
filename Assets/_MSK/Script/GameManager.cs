using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState { Waiting, Playing, GameOver, Finish }
    [ReadOnly] public GameState gameState;
    PlayerController playerController;

    //define player reborn event, it will called all registered objects
    public delegate void OnPlayerReborn();
    public static OnPlayerReborn playerRebornEvent;
    public static bool gameIsPaused;

    //[DllImport("__Internal")]
    //private static extern void ShowAdv();

    public PlayerController Player
    {
        get
        {
            if (playerController != null)
                return playerController;
            else
            {
                playerController = FindObjectOfType<PlayerController>();
                if (playerController)
                    return playerController;
                else
                    return null;
            }
        }
    }

    [ReadOnly] public Vector3 checkPoint;
    public void SetCheckPoint(Vector3 pos)
    {
        checkPoint = pos;
    }

    public void Awake()
    {
        Instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (CharacterHolder.Instance != null)
        {
            if (Player != null)
                Destroy(Player.gameObject);

            Instantiate(CharacterHolder.Instance.GetPickedCharacter(), Player.transform.position, Player.transform.rotation);
        }
        else
        {
            var FindCharacterHolder = FindObjectOfType<CharacterHolder>();
            if (FindCharacterHolder)
            {
                if (Player != null)
                    Destroy(Player.gameObject);

                Instantiate(FindCharacterHolder.GetPickedCharacter(), Player.transform.position, Player.transform.rotation);
            }
        }
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

#if UNITY_WEBGL 
    public void CloseAdv()
    {
        //Time.timeScale = 1;
        gameIsPaused = false;
        AudioListener.pause = false;
    }

    public void SoundOffAdv()
    {
        //Time.timeScale = 0;
        gameIsPaused = true;
        AudioListener.pause = true;
    }
#endif


    private void Start()
    {
#if UNITY_WEBGL      
        //ShowAdv();
#endif

        SoundManager.PlayGameMusic();

        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowAdmobBanner(false);
        }
    }

    private void Update()
    {
        if (gameState == GameState.Waiting)
        {
            if (Input.anyKeyDown && !gameIsPaused)
            {
                PlayGame();
            }
        }
    }

    public void PlayGame()
    {
        gameState = GameState.Playing;
        Player.Play();
        MenuManager.Instance.Play();
        SoundManager.PlaySfx(SoundManager.Instance.soundBeginGame);
    }

    public void GameOver()
    {
        if (gameState == GameState.GameOver)
            return;

        SoundManager.Instance.PauseMusic(true);
        Time.timeScale = 1;
        gameState = GameState.GameOver;

        Player.Gameover();
        SoundManager.PlaySfx(SoundManager.Instance.soundGameover);
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowNormalAd(GameState.GameOver);
            AdsManager.Instance.ShowAdmobBanner(true);
        }

        MenuManager.Instance.GameOver();
    }

    public void FinishGame()
    {
        if (gameState == GameState.Finish)
            return;

        StarsHandler.instance.SaveStarProgress(SceneManager.GetActiveScene().buildIndex);
        SaveManager.instance.money += 20;
        
        gameState = GameState.Finish;

        if (SaveManager.levelPlaying >= SaveManager.levelHighest)
        {
            SaveManager.levelHighest++;
        }


        SaveManager.instance.Save();      

        MenuManager.Instance.Finish();
        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowNormalAd(GameState.Finish);
            AdsManager.Instance.ShowAdmobBanner(true);
        }

        SoundManager.PlaySfx(SoundManager.Instance.soundGamefinish);
    }

    public void Continue()
    {
        if (playerRebornEvent != null)
            playerRebornEvent();

        SoundManager.Instance.PauseMusic(false);

        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowAdmobBanner(false);
        }

        Invoke("SpawnPlayer", 0.1f);
    }

    void SpawnPlayer()
    {
        Destroy(playerController.gameObject);
        playerController = Instantiate(CharacterHolder.Instance.GetPickedCharacter(), checkPoint, Quaternion.identity).GetComponent<PlayerController>();
        gameState = GameState.Waiting;
    }
}