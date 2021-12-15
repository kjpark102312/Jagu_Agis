using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

using DG.Tweening;

public class TitleUI : MonoBehaviour
{
    public GameObject StageSelectPanel;
    public GameObject BackGroundPanel;
    public Text touchText;
    
    private bool isClickScreen = false;

    private void Start()
    {
        TouchTextAnim();
        SoundManager.Instance.PlayBGMSound();
    }

    public void ClickScreen()
    {
        isClickScreen = !isClickScreen;
        StageSelectPanel.SetActive(isClickScreen);
        BackGroundPanel.SetActive(!isClickScreen);
    }

    public void TouchTextAnim()
    {
        touchText.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        touchText.DOFade(0f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    public void ReturnBtn()
    {
        StageSelectPanel.SetActive(false);
        BackGroundPanel.SetActive(true);
    }

}
