using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject StageBtn;

    private Button[] buttons;

    private int nowStage = 1;
    private int currentStage;


    void Start()
    {
        buttons = StageBtn.GetComponentsInChildren<Button>();
        StageClear();
        StageUnLock();
    }

    public void StageClear()
    {
        PlayerPrefs.SetInt("stageUnlock", nowStage);

        nowStage++;
    }

    public void StageUnLock()
    {
        currentStage = PlayerPrefs.GetInt("stageUnlock");

        for (int i = currentStage; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        Debug.Log(currentStage);
    }

    public void InStage(int stageIndex)
    {
        SceneManager.LoadScene("Main");
    }
}
