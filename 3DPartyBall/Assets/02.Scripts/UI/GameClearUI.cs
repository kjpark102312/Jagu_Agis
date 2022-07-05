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
            fadeImage.gameObject.SetActive(true);
            fadeImage.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                if (StageManager.Instance._currentStage == "Tutorial")
                    StageManager.Instance.LoadStage("Lv_1");
                else
                {
                    int nextStage = GameManager.Instance.stageName + 1;
                    GameManager.Instance.stageName = nextStage;
                    StageManager.Instance.LoadStage("Lv_" + nextStage);
                }
                SceneManager.LoadScene("Main");
            });
        });

        selectStageButton.onClick.AddListener(() =>
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                SceneManager.LoadScene("Title");
            });
        });

        reStartButton.onClick.AddListener(() =>
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        });
    }
}
