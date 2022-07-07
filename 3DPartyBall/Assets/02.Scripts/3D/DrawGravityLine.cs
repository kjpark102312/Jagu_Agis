using System.Collections.Generic;
using UnityEngine;

public class DrawGravityLine : MonoBehaviour
{
    [SerializeField] GameObject line;

    [HideInInspector]
    public List<Vector3> linePos = new List<Vector3>();

    public List<GameObject> gravities = new List<GameObject>();
    public List<GameObject> clones = new List<GameObject>();
    public List<GameObject> cloneGravities = new List<GameObject>();

    private bool isDrawing = false;

    public GameObject curLineObj;

    public int _drawingCount = 5;

    SellHandler sellHandler;

    GravityDir gravityDir;

    LineRenderer lr;
    BoxCollider col;
    InGameUI uiUpdater;
    StageInfo stageInfo;


    void Start()
    {
        sellHandler = FindObjectOfType<SellHandler>();
        uiUpdater = FindObjectOfType<InGameUI>();
        stageInfo = FindObjectOfType<StageInfo>();

        _drawingCount = stageInfo._gravityCount;

        uiUpdater.UpdateLineCount(_drawingCount);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (GameManager.Instance.isStageSelect && !GameManager.Instance.IsPause && _drawingCount > 0)
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

                    gravities.Add(curLineObj);
                    linePos.Add(hit.point);

                    lr.positionCount = 1;
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
                    _drawingCount--;
                    linePos.Add(hit.point);

                    lr.SetPosition(0, linePos[0]);
                    lr.SetPosition(1, linePos[linePos.Count - 1]);

                    lr.positionCount = 2;

                    gravityDir.gravityDir = (linePos[linePos.Count - 1] - linePos[0]);

                    lr.GetComponent<GravityDir>().createOrder = gravities.Count;

                    SetLineCol(curLineObj);
                    SetCloneCol();

                    EndCloneDraw();

                    GravityLengthCheck();
                    GravityPositionCheck();
                    GravityCountCheck();

                    linePos.Clear();

                    SoundManager.Instance.PlaySFXSound("GravityFieldMaking");
                    
                    uiUpdater.UpdateLineCount(_drawingCount);
                }
            }
        }
    }

    //�ݶ��̴� ��ġ �����ϴ� �Լ�
    void SetLineCol(GameObject lineObj)
    {
        col = lineObj.GetComponentInChildren<BoxCollider>();

        col.size = new Vector3(3.0f, 1.0f, Vector3.Distance(linePos[0], linePos[linePos.Count - 1]));
        curLineObj.transform.GetChild(0).transform.position = (linePos[linePos.Count - 1] + linePos[0]) / 2;
        col.transform.LookAt(linePos[linePos.Count - 1]);

        GameManager.Instance.gravities.Add(lineObj);

        col.enabled = true;
    }


    void SetCloneCol()
    {
        for (int i = 0; i < cloneGravities.Count; i++)
        {
            col = cloneGravities[i].GetComponentInChildren<BoxCollider>();

            LineRenderer line = cloneGravities[i].GetComponent<LineRenderer>();

            line.SetPosition(0, line.GetPosition(0));
            line.SetPosition(1, line.GetPosition(line.positionCount - 1));

            col.size = new Vector3(3.0f, 1.0f, Vector3.Distance
                (line.GetPosition(0), line.GetPosition(line.positionCount - 1)));

            cloneGravities[i].transform.GetChild(0).transform.position =
                (line.GetPosition(line.positionCount - 1) + line.GetPosition(0)) / 2;

            col.transform.LookAt(line.GetPosition(line.positionCount - 1));

            col.enabled = true;
        }
    }

    void CreateGravityLineClone()
    {
        for (int i = 0; i < sellHandler.subSells.Count; i++)
        {
            GameObject curCloneLineObj = Instantiate(line);

            curCloneLineObj.GetComponent<LineRenderer>().positionCount = 1;

            cloneGravities.Add(curCloneLineObj);
            clones.Add(curCloneLineObj);
        }
    }

    void DrawingClone()
    {
        Vector3 firstPos;

        for (int i = 0; i < cloneGravities.Count; i++)
        {
            LineRenderer line = cloneGravities[i].GetComponent<LineRenderer>();

            line.positionCount++;

            for (int j = 0; j < linePos.Count; j++)
            {
                Vector3 firstDir = (linePos[j] - sellHandler.mainSell.transform.position).normalized;
                float distance = Vector2.Distance(linePos[j], sellHandler.mainSell.transform.position);

                for (int k = 0; k < sellHandler.subSells.Count; k++)
                {
                    firstPos = sellHandler.subSells[k].transform.position + (firstDir * distance);

                    if (j > cloneGravities[k].GetComponent<LineRenderer>().positionCount)
                        return;

                    cloneGravities[k].GetComponent<LineRenderer>().SetPosition(j, firstPos);
                }
            }
        }
    }

    

    void EndCloneDraw()
    {
        for (int i = 0; i < cloneGravities.Count; i++)
        {
            LineRenderer line = cloneGravities[i].GetComponent<LineRenderer>();

            line.SetPosition(0, line.GetPosition(0));
            line.SetPosition(1, line.GetPosition(line.positionCount - 1));

            line.positionCount = 2;

            line.GetComponent<GravityDir>().gravityDir = line.GetPosition(line.positionCount - 1) - line.GetPosition(0);

            GameManager.Instance.cloneGravities.Add(line.gameObject);

            line.GetComponent<GravityDir>().createOrder = cloneGravities.Count;
        }

        cloneGravities.Clear();
    }

    void GravityLengthCheck()
    {
        if (Vector3.Distance(linePos[0], linePos[linePos.Count - 1]) < 1.5f)
        {
            Debug.Log("�߷����� �ʹ� ª���ϴ�");
            uiUpdater.WarningText(0);
            ReMoveGravityLine(GameManager.Instance.gravities.Count - 1);

            _drawingCount++;
        }
    }

    void GravityCountCheck()
    {
        if (gravities.Count > 3)
        {
            ReMoveGravityLine(0);
        }
    }


    void GravityPositionCheck()
    {
        if(stageInfo.isTwoSell)
        {
            if (Vector3.Distance(linePos[0], GameManager.Instance.mainSell.transform.position) >= 15f)
            {
                Debug.Log("���μ� ���� �߷����� �׷��ּ���");

                ReMoveGravityLine(GameManager.Instance.gravities.Count - 1);
                uiUpdater.WarningText(1);
                _drawingCount++;
            }
        }
        if(stageInfo.isFourSell)
        {
            if (Vector3.Distance(linePos[0], GameManager.Instance.mainSell.transform.position) >= 8f)
            {
                Debug.Log("���μ� ���� �߷����� �׷��ּ���");

                ReMoveGravityLine(GameManager.Instance.gravities.Count - 1);
                uiUpdater.WarningText(1);
                _drawingCount++;
            }
        }
        
    }

    void ReMoveGravityLine(int idx)
    {
        Destroy(gravities[idx]);
        gravities.RemoveAt(idx);
        GameManager.Instance.gravities.RemoveAt(idx);

        if(clones.Count > 0)
        {
            Destroy(clones[idx]);
            clones.RemoveAt(idx);
            GameManager.Instance.cloneGravities.RemoveAt(idx);
        }
    }
}
