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
        GameManager.Instance.IsPause = true;
        first.onClick.AddListener(() =>
        {
            firstTutoPanel.SetActive(false);
            GameManager.Instance.IsPause = false;
        });

        second.onClick.AddListener(() =>
        {
            secondTutoPanel.SetActive(false);
            GameManager.Instance.IsPause = false;
        });

        third.onClick.AddListener(() =>
        {
            thirdTutoPanel.SetActive(false);
            GameManager.Instance.IsPause = false;
        });

        PlayerPrefs.SetString(TutorialKey, "true");

    }

    public void ShowSecondPanel()
    {
        if(PlayerPrefs.GetString(seTutorialKey) != "true")
        {
            secondTutoPanel.SetActive(true);
            GameManager.Instance.IsPause = true;
        }
    }

    public void ShowThirdPanel()
    {
        if (PlayerPrefs.GetString(thTutorialKey) != "true")
        {
            thirdTutoPanel.SetActive(true);
            GameManager.Instance.IsPause = true;
        }
    }

    
}
