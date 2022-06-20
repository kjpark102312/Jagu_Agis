using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Transform content;   // 부모 Trm

    private List<GameObject> stageObjs = new List<GameObject>(); // Resources에 저장되어 있는 스테이지들의 모임

    private void Awake()
    {
        GameObject[] objs = Resources.LoadAll<GameObject>("Maps");

        // objs에 있는거 모두 stageObjs로 이동
        foreach (GameObject obj in objs)
        {
            Debug.Log("이 맵이 추가됨 : " + obj.name);
            GameObject o = Instantiate(obj, content);

            StageValue sv = o.GetComponent<StageValue>();
            sv.startButton.onClick.AddListener(() =>
            {
                LoadStage(sv.stageName);
            });

            stageObjs.Add(o);
        }
    }

    public void LoadStage(string stageName)
    {
        Debug.Log(stageName + " 스테이지 불러오기!");
        // 스테이지 불러오는 방식은 생각하기
    }


    //public GameObject StageBtn;

    //public Button[] buttons;

    //private int currentStage = 0;

    //void Start()
    //{
    //    buttons = StageBtn.GetComponentsInChildren<Button>();
    //    StageUnLock();
    //}

    //public void StageUnLock()
    //{
    //    currentStage = PlayerPrefs.GetInt("stageUnlock");

    //    Debug.Log(currentStage);

    //    for (int i = currentStage + 1; i < buttons.Length; i++)
    //    {
    //        buttons[i].interactable = false;
    //    }


    //}

}
