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


    [SerializeField] Button dataResetButton;

    void Start()
    {
        if (PlayerPrefs.GetString(TutorialKey) != "")
            return;
        

        firstTutoPanel.SetActive(true);
        GameManager.Instance.IsPause = false;

        third.onClick.AddListener(() =>
        {
            thirdTutoPanel.SetActive(false);
            GameManager.Instance.IsPause = false;
            PlayerPrefs.SetString(thTutorialKey, "true");
        });
    }

    public void ShowSecondPanel()
    {
        Debug.Log(PlayerPrefs.GetString(seTutorialKey));

        if (PlayerPrefs.GetString(seTutorialKey) != "true")
        {
            Debug.Log(0);
            secondTutoPanel.SetActive(true);
            
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

    public void HideFirstPanel()
    {
        firstTutoPanel.SetActive(false);
        GameManager.Instance.IsPause = false;
        PlayerPrefs.SetString(TutorialKey, "true");
    }

    public void HideSecondPanel()
    {
        secondTutoPanel.SetActive(false);
        GameManager.Instance.IsPause = false;
        PlayerPrefs.SetString(seTutorialKey, "true");
    }
}
