using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isStageSelect;

    public List<GameObject> gravities = new List<GameObject>();
    public List<GameObject> cloneGravities = new List<GameObject>();
    public GameObject mainSell = null;

    public bool isStageClear;

    public int stageName;


    private bool _isPause;
    public bool IsPause
    {
        get
        {
            return _isPause;
        }

        set
        {
            _isPause = value;
            _onPauseChanged(_isPause);
        }
    }
    public Action<bool> _onPauseChanged = (_isPaused) => { };

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = Instantiate(new GameObject());
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }

        _instance = this;
        DontDestroyOnLoad(_instance.gameObject);

        SceneManager.sceneLoaded += (scene, loadSceneMode) =>
        {
            Debug.Log("새 씬 로드");
            isStageSelect = false;
            IsPause = false;
            gravities.Clear();
            cloneGravities.Clear();
        };
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    
}
