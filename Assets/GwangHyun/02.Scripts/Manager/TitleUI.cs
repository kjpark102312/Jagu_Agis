using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    public GameObject StageSelectPanel;


    public void ClickScreen()
    {
        StageSelectPanel.SetActive(true);
    }
}
