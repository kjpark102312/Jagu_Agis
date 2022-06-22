using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinUI : MonoBehaviour
{
    public List<Button> skinButtonList;
    public Button returnButton;

    private void Awake()
    {
        for (int i = 0; i < skinButtonList.Count; i++)
        {
            skinButtonList[i].onClick.AddListener(() =>
            {
                // ��Ų �ٲٴ� �Լ� ����ϱ�
            });
        }

        returnButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIPanel.Title).SetActive(true);
        });
    }
}
