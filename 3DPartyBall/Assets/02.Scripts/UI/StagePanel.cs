using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePanel : MonoBehaviour
{

    public GameObject[] panels;

    public Button right;
    public Button left;

    int index = 0;

    private void Start()
    {
        right.onClick.AddListener(() =>
        {
            panels[index].SetActive(false);
            index++;
            panels[index].SetActive(true);
        });

        left.onClick.AddListener(() =>
        {
            panels[index].SetActive(false);
            index--;
            panels[index].SetActive(true);
        });
    }
}
