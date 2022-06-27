using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameClearUI : MonoBehaviour
{
    [SerializeField] Image fadeImage = null;

    [SerializeField] Button nextStageButton = null;
    [SerializeField] Button selectStageButton = null;
    [SerializeField] Button reStartButton = null;

    private void Awake()
    {
        nextStageButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Main");
        });

        selectStageButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });

        reStartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}
