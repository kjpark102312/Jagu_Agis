using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    public bool isSelect;

    [SerializeField]
    private GameObject selectPanel;

    private Transform mainSell;


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

            if (hit && !GameManager.Instance.isStageSelect)
            {
                selectPanel.SetActive(true);
                rect.position = Camera.main.WorldToScreenPoint(hit.transform.gameObject.transform.position);
                mainSell = hit.transform.parent;
            }
        }
    }

    public void SelectSell()
    {
        GameManager.Instance.isStageSelect = true;

        EventSystem.current.currentSelectedGameObject.SetActive(false);

        GetComponent<DrawGravity>().mainMap = mainSell;
    }
}
