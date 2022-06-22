using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UI Panel���� ���������� ����
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

        // ���� �ε� �ɶ����� ��ųʸ��� �ִ� �� �ʱ�ȭ
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
