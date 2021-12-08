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
    public List<Transform> subMap = new List<Transform>();

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
                GameManager.Instance.gravities.Insert(0, line);

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
                    GameManager.Instance.gravities.RemoveAt(GameManager.Instance.gravities.Count-1);
                }

                points.Clear();

                gravityCount--;
                
                EndDraw();
            }
        }
    }

    void CloneGravity()
    {
        for (int i = 0; i < subMap.Count; i++)
        {
            GameObject line = Instantiate(linePrefab);

            line.GetComponent<EdgeCollider2D>().enabled = false;
            line.GetComponent<LineRenderer>().positionCount = 1;
            cloneGravities.Add(line);
        }
    }

    void DrawCloneGravity()
    {
        Debug.LogError(cloneGravities.Count);

        for (int i = 0; i < cloneGravities.Count; i++)
        {
            LineRenderer line = cloneGravities[i].GetComponent<LineRenderer>();

            line.positionCount++;

            Vector3[] pointArray = new Vector3[points.Count];
            Vector2 firstPos = new Vector2();
            for (int j = 0; j < points.Count; j++)
            {
                for(int k = 0; k < subMap.Count; k++)
                {
                    Vector2 firstDir = (points[j] - (Vector2)mainMap.position).normalized;
                    float distance = Vector2.Distance(points[j], mainMap.position);

                    firstPos = (Vector2)subMap[k].position + (firstDir * distance);
                }
                pointArray[j] = firstPos;
            }
            line.SetPositions(pointArray);
        }
    }

    void EndDraw()
    {
        for (int i = 0; i < cloneGravities.Count; i++)
        {
            LineRenderer line = cloneGravities[i].GetComponent<LineRenderer>();

            line.SetPosition(0, line.GetPosition(0));
            line.SetPosition(1, line.GetPosition(line.positionCount-1));

            line.positionCount = 2;

            cloneGravities.Remove(cloneGravities[i]);
        }
    }


    void DrawArrow()
    {
        float dis = Vector2.Distance(points[0], points[points.Count - 1]);
        Vector2 dir = points[points.Count - 1] - points[0];
        

        arrowCount = Mathf.RoundToInt(dis / 1);

        
        Debug.LogError(dis);
        Debug.LogError(arrowCount);

        for (int i = 0; i < arrowCount; i++)
        {
            float space = ((dis / arrowCount)* 0.2f) * (i + 1);

            Debug.Log(space);

            GameObject clone = Instantiate(arrowPrefab, points[0] + (dir * space), Obstaclerotate(GD.gravityDir));
        }
    }
    
    public Quaternion Obstaclerotate(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
