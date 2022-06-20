using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageValue : MonoBehaviour
{
    [HideInInspector] public string stageName;      // ���������� �̸�
    [HideInInspector] public Button startButton;    // �������� �����ϴ� ��ư

    private void Awake()
    {
        stageName = transform.Find("StageName").GetComponent<Text>().text;
        startButton = transform.GetComponentInChildren<Button>();
    }
}
