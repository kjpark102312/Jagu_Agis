using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGravity : MonoBehaviour
{
    public GameObject linePrefab;

    private GravityDir GD;

    private LineRenderer lr;
    private EdgeCollider2D col;

    public List<Vector2> points = new List<Vector2>();

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject line = Instantiate(linePrefab);
            GD = line.GetComponent<GravityDir>();
            lr = line.GetComponent<LineRenderer>();
            col = line.GetComponent<EdgeCollider2D>();
            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lr.SetPosition(0, points[0]);
            lr.positionCount = 1;
        }
        else if(Input.GetMouseButton(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            points.Add(pos);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, pos);
            col.points = points.ToArray();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            lr.positionCount = 2;

            GD.gravityDir = points[points.Count - 1] - points[0];

            lr.SetPosition(0, points[0]);
            lr.SetPosition(1, points[points.Count - 1]);

            points.Clear();
        }
    }
}
