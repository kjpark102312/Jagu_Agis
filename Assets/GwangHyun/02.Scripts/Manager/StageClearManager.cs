using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearManager : MonoBehaviour
{
    private int nowStage;

    private void Start()
    {
        nowStage = GameManager.Instance.nowStageIndex;
    }

    public void StageClear()
    {
        PlayerPrefs.SetInt("stageUnlock", nowStage);

        nowStage++;
    }

    public void NextStage()
    {
        GameManager.Instance.LoadScene(nowStage + 1);
    }
}
