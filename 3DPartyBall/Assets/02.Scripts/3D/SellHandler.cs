using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellHandler : MonoBehaviour
{
    public GameObject mainSell;
    public List<GameObject> subSells = new List<GameObject>();
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

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, 1 << 6))
            {
                mainSell = hit.transform.gameObject;

                foreach(var items in FindObjectsOfType<Sell>())
                {
                    subSells.Add(items.gameObject);
                }
                subSells.Remove(mainSell);
            }
            GameManager.Instance.isStageSelect = true;
        }
    }
}
