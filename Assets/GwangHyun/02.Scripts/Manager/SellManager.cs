using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    private bool isSelect;

    [SerializeField]
    private GameObject selectPanel;

    private RectTransform rect;

    private void Start()
    {
        rect = selectPanel.GetComponent<RectTransform>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 100f, 1 << 6);

            if (hit)
            {
                selectPanel.SetActive(true);
                rect.position = Camera.main.WorldToScreenPoint(hit.transform.gameObject.transform.position);
            }
        }
    }

    public void SelectSell()
    {
        isSelect = true;

        GetComponent<DrawGravity>().mainMap = 
    }
}
