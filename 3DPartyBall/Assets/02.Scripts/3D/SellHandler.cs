using System.Collections.Generic;
using UnityEngine;

public class SellHandler : MonoBehaviour
{
    public GameObject mainSell;
    public List<GameObject> subSells = new List<GameObject>();

    DrawGravityLine drawGravityLine = null;

    bool isCanSelect = false;


    private void Start()
    {
        drawGravityLine = FindObjectOfType<DrawGravityLine>();
    }
    void Update()
    {
        SelectSell();
    }

    void SelectSell()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.isStageSelect)
                return;
            if (!isCanSelect)
            {
                isCanSelect = true;
                return;
            }

            if (isCanSelect)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, 1 << 6))
                {
                    if (!hit.collider.CompareTag("Sell"))
                        return;

                    mainSell = hit.transform.gameObject;

                    foreach (var items in FindObjectsOfType<Sell>())
                    {
                        subSells.Add(items.gameObject);
                    }
                    subSells.Remove(mainSell);

                    GameManager.Instance.mainSell = mainSell;
                }
                GameManager.Instance.isStageSelect = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (GameManager.Instance.isStageSelect)
                drawGravityLine.enabled = true;
        }
    }
}
