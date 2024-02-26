using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayController : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] scriptableObjects;
    [SerializeField] private SeasonDisplay seasonDisplay;
    private int currentIndex;

    private void Awake()
    {
        ChangeSeason(0);
    }

    public void ChangeSeason(int index)
    {
        currentIndex += index;

        if (currentIndex < 0) currentIndex = scriptableObjects.Length - 1;
        else if (currentIndex > scriptableObjects.Length-1) currentIndex = 0;

        if(seasonDisplay != null) seasonDisplay.DisplayLevel((MenuLevel)scriptableObjects[currentIndex]);
    }
}
