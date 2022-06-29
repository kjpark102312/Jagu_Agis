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

    [Header("옵션관련 오브젝트")]
    [SerializeField] GameObject backgroundPanel = null;
    [SerializeField] GameObject circlePanel = null;

    [Header("홈버튼, 재시작버튼 관련 오브젝트")]
    [SerializeField] Image fadeImage = null;




    bool isMute = false;
    bool isOnOption = false;

    Sequence mySequence;

    private void Awake()
    {
        reStartButton.onClick.AddListener(() =>
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        });

        optionParentButton.onClick.AddListener(() =>
        {
            //옵션패널 켜지기
            if(!isOnOption)
            {
                backgroundPanel.SetActive(true);
                backgroundPanel.transform.DOScaleY(1.0f, 0.4f);
                circlePanel.SetActive(false);
                StartCoroutine(ButtonActive());
            }
            else
            {
                mySequence = DOTween.Sequence();

                
                mySequence.Append(backgroundPanel.transform.DOScaleY(0.3f, 0.4f));
                mySequence.Append(backgroundPanel.transform.DOScaleX(0.5f, 0.1f));
                mySequence.AppendCallback(() => {
                    backgroundPanel.SetActive(false);
                    backgroundPanel.transform.localScale = new Vector3(1, 0.3f, 1f);
                    Debug.Log("Sad");
                });

                circlePanel.SetActive(true);
                soundButton.gameObject.SetActive(false);
                homeButton.gameObject.SetActive(false);
            }
            isOnOption = !isOnOption;
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
            fadeImage.gameObject.SetActive(true);
            fadeImage.DOFade(1.0f, 1.0f).OnComplete(() =>
            {
                SceneManager.LoadScene("Title");
            });
        });
    }

    IEnumerator ButtonActive()
    {
        yield return new WaitForSeconds(0.15f);
        soundButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        homeButton.gameObject.SetActive(true);
    }
}
