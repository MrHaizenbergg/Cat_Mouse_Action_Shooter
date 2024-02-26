using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternationalTextMesh : MonoBehaviour
{
    public GameObject ruTable;
    public GameObject enTable;

    private void Start()
    {
        if (Language.Instance.currentLanguage == "en")
        {
            enTable.SetActive(true);
            ruTable.SetActive(false);
        }
        else if (Language.Instance.currentLanguage == "ru")
        {
            ruTable.SetActive(true);
            enTable.SetActive(false);
        }
        else
        {
            enTable.SetActive(true);
            ruTable.SetActive(false);
        }
    }
}