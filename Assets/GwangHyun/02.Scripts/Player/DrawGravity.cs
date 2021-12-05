using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGravity : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject arrowPrefab;

    private GravityDir GD;

    private LineRenderer lr;
    private EdgeCollider2D col;

    public List<Vector2> points = new List<Vector2>();
    public List<Vector2> colPoints = new List<Vector2>();
    public List<GameObject> gravities = new List<GameObject>();
    public List<GameObject> cloneGravities = new List<GameObject>();

    public Transform mainMap;
    public Transform[] subMap;

    private int arrowCount;

    public int MaxGravityCount = 5;
    public int gravityCount = 5;

    private int drawingPoint = -1;
    private int mapPosPoint = -1;

    private void Start()
    {
        
    }

    void Update()
    {
        if(GameManager.Instance.isStageSelect && gravityCount > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {

                GameObject line = Instantiate(linePrefab);
                GD = line.GetComponent<GravityDir>();
                lr = line.GetComponent<LineRenderer>();
                col = line.GetComponent<EdgeCollider2D>();
                points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                lr.SetPosition(0, points[0]);
                lr.positionCount = 1;

                col.enabled = false;

                gravities.Add(line);

                CloneGravity();
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);

                DrawCloneGravity();

            }
            else if (Input.GetMouseButtonUp(0))
            {
                lr.positionCount = 2;

                GD.gravityDir = points[points.Count - 1] - points[0];

                col.enabled = true;

                colPoints.Add(points[0]);
                colPoints.Add(points[points.Count - 1]);

                col.SetPoints(colPoints);

                colPoints.Clear();

                lr.SetPosition(0, points[0]);
                lr.SetPosition(1, points[points.Count - 1]);

                float distance = GD.gravityDir.sqrMagnitude;

                //DrawArrow();

                if (distance == 0 & distance < 2)
                {
                    //중력장이 너무 짧음
                    Destroy(gravities[gravities.Count - 1]);
                    gravities.RemoveAt(gravities.Count - 1);
                }

                points.Clear();

                gravityCount--;
            }
        }
    }

    void CloneGravity()
    {
        for (int i = 0; i < subMap.Length; i++)
        {
            GameObject line = Instantiate(linePrefab);

            line.GetComponent<EdgeCollider2D>().enabled = false;
            line.GetComponent<LineRenderer>().positionCount = 1;
            cloneGravities.Add(line);

            Vector2 firstDir = (points[0] - (Vector2)mainMap.position).normalized;
            float distance = Vector2.Distance(points[0], mainMap.position);

            Vector2 firstPos = (Vector2)subMap[i].position + (firstDir * distance);

            cloneGravities[i].GetComponent<LineRenderer>().SetPosition(0, firstPos);

            
        }
    }

    void DrawCloneGravity()
    {
        Debug.LogError(cloneGravities.Count);

        for (int j = 0; j < cloneGravities.Count; j++)
        {
            Debug.LogError("첫 그리기");

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            LineRenderer line = cloneGravities[j].GetComponent<LineRenderer>();

            line.positionCount++;
            line.SetPosition(line.positionCount - 1, pos + DrawingMapPos());
        }
    }

    Vector2 DrawingMapPos()
    {
        if(mapPosPoint < subMap.Length - 1)
        {
            mapPosPoint++;
            Debug.LogWarning(mapPosPoint);
        }

        return SubMapPos(mapPosPoint);
    }

    Vector2 SubMapPos(int i)
    {
        Vector2 firstPos = (Vector2)subMap[i].position + Drawing();

        return firstPos;
    }


    Vector2 Drawing()
    {
        drawingPoint++;
        return OnDraw(drawingPoint);
    }

    Vector2 OnDraw(int i)
    {
        Vector2 firstDir = (points[i] - (Vector2)mainMap.position).normalized;
        float distance = Vector2.Distance(points[i], mainMap.position);

        return firstDir * distance;
    }

    void DrawArrow()
    {
        float dis = Vector2.Distance(points[0], points[1]);

        Debug.LogError(dis);

        for (int i = 0; i < arrowCount; i++)
        {
            int space =  i + 1;
            
            //GameObject clone = Instantiate(arrowPrefab, , Obstaclerotate(GD.gravityDir));

            //중력장 복제 하고나서 이거 고치기.
        }
    }
    
    public Quaternion Obstaclerotate(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
