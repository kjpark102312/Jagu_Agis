using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGravityLine : MonoBehaviour
{
    [SerializeField] GameObject line;

    public List<Vector3> linePos = new List<Vector3>();

    public List<GameObject> cloneGravities = new List<GameObject>();

    private bool isDrawing = false;

    GravityDir gravityDir;

    LineRenderer lr;
    BoxCollider col;

    // 라인생성시 오브젝트 받아오기 위한 변수
    GameObject curLineObj;

    SellHandler sellHandler;

    void Start()
    {
        sellHandler = FindObjectOfType<SellHandler>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(GameManager.Instance.isStageSelect)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, 1 << 6))
                {
                    isDrawing = true;
                    curLineObj = Instantiate(line);
                    CreateGravityLineClone();

                    lr = curLineObj.GetComponent<LineRenderer>();
                    gravityDir = curLineObj.GetComponent<GravityDir>();

                    linePos.Add(hit.point);

                    lr.positionCount++;
                    lr.SetPosition(0, hit.point);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (isDrawing)
                {
                    if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, 1 << 6))
                    {
                        linePos.Add(hit.point);
                        lr.positionCount++;
                        lr.SetPosition(lr.positionCount - 1, hit.point);

                        DrawingClone();
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDrawing = false;

                if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, 1 << 6))
                {
                    linePos.Add(hit.point);

                    lr.SetPosition(0, linePos[0]);
                    lr.SetPosition(1, linePos[linePos.Count - 1]);

                    lr.positionCount = 2;

                    gravityDir.gravityDir = (linePos[linePos.Count - 1] - linePos[0]);
                    SetLineCol(curLineObj);
                    EndCloneDraw();

                    linePos.Clear();
                }
            }
        }
    }

    //콜라이더 위치 조정하는 함수
    void SetLineCol(GameObject lineObj)
    {
        col = lineObj.GetComponentInChildren<BoxCollider>();

        col.size = new Vector3(3.0f, 1.0f, Vector3.Distance(linePos[0], linePos[linePos.Count - 1]));
        curLineObj.transform.GetChild(0).transform.position = (linePos[linePos.Count - 1] + linePos[0]) / 2;
        col.transform.LookAt(linePos[linePos.Count - 1]);

        col.enabled = true;
    }

    void CreateGravityLineClone()
    {
        for (int i = 0; i < sellHandler.subSells.Count; i++)
        {
            GameObject curCloneLineObj = Instantiate(line);

            curCloneLineObj.GetComponent<LineRenderer>().positionCount = 1;

            cloneGravities.Add(curCloneLineObj);
        }
    }   

    void DrawingClone()
    {
        Vector3 firstPos;

        for(int i = 0; i < cloneGravities.Count; i++)
        {
            LineRenderer line = cloneGravities[i].GetComponent<LineRenderer>();


            line.positionCount++;

            Debug.Log(line.positionCount);
            for (int j = 0; j < linePos.Count; j++)
            {
                Vector3 firstDir = (linePos[j] - sellHandler.mainSell.transform.position).normalized;
                float distance = Vector2.Distance(linePos[j], sellHandler.mainSell.transform.position);
                
                //Debug.Log(distance);

                for (int k = 0; k < sellHandler.subSells.Count; k++)
                {
                    firstPos = sellHandler.subSells[k].transform.position + (firstDir * distance);

                    cloneGravities[k].GetComponent<LineRenderer>().SetPosition(j, firstPos);
                }
            }
        }

        
    }
    void EndCloneDraw()
    {
        for (int i = GameManager.Instance.cloneGravities.Count - 1; i >= 0; i--)
        {
            LineRenderer line = cloneGravities[i].GetComponent<LineRenderer>();

            line.SetPosition(0, line.GetPosition(0));
            line.SetPosition(1, line.GetPosition(line.positionCount - 1));
            line.positionCount = 2;

            SetLineCol(line.gameObject);

            line.GetComponent<GravityDir>().gravityDir = line.GetPosition(line.positionCount - 1) - line.GetPosition(0);

            cloneGravities.Remove(GameManager.Instance.cloneGravities[i]);
        }
    }
}
