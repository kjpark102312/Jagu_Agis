using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Scroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private ScrollRect sR;

    private void Awake()
    {
        sR = transform.GetComponent<ScrollRect>();
    }

    public void OnBeginDrag(PointerEventData e)
    {
        sR.OnBeginDrag(e);
    }

    public void OnDrag(PointerEventData e)
    {
        sR.OnDrag(e);
    }

    public void OnEndDrag(PointerEventData e)
    {
        sR.OnEndDrag(e);
    }
}