using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    protected float moveForce = 15f;

    public Rigidbody rb;
    protected ConstantForce constant;

    public List<GameObject> cols = new List<GameObject>();
    public bool isTrigger;

    [HideInInspector]
    public GravityDir curGravityDir;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        constant = GetComponent<ConstantForce>();
    }

    public virtual void Update()
    {
        if(cols.Count == 1)
        {
            curGravityDir = cols[0].GetComponentInParent<GravityDir>();
            constant.force = curGravityDir.gravityDir;
        }
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

                Debug.Log(findIndex);


                curGravityDir = GameManager.Instance.gravities[findIndex - 1].GetComponentInParent<GravityDir>();
                constant.force = curGravityDir.gravityDir;
            }
        }
    }   

    private void OnTriggerEnter(Collider other)
    {
        GravityDir col = other.GetComponentInParent<GravityDir>();
        if (other.CompareTag("Gravity"))
        {
            rb.velocity = Vector2.zero;

            cols.Insert(0, other.gameObject);


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
            constant.force = other.GetComponentInParent<GravityDir>().gravityDir;
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

            //SoundManager.Instance.StopSfx();
        }
    }
}
