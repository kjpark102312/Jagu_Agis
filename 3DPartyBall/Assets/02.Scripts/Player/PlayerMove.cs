using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    protected float moveForce = 15f;

    public Rigidbody rb;
    protected ConstantForce constant;

    public List<GameObject> cols = new List<GameObject>();
    public bool isTrigger;

    [HideInInspector]
    public GravityDir curGravityDir;

    Vector3 _touchDir;
    Vector3 gravityDir;

    Vector3 velocity;
    public float length = 0.3f;
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        constant = GetComponent<ConstantForce>();
    }

    public virtual void Update()
    {
        length += Time.deltaTime;

        Debug.LogError(gravityDir);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gravity"))
        {
            cols.Insert(0, other.gameObject);

            if (cols.Count == 1)
                gravityDir = other.GetComponentInParent<GravityDir>().gravityDir.normalized;
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

            Debug.Log("asd");
            velocity = rb.velocity;

            rb.velocity = Vector3.zero;

            Vector3 dir = other.ClosestPoint(transform.position);

            _touchDir = transform.position - dir; // 중력장에 닿았을때 중력장 방향

            Debug.DrawRay(transform.position, _touchDir, Color.red, 4f);

            length = 0.5f;
            

            //SoundManager.Instance.PlaySFXSound("InGravity", 5f);
        }
        else if (other.CompareTag("Map") || other.CompareTag("obstacle"))
        {
            //SoundManager.Instance.PlaySFXSound("dropBall");
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Gravity"))
        {
            rb.useGravity = false;

            rb.velocity = Vector3.Lerp(velocity, gravityDir * 10, length);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GravityDir col = other.GetComponentInParent<GravityDir>();
        if (other.CompareTag("Gravity"))
        {
            col.isTrigger = false;
            rb.useGravity = true;
            constant.force = Vector3.zero;

            cols.Remove(other.gameObject);

            SoundManager.Instance.StopSfx();
        }
    }
}
