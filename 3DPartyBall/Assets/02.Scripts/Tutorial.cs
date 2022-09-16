using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public GameObject firstTutoPanel;
    public GameObject secondTutoPanel;
    public GameObject thirdTutoPanel;


    [SerializeField] Button first;
    [SerializeField] Button second;
    [SerializeField] Button third;

    public const string TutorialKey = "Tuto";
    public const string seTutorialKey = "line";
    public const string thTutorialKey = "count";

    void Start()
    {
        Debug.Log(PlayerPrefs.GetString(TutorialKey));
        if (PlayerPrefs.GetString(TutorialKey) != "")
            return;

        

        firstTutoPanel.SetActive(true);

        first.onClick.AddListener(() =>
        {
            firstTutoPanel.SetActive(false);
        });

        second.onClick.AddListener(() =>
        {
            secondTutoPanel.SetActive(false);
        });

        third.onClick.AddListener(() =>
        {
            thirdTutoPanel.SetActive(false);
        });

        PlayerPrefs.SetString(TutorialKey, "true");

    }

    public void ShowSecondPanel()
    {
        if(PlayerPrefs.GetString(seTutorialKey) != "true")
        {
            secondTutoPanel.SetActive(true);
        }
    }

    public void ShowThirdPanel()
    {
        if (PlayerPrefs.GetString(thTutorialKey) != "true")
        {
            thirdTutoPanel.SetActive(true);
        }
    }

    
}
