using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UI Panel들을 열거형으로 정리
/// </summary>
public enum UIPanel
{
    Title,
    StageSelect,
    Skin,
    InGame,
    Option,
    GameClear,
    Developer,
}

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _mainUI = null;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject().AddComponent<UIManager>();
                _instance._mainUI = GameObject.Find("MainUI");
            }

            return _instance;
        }
    }

    private static UIManager _instance = null;

    private Dictionary<UIPanel, GameObject> uiPanelDic = new Dictionary<UIPanel, GameObject>();

    private void Awake()
    {
        _instance = this;
        _instance._mainUI = GameObject.Find("MainUI");

        // 씬이 로딩 될때마다 딕셔너리에 있는 값 초기화
        SceneManager.sceneLoaded += (scene, loadSceneMode) =>
        {
            uiPanelDic.Clear();
        };
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public GameObject GetUI(UIPanel panel)
    {
        if(uiPanelDic.ContainsKey(panel))
            return uiPanelDic[panel];
        else
        {
            GameObject obj = _mainUI.transform.Find(panel.ToString()).gameObject;


            if (obj != null)
            {
                uiPanelDic[panel] = obj;
            }

            return obj;
        }
    }
}
