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

    [SerializeField] Sprite soundOnImage = null;
    [SerializeField] Sprite soundOffImage = null;


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
            mySequence.Kill();
            StopCoroutine(ButtonActive());
            if(!isOnOption)
            {

                backgroundPanel.SetActive(true);
                DOTween.To(() => backgroundPanel.GetComponent<RectTransform>().sizeDelta, x => backgroundPanel.GetComponent<RectTransform>().sizeDelta = x, new Vector2(150, 440), 0.4f);
                StartCoroutine(ButtonActive());
            }
            else
            {
                DOTween.To(() => backgroundPanel.GetComponent<RectTransform>().sizeDelta, 
                    x => backgroundPanel.GetComponent<RectTransform>().sizeDelta = x, new Vector2(150, 140), 0.4f);

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
                SoundManager.Instance.ResumeAllSound();
                soundButton.GetComponent<Image>().sprite = soundOnImage;
                Debug.Log("사운드 켜기");
            }
            else
            {
                //사운드 끄기
                SoundManager.Instance.PauseAllSound();
                soundButton.GetComponent<Image>().sprite = soundOffImage;
                Debug.Log("사운드 끄기");
            }
            isMute = !isMute;
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
