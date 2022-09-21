using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    private Dictionary<string, GameObject> stageObjDic = new Dictionary<string, GameObject>();

    private GameObject _currentStageObj = null;
    public string _currentStage = null;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        DontDestroyOnLoad(this);

        PlayerPrefs.DeleteAll();

        GameObject[] stages = Resources.LoadAll<GameObject>("Stages");

        for (int i = 0; i < stages.Length; i++)
        {
            stageObjDic[stages[i].name] = stages[i];
        }

        SceneManager.sceneLoaded += (scene, loadSceneMode) =>
        {
            if (scene.name == "Main")
            {
                if(_currentStage == "Lv_16")
                {
                    SceneManager.LoadScene("ComingSoon");
                    return;
                }
                _currentStageObj = Instantiate(stageObjDic[_currentStage]);
            }
        };
    }

    public void LoadStage(string stageName)
    {
        _currentStage = stageName;
        Debug.Log(_currentStage);
        if (stageName == "Coming Soon")
        {
            return;
        }
        if (stageName != "Tutorial")
        {
            string[] splitString = stageName.Split('_');
            GameManager.Instance.stageName = int.Parse(splitString[1]);
            PlayerPrefs.SetString("Tuto", "true");
        }


        SceneManager.LoadScene("Main");
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
