using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// StageSelect UI�� �ڽ� ������Ʈ ��ɿ� ���� ���� �� ��
/// </summary>
public class StageSelectUI : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Button returnButton;

    private List<Button> _stageButtonList = new List<Button>();
    private Transform content;

    private void Awake()
    {
        content = scrollRect.content;                         // �������� �̵��ϴ� ��ư���� �ִ� �Լ�
        content.position += Vector3.right * 10000;

        for(int i = 0; i < content.childCount; i++)
        {
            Button button = content.GetChild(i).GetComponent<Button>();

            if(button != null)
            {
                string str = button.GetComponentInChildren<Text>().text;

                button.onClick.AddListener(() =>
                {
                    // ���⼭ �������� �ҷ����� �ڵ� �ۼ��ϱ�
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
