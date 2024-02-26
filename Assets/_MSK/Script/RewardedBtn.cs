using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class RewardedBtn : MonoBehaviour
{
    [SerializeField] private GameObject _btnRewarded;
   
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
        Debug.Log("RewAddListener");
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
        Debug.Log("RewDisable RemoveListeners");
    }

    public void Rewarded(int id)
    {
        if (id == 0)
        {
            SaveManager.instance.money += 100;
            SaveManager.instance.Save();
            _btnRewarded.SetActive(false);
        }
        else if (id == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            Debug.Log("RewNextLevel: " + SceneManager.GetActiveScene().buildIndex+1);
        }
        Debug.Log("CloseAdvRewMoneyAdd");
    }

    public void OpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }
}
