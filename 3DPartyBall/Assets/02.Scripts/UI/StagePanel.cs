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
            if (index == 1)
                return;

            panels[index].SetActive(false);
            index++;
            panels[index].SetActive(true);
        });

        left.onClick.AddListener(() =>
        {
            if (index == 0)
                return;

            panels[index].SetActive(false);
            index--;
            panels[index].SetActive(true);
        });
    }
}
