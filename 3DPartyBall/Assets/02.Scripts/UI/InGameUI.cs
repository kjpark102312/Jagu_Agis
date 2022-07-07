using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class InGameUI : MonoBehaviour
{
    [SerializeField] Text magneticCountText;
    [SerializeField] Text[] warningText;
    public void UpdateLineCount(int lineCount)
    {
        if (lineCount < 0)
            return;
        if (lineCount > 100)
        {
            magneticCountText.text = "¡Ä";
            return;
        }
         

        magneticCountText.text = lineCount.ToString();
    }

    public void WarningText(int i)
    {
        if (GameManager.Instance.IsPause)
            return;

        warningText[i].DOFade(255f, 0.1f);
        warningText[i].DOFade(0f, 0.7f);
    }


}
