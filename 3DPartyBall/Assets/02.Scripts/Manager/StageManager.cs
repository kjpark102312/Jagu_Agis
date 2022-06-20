using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Transform content;   // �θ� Trm

    private List<GameObject> stageObjs = new List<GameObject>(); // Resources�� ����Ǿ� �ִ� ������������ ����

    private void Awake()
    {
        GameObject[] objs = Resources.LoadAll<GameObject>("Maps");

        // objs�� �ִ°� ��� stageObjs�� �̵�
        foreach (GameObject obj in objs)
        {
            Debug.Log("�� ���� �߰��� : " + obj.name);
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
        Debug.Log(stageName + " �������� �ҷ�����!");
        // �������� �ҷ����� ����� �����ϱ�
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
