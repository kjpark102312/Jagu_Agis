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
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        constant = GetComponent<ConstantForce>();
    }

    public virtual void Move(Vector3 moveDir)
    {
        //rb.AddRelativeForce(moveDir * moveForce, ForceMode2D.Impulse);
    }

    public virtual void Update()
    {
        if(cols.Count > 0)
        {
            constant.force = cols[0].GetComponentInParent<GravityDir>().gravityDir;
            if(cols.Count >= 2)
            {
                for(int i = 0; i < cols.Count; i++)
                {
                    int findIndex = GameManager.Instance.gravities.FindIndex(x => x == cols[i]);

                    Debug.LogWarning(findIndex);

                    if (findIndex == 0)
                    {
                        constant.force = GameManager.Instance.gravities[findIndex].GetComponentInParent<GravityDir>().gravityDir;
                    }
                }
            }
        }
        

        //if(cols.Count <= 0 && GameManager.Instance.isStageSelect)
        //{
        //    rb.gravityScale = 1;
        //    constant.force = Vector2.zero;
        //}
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
