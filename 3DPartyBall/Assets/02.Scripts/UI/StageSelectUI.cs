using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    public List<Button> stageButtons;
    public Button returnButton;

    private void Awake()
    {
        for (int i = 0; i < stageButtons.Count; i++)
        {
            stageButtons[i].onClick.AddListener(() =>
            {
                // 스테이지 이동 하는 함수 사용
            });
        }

        returnButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIPanel.Title).SetActive(true);
        });
    }
}
