using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InGameUI : MonoBehaviour
{
    [SerializeField] Text magneticCountText;

    public void UpdateLineCount(int lineCount)
    {
        if (lineCount < 0)
            return;

        magneticCountText.text = lineCount.ToString();
    }


}
