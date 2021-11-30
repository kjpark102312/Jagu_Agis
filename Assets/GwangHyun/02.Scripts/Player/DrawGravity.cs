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

    public List<Vector3> points = new List<Vector3>();
    public List<Vector2> colPoints = new List<Vector2>();
    public List<GameObject> gravities = new List<GameObject>();

    public Transform mainMap;
    public Transform subMap;

    private int arrowCount;

    public int gravityCount = 5;

    private SellManager sell;

    private void Start()
    {
        sell = GetComponent<SellManager>();
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

                //다른맵 관련
                Vector3 dir = (points[0] - mainMap.position).normalized;
                float distance = Vector2.Distance(points[0], mainMap.position);

                Vector2 pos = subMap.position + (dir * distance);

            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);

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
                arrowCount = Mathf.RoundToInt((distance * distance) / 400);
                Debug.Log(distance * distance);

                if(distance == 0 & distance < 2)
                {
                    //중력장이 너무 짧음
                    Destroy(gravities[gravities.Count - 1]);
                    gravities.RemoveAt(gravities.Count - 1);
                }

                DrawArrow(points[0]);

                points.Clear();

                gravityCount--;
            }
        }
    }

    void DrawArrow(Vector3 LineTr)
    {
        for (int i = 0; i < arrowCount; i++)
        {
            int space =  i + 1;
            
            GameObject clone = Instantiate(arrowPrefab, LineTr + GD.gravityDir.normalized * space, Obstaclerotate(GD.gravityDir));
        }
    }
    
    public Quaternion Obstaclerotate(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
