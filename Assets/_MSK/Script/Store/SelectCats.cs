using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCats : MonoBehaviour
{
    [Header("Navigations Buttons")]
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;

    [Header("Play/Buy Buttons")]
    //[SerializeField] private Button play;
    [SerializeField] private Button buy;
    [SerializeField] private Text priceText;

    [Header("Cats Atributes")]
    [SerializeField] private int[] catPrices;
    private int currentCat;

    private void Start()
    {
        currentCat = SaveManager.instance.currentCatSkin;
        SelectCat(currentCat);
    }

    private void Update()
    {
        if (buy.gameObject.activeInHierarchy)
            buy.interactable = SaveManager.instance.money >= catPrices[currentCat];
    }

    private void SelectCat(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (SaveManager.instance.catsUnlocked[currentCat])
        {
            buy.gameObject.SetActive(false);
        }
        else
        {
            buy.gameObject.SetActive(true);
            priceText.text = catPrices[currentCat] + "$";
        }
    }

    public void ChangeCat(int change)
    {
        currentCat += change;

        if (currentCat > transform.childCount - 1)
            currentCat = 0;
        else if (currentCat < 0)
            currentCat = transform.childCount - 1;

        SaveManager.instance.currentCatSkin = currentCat;
        SaveManager.instance.Save();

        SelectCat(currentCat);
    }

    public void BuyCat()
    {
        SoundManager.Instance.PlayBuyMusic();
        SaveManager.instance.money -= catPrices[currentCat];
        SaveManager.instance.catsUnlocked[currentCat] = true;
        SaveManager.instance.Save();
        UpdateUI();
    }
}
