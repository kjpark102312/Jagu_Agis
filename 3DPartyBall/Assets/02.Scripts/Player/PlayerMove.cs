using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector]
    public GravityDir curGravityDir;

    protected float moveForce = 15f;

    public Rigidbody rb;

    public List<GameObject> cols = new List<GameObject>();

    public bool isGoal;

    Vector3 _touchDir;
    Vector3 gravityDir;

    Vector3 velocity;
    float length = 0.3f;

    bool isOnWall;
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Update()
    {
        length += Time.deltaTime;

        for (int i = 0; i < cols.Count; i++)
        {
            if (cols[i] == null)
                cols.RemoveAt(i);
        }

        if (cols.Count == 1)
            gravityDir = cols[0].GetComponentInParent<GravityDir>().gravityDir.normalized;
        else if (cols.Count >= 2)
        {
            int findIndex = 0;
            for (int i = 0; i < cols.Count; i++)
            {
                int idx = cols[i].GetComponentInParent<GravityDir>().createOrder;

                if (idx >= findIndex)
                {
                    findIndex = idx;
                }
            }

            for (int i = 0; i < cols.Count; i++)
            {
                if (findIndex == cols[i].GetComponentInParent<GravityDir>().createOrder)
                    gravityDir = cols[i].GetComponentInParent<GravityDir>().gravityDir.normalized;
            }
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gravity"))
        {
            cols.Insert(0, other.gameObject);

            

            velocity = rb.velocity;

            rb.velocity = Vector3.zero;

            rb.useGravity = false;

            Vector3 dir = other.ClosestPoint(transform.position);

            _touchDir = transform.position - dir; // 중력장에 닿았을때 중력장 방향

            Debug.DrawRay(transform.position, _touchDir, Color.red, 4f);

            length = 0.5f;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Gravity") && !isGoal)
        {
            rb.velocity = Vector3.Lerp(velocity.normalized * 10, gravityDir * 10, length);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GravityDir col = other.GetComponentInParent<GravityDir>();
        if (other.CompareTag("Gravity"))
        {
            col.isTrigger = false;
            rb.useGravity = true;

            cols.Remove(other.gameObject);

        }
    }
}
