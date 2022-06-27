using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class OptionUI : MonoBehaviour
{
    [SerializeField] Button reStartButton = null;
    [SerializeField] Button optionParentButton = null;
    [SerializeField] Button soundButton = null;
    [SerializeField] Button homeButton = null;

    bool isMute = false;

    private void Awake()
    {
        reStartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        optionParentButton.onClick.AddListener(() =>
        {
            //옵션패널 켜지기
        });

        soundButton.onClick.AddListener(() =>
        {
            if(isMute)
            {
                //사운드 켜기
            }
            else
            {
                //사운드 끄기
            }
        });

        homeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });
    }
}
