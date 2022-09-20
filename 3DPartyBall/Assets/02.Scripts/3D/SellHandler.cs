using System.Collections.Generic;
using UnityEngine;

public class SellHandler : MonoBehaviour
{
    public GameObject mainSell;
    public List<GameObject> subSells = new List<GameObject>();

    DrawGravityLine drawGravityLine = null;

    public bool isCanSelect = false;

    Tutorial tutorial;

    private void Start()
    {
        drawGravityLine = FindObjectOfType<DrawGravityLine>();
        tutorial = FindObjectOfType<Tutorial>();
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, 1 << 6))
            {
                if (!hit.collider.CompareTag("Sell"))
                    return;

                subSells.Clear();
                foreach (var items in FindObjectsOfType<Sell>())
                {
                    subSells.Add(items.gameObject);
                }

                for (int i = 0; i < subSells.Count; i++)
                {
                    subSells[i].GetComponent<SpriteRenderer>().material = subSells[i].GetComponent<Sell>().normalMat;
                }

                if (isCanSelect)
                {
                    CheckSelectSell();
                    tutorial.ShowSecondPanel();
                    PlayerPrefs.SetString("line", "true");


                    if (GameManager.Instance.mainSell != null)
                      return;
                }

                mainSell = hit.transform.parent.gameObject;
                Debug.Log(mainSell);

                mainSell.GetComponent<SpriteRenderer>().material = mainSell.GetComponent<Sell>().outlineMat;

                subSells.Remove(mainSell);

                

                isCanSelect = true;
                return;
            }

            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (GameManager.Instance.isStageSelect)
                drawGravityLine.enabled = true;
        }
    }


    void CheckSelectSell()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (Physics.Raycast(_ray, out _hit, Camera.main.farClipPlane, 1 << 6))
        {
            if (!_hit.collider.CompareTag("Sell"))
                return;
            if (mainSell != _hit.transform.parent.gameObject)
            {
                mainSell = _hit.transform.parent.gameObject;
                return;
            }
            if (mainSell == _hit.transform.parent.gameObject)
            {
                mainSell.GetComponent<SpriteRenderer>().material = mainSell.GetComponent<Sell>().normalMat;
                GameManager.Instance.mainSell = mainSell;
                GameManager.Instance.isStageSelect = true;
                subSells.Remove(mainSell);
                _hit.transform.gameObject.SetActive(false);
                return;
            }
        }
    }
}
