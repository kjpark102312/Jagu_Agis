using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        _instance = this;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
