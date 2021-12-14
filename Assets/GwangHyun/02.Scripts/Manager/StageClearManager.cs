using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearManager : MonoBehaviour
{
    private int nowStage;
    public GameObject clearPanel;

    
    private static StageClearManager instance = null;
    public static StageClearManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("instance°¡ nullÀÔ´Ï´Ù.");

                return null;
            }

            return instance;
        }

        private set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Áßº¹µÈ instance ÀÔ´Ï´Ù.");
            Destroy(this);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        nowStage = GameManager.Instance.nowStageIndex;
    }

    public void StageClear()
    {
        if(GameManager.Instance.isStageClear)
        {
            clearPanel.SetActive(true);
            PlayerPrefs.SetInt("stageUnlock", nowStage);
            nowStage++;
        }
    }

    public void NextStage()
    {
        Debug.Log(nowStage);
        GameManager.Instance.LoadScene(nowStage);
        GameManager.Instance.isStageClear = false;
    }
}
