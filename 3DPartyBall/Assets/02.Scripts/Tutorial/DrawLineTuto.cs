using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DrawLineTuto : MonoBehaviour
{

    public Image fingerImage;

    public Vector2 originPos;
    void Start()
    {
        originPos = fingerImage.rectTransform.anchoredPosition;
        MoveImage();
    }

  

    void MoveImage()
    {
        fingerImage.rectTransform.DOAnchorPos(new Vector2(originPos.x, 100), 2f).SetLoops(-1);
    }
}
