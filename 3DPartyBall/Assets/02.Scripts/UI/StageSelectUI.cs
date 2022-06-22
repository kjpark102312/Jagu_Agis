using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// StageSelect UI의 자식 오브젝트 기능에 대해 정의 한 것
/// </summary>
public class StageSelectUI : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Button returnButton;

    private List<Button> _stageButtonList = new List<Button>();
    private Transform content;

    private void Awake()
    {
        content = scrollRect.content;                         // 스테이지 이동하는 버튼들이 있는 함수
        content.position += Vector3.right * 10000;

        for(int i = 0; i < content.childCount; i++)
        {
            Button button = content.GetChild(i).GetComponent<Button>();

            if(button != null)
            {
                string str = button.GetComponentInChildren<Text>().text;

                button.onClick.AddListener(() =>
                {
                    // 여기서 스테이지 불러오는 코드 작성하기
                    Debug.Log(str);
                    StageManager.Instance.LoadStage(str);
                });
            }

            _stageButtonList.Add(button);
        }

        returnButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIPanel.Title).SetActive(true);
        });
    }

    private void OnDisable()
    {
        if (content != null)
            content.position += Vector3.right * 10000;
    }
}
