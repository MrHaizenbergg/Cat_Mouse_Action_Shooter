using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCatInScene : MonoBehaviour
{
    //[SerializeField] private GameObject[] catSkins;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private Material[] material;

    private void Awake()
    {
        if (SaveManager.instance.catsUnlocked[1] == true && SaveManager.instance.currentCatSkin == 1)
            ChooseCatSkin(1);
        else if (SaveManager.instance.catsUnlocked[2] == true && SaveManager.instance.currentCatSkin == 2)
            ChooseCatSkin(2);
        else if (SaveManager.instance.catsUnlocked[3] == true && SaveManager.instance.currentCatSkin == 3)
            ChooseCatSkin(3);
        else
            ChooseCatSkin(0);

        //ChooseCatSkin(SaveManager.instance.currentCatSkin);
        //ChooseCatSkin(SaveManager.instance.catsUnlocked);
    }

    private void ChooseCatSkin(int index)
    {
        meshRenderer.material = material[index];
        Debug.Log("Switch Skin on " + index);
    }
}