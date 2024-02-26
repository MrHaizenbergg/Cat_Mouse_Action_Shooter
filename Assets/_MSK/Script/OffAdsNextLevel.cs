using UnityEngine;
using UnityEngine.SceneManagement;

public class OffAdsNextLevel : MonoBehaviour
{
    [SerializeField] private GameObject _btnAdsNextLvl;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 20)
        {
            _btnAdsNextLvl.SetActive(false);
            Debug.Log("ReturnStart");
        }
    }
}
