using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Transform content;   // �θ� Trm

    private List<GameObject> _selectUIList = new List<GameObject>();    // Resources�� ����Ǿ� �ִ� UI���� ����
    private Dictionary<string, GameObject> _stageDic = new Dictionary<string, GameObject>();       // Resources�� ����Ǿ� �ִ� stage���� ���� ( Instantiate ���� ���� )

    private GameObject _currentStage = null;
    private string _currentStageName = null;

    private void Awake()
    {
        GameObject[] uis = Resources.LoadAll<GameObject>("MapSelectUI"); // MapSelectUI ���� �ȿ� �ִ� UI ������Ʈ ��� ��������
        GameObject[] stages = Resources.LoadAll<GameObject>("Stages");      // Stages ���� �ȿ� �ִ� ������Ʈ ��� ������

        // objs�� �ִ°� ��� stageObjs�� �̵�
        foreach (GameObject ui in uis)
        {
            GameObject o = Instantiate(ui, content);
            StageSelectUI sv = o.GetComponent<StageSelectUI>();

            sv.startButton.onClick.AddListener(() =>
            {
                LoadStage(sv.stageName);
            });

            _selectUIList.Add(o);
        }

        foreach(GameObject stage in stages)
        {
            Debug.Log("�� �������� �̸� : " + stage.name);
            _stageDic.Add(stage.name, stage);
        }

        SceneManager.sceneLoaded += (scene, loadSceneMode) =>
        {
            if(scene.name.Contains("Main")) // �̵��Ϸ��� ���� �ΰ����̶�� ����
            {
                MakeStage();
            }
        };
    }

    public void LoadStage(string stageName)
    {
        _currentStageName = stageName;

        if (!(SceneManager.GetActiveScene().name == "Main"))
            SceneManager.LoadScene("Main");
        else
        {
            // �̹� �����Ǿ� �ִ� �������� ���� �ϰ� ���ο� �������� ����
            Destroy(_currentStage);
            MakeStage();
        }

        // �������� �����ϱ�

        // �������� �ҷ����� ����� �� �����ϱ�
        // �������� �̵� ������ 
        // 1. ���� �� �̵�
        // 2. �ش� �������� ����
        // �̷��� �ϴ°� ���� ������
    }

    private void MakeStage()
    {
        Debug.Log("������ �������� �̸� : " + _currentStageName);
        _currentStage = Instantiate(_stageDic[_currentStageName]);
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
