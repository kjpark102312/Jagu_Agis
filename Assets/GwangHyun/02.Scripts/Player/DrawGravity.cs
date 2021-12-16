using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DrawGravity : MonoBehaviour
{
    public Text[] warningTxt;

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
    
    [System.Serializable]
    public class ArrowsArray
    {
        public List<GameObject> arrows = new List<GameObject>();
    }

    public ArrowsArray[] cloneArrows;


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
                GD.gravityDir = points[points.Count - 1] - points[0];

                col.enabled = true;

                colPoints.Add(points[0]);
                colPoints.Add(points[points.Count - 1]);

                col.SetPoints(colPoints);

                colPoints.Clear();

                lr.SetPosition(0, points[0]);
                lr.SetPosition(1, points[points.Count - 1]);

                lr.positionCount = 2;

                CheckGrvity();
                CheckGravityLength();
                //DrawArrow();
                EndDraw();

                points.Clear();

                gravityCount--;
            }
        }
    }

    void  CheckGravityLength()  
    {
        for(int i= 0; i < points.Count; i++)
        {
            float distance = Vector2.Distance(points[i], mainMap.transform.position);

            if(distance > 6)
            {
                Destroy(gravities[gravities.Count - 1]);
                gravities.RemoveAt(gravities.Count - 1);
                for (int j = cloneGravities.Count - 1; j >= 0; j--)
                {
                    Destroy(cloneGravities[j]);
                    cloneGravities.RemoveAt(j);
                    WarningText(0);
                }
                gravityCount++;
                break;
            }
        }
    }

    void CheckGrvity()
    {
        float distance = Vector2.Distance(points[0], points[points.Count - 1]);

        if (distance < 2)
        {
            Destroy(gravities[gravities.Count - 1]);
            gravities.RemoveAt(gravities.Count - 1);
            for (int i = cloneGravities.Count - 1; i >= 0; i--)
            {
                Destroy(cloneGravities[i]);
                cloneGravities.RemoveAt(i);
                GameManager.Instance.cloneGravitis.RemoveAt(i);
            }
            WarningText(1);
            GameManager.Instance.gravities.RemoveAt(GameManager.Instance.gravities.Count - 1);
            gravityCount++;
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
            GameManager.Instance.cloneGravitis.Add(line);
        }
    }

    void DrawCloneGravity()
    {
        if(gravities.Count > 3)
        {
            Destroy(gravities[0]);
            gravities.RemoveAt(0);
            GameManager.Instance.gravities.RemoveAt(0);
        }

        if(GameManager.Instance.cloneGravitis.Count > 3 * subMap.Count)
        {
            Destroy(GameManager.Instance.cloneGravitis[0]);
            GameManager.Instance.cloneGravitis.RemoveAt(0);
        }

        for (int i = 0; i < cloneGravities.Count; i++)
        {
            LineRenderer line = cloneGravities[i].GetComponent<LineRenderer>();

            line.positionCount++;

            Vector3[] pointArray = new Vector3[points.Count];
            Vector2 firstPos = new Vector2();
            for (int j = 0; j < points.Count; j++)
            {
                Vector2 firstDir = (points[j] - (Vector2)mainMap.position).normalized;
                float distance = Vector2.Distance(points[j], mainMap.position);

                Debug.Log(distance);

                for (int k = 0; k < subMap.Count; k++)
                {
                    firstPos = (Vector2)subMap[k].position + (firstDir * distance);

                    cloneGravities[k].GetComponent<LineRenderer>().SetPosition(j, firstPos);
                }
            }
        }
    }

    public void WarningText(int i)
    {
        warningTxt[i].DOFade(255f, 0.1f);
        warningTxt[i].DOFade(0f, 0.7f);
    }

    void EndDraw()
    {
        for (int i = cloneGravities.Count - 1; i >= 0; i--)
        {
            LineRenderer line = cloneGravities[i].GetComponent<LineRenderer>();
            EdgeCollider2D edge =  line.GetComponent<EdgeCollider2D>();
            List<Vector2> points = new List<Vector2>();

            points.Add(line.GetPosition(0));
            points.Add(line.GetPosition(line.positionCount - 1));

            line.SetPosition(0, line.GetPosition(0));
            line.SetPosition(1, line.GetPosition(line.positionCount-1));
            line.positionCount = 2;

            edge.enabled = true;
            edge.SetPoints(points);

            line.GetComponent<GravityDir>().gravityDir = line.GetPosition(line.positionCount - 1) - line.GetPosition(0);

            cloneGravities.Remove(cloneGravities[i]);

        }
    }

    void DrawArrow()
    {
        float dis = Vector2.Distance(points[0], points[points.Count - 1]);
        Vector2 dir = points[points.Count - 1] - points[0];
        

        arrowCount = Mathf.RoundToInt(dis / 1);

        if(cloneArrows.Length > 3)
        {
            for(int i = 0; i <  cloneArrows[0].arrows.Count; i++)
            {
                Destroy(cloneArrows[0].arrows[i]);
            }
        }

        for (int i = 0; i < arrowCount; i++)
        {
            float space = ((dis / arrowCount)* 0.12f) * (i + 1);

            Debug.Log(space);

            GameObject clone = Instantiate(arrowPrefab, points[0] + (dir * space), Obstaclerotate(GD.gravityDir));

            cloneArrows[0].arrows.Add(clone);
        }
    }
    
    public Quaternion Obstaclerotate(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
