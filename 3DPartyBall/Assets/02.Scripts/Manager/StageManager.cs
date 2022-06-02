using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject StageBtn;

    public Button[] buttons;

    private int currentStage = 0;

    void Start()
    {
        buttons = StageBtn.GetComponentsInChildren<Button>();
        StageUnLock();
    }

    public void StageUnLock()
    {
        currentStage = PlayerPrefs.GetInt("stageUnlock");

        Debug.Log(currentStage);

        for (int i = currentStage + 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }

}
