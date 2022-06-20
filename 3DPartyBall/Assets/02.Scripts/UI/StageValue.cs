using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageValue : MonoBehaviour
{
    [HideInInspector] public string stageName;      // 스테이지의 이름
    [HideInInspector] public Button startButton;    // 스테이지 시작하는 버튼

    private void Awake()
    {
        stageName = transform.Find("StageName").GetComponent<Text>().text;
        startButton = transform.GetComponentInChildren<Button>();
    }
}
