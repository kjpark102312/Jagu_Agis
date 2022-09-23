using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectSellTuto : MonoBehaviour
{
    public Image guideImage;

    public Sprite fingerImage;
    public Sprite touchImage;



    private void Start()
    {
        StartCoroutine(ChangeImage());
    }

    IEnumerator ChangeImage()
    {
        while(true)
        {
            guideImage.sprite = fingerImage;
            yield return new WaitForSecondsRealtime(0.4f);
            guideImage.sprite = touchImage;
            yield return new WaitForSecondsRealtime(0.4f);
        }
    }
}
