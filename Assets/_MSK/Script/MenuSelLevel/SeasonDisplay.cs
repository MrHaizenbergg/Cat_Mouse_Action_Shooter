using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SeasonDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text seasonName;
    [SerializeField] private TMP_Text seasonDescription;
    [SerializeField] private Image seasonImage;
    [SerializeField] private Button seasonButton;
    [SerializeField] private GameObject seasonLock;
    [SerializeField] private GameObject[] seasonCanvas;

    public void DisplayLevel(MenuLevel menuLevel)
    {
        if (Language.Instance.currentLanguage == "en")
        {
            seasonName.text = menuLevel.seasonNameEn;
            seasonDescription.text = menuLevel.seasonDescriptionEn;
        }
        else if(Language.Instance.currentLanguage == "ru")
        {
            seasonName.text = menuLevel.seasonName;
            seasonDescription.text = menuLevel.seasonDescription;
        }
        else
        {
            seasonName.text = menuLevel.seasonNameEn;
            seasonDescription.text = menuLevel.seasonDescriptionEn;
        }

        seasonImage.sprite = menuLevel.seasonImage;

        bool seasonUnlocked = SaveManager.instance.unlockSeasons >= menuLevel.seasonIndex;

        seasonLock.SetActive(!seasonUnlocked);
        seasonButton.interactable = seasonUnlocked;

        if (seasonUnlocked)
            seasonImage.color = Color.white;
        else
            seasonImage.color = Color.grey;

        seasonButton.onClick.RemoveAllListeners();

        if (menuLevel.seasonIndex == 0)
            seasonButton.onClick.AddListener(OpenFirstSeason);
        else if (menuLevel.seasonIndex == 1)
            seasonButton.onClick.AddListener(OpenSecondSeason);
    }

    private void OpenFirstSeason()
    {
        HomeMenu.Instance.ShowLevelUI(this);
        //seasonCanvas[0].gameObject.SetActive(true);
    }

    private void OpenSecondSeason()
    {
        seasonCanvas[1].gameObject.SetActive(true);
    }
}
