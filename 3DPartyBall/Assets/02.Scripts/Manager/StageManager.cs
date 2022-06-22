using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Transform content;   // 부모 Trm

    private List<GameObject> _selectUIList = new List<GameObject>();    // Resources에 저장되어 있는 UI들의 모임
    private Dictionary<string, GameObject> _stageDic = new Dictionary<string, GameObject>();       // Resources에 저장되어 있는 stage들의 모임 ( Instantiate 안한 상태 )

    private GameObject _currentStage = null;

    private void Awake()
    {
        GameObject[] uis = Resources.LoadAll<GameObject>("MapSelectUI"); // MapSelectUI 폴더 안에 있는 UI 오브젝트 모두 가져오기
        GameObject[] stages = Resources.LoadAll<GameObject>("Stages");      // Stages 폴더 안에 있는 오브젝트 모두 가져옴

        // objs에 있는거 모두 stageObjs로 이동
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
            Debug.Log("들어간 스테이지 이름 : " + stage.name);
            _stageDic.Add(stage.name, stage);
        }
    }

    public void LoadStage(string stageName)
    { 
        if (!(SceneManager.GetActiveScene().name == "Main"))
            SceneManager.LoadScene("Main");
        else
        {
            // 이미 생성되어 있는 스테이지 삭제
            Destroy(_currentStage);
        }

        SceneManager.sceneLoaded += (scene, loadSceneMode) =>
        {
            Debug.Log("생성된 스테이지 이름 : " + stageName);
            _currentStage = Instantiate(_stageDic[stageName]);
        };

        // 스테이지 생성하기

        // 스테이지 불러오는 방식은 잘 생각하기
        // 스테이지 이동 순서는 
        // 1. 게임 씬 이동
        // 2. 해당 스테이지 생성
        // 이렇게 하는게 맞지 않을까
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
