using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TitleUI의 자식 오브젝트들의 모든 기능을 정의하는 곳
/// </summary>
public class TitleUI : MonoBehaviour
{
    public Button playButton;       // 누르면 StageSelectUI 활성화
    public Button skinButton;       // 누르면 SkinUI 활성화
    public Button developerButton;  // 누르면 DeveloperUI 활성화

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            // SelectStageUI 활성화
            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIPanel.StageSelect).SetActive(true);
            SoundManager.Instance.PlaySFXSound("BtnTouchSound");

            PlayerPrefs.SetString("IsFirst", "false");
        });

        skinButton.onClick.AddListener(() =>
        {
            // SkinUI 활성화
            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIPanel.Skin).SetActive(true);
            SoundManager.Instance.PlaySFXSound("BtnTouchSound");
        });

        developerButton.onClick.AddListener(() =>
        {
            // DeveloperUI 활성화
            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIPanel.Developer).SetActive(true);
            SoundManager.Instance.PlaySFXSound("BtnTouchSound");
        });
    }

    private void Start()
    {
        if(PlayerPrefs.GetString("IsFirst") == "false")
        {
            UIManager.Instance.GetUI(UIPanel.StageSelect).SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
