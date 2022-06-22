using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinUI : MonoBehaviour
{
    public ScrollRect scrollRect;
    public Button returnButton;

    private List<Button> _skinButtonList = new List<Button>();
    private Transform content;

    private void Awake()
    {
        content = scrollRect.content;                         // 스킨 바꾸는 버튼들이 있는 곳
        content.position += Vector3.right * 10000;

        for (int i = 0; i < content.childCount; i++)
        {
            Button button = content.GetChild(i).GetComponent<Button>();

            if (button != null)
            {
                string str = button.GetComponentInChildren<Text>().text;

                button.onClick.AddListener(() =>
                {
                    // 여기서 스킨 불러오는 코드 작성
                    Debug.Log(str);
                });
            }

            _skinButtonList.Add(button);
        }

        returnButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIPanel.Title).SetActive(true);
        });
    }

    private void OnDisable()
    {
        if(content != null)
            content.position += Vector3.right * 10000;
    }
}
