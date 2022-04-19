using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGravityLine : MonoBehaviour
{
    [SerializeField] GameObject line;

    public List<Vector3> linePos = new List<Vector3>();

    public Camera mainCamera;

    private bool isDrawing = false;

    GravityDir gravityDir;

    LineRenderer lr;
    BoxCollider col;
    GameObject curLineObj;

    void Start()
    {
        mainCamera = Camera.main;

    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, 1 << 9))
            {
                isDrawing = true;
                curLineObj = Instantiate(line);

                lr = curLineObj.GetComponent<LineRenderer>();
                gravityDir = curLineObj.GetComponent<GravityDir>();

                linePos.Add(hit.point);

                lr.positionCount++;
                lr.SetPosition(0, hit.point);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if(isDrawing)
            {
                if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, 1 << 9))
                {
                    linePos.Add(hit.point);
                    lr.positionCount++;
                    lr.SetPosition(lr.positionCount - 1, hit.point);

                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            
            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, 1 << 9))
            {
                linePos.Add(hit.point);

                lr.SetPosition(0, linePos[0]);
                lr.SetPosition(1, linePos[linePos.Count - 1]);

                lr.positionCount = 2;

                gravityDir.gravityDir = (linePos[linePos.Count - 1] - linePos[0]);
                SetLineCol();

                linePos.Clear();
            }
        }
    }

    void SetLineCol()
    {
        col = curLineObj.GetComponentInChildren<BoxCollider>();

        col.size = new Vector3(3.0f, 1.0f, Vector3.Distance(linePos[0], linePos[linePos.Count - 1]));
        curLineObj.transform.GetChild(0).transform.position = (linePos[linePos.Count - 1] + linePos[0]) / 2;
        col.transform.LookAt(linePos[linePos.Count - 1]);
    }
}
