using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TitleUI�� �ڽ� ������Ʈ���� ��� ����� �����ϴ� ��
/// </summary>
public class TitleUI : MonoBehaviour
{
    public Button playButton;       // ������ StageSelectUI Ȱ��ȭ
    public Button skinButton;       // ������ SkinUI Ȱ��ȭ
    public Button developerButton;  // ������ DeveloperUI Ȱ��ȭ

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            // SelectStageUI Ȱ��ȭ
            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIPanel.StageSelect).SetActive(true);
            SoundManager.Instance.PlaySFXSound("BtnTouchSound");

            PlayerPrefs.SetString("IsFirst", "false");
        });

        skinButton.onClick.AddListener(() =>
        {
            // SkinUI Ȱ��ȭ
            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIPanel.Skin).SetActive(true);
            SoundManager.Instance.PlaySFXSound("BtnTouchSound");
        });

        developerButton.onClick.AddListener(() =>
        {
            // DeveloperUI Ȱ��ȭ
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
