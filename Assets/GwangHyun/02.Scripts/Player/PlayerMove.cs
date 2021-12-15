using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    protected float moveForce = 15f;

    public Rigidbody2D rb;
    protected ConstantForce2D constant;

    public List<GameObject> cols = new List<GameObject>();

    protected PlayerInput input;

    public bool isTrigger;
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        constant = GetComponent<ConstantForce2D>();
    }

    public virtual void Move(Vector3 moveDir)
    {
        rb.AddRelativeForce(moveDir * moveForce, ForceMode2D.Impulse);
    }

    public virtual void Update()
    {
        if(cols.Count > 0)
        {
            constant.force = cols[0].GetComponent<GravityDir>().gravityDir;
            if(cols.Count >= 2)
            {
                for(int i = 0; i < cols.Count; i++)
                {
                    int findIndex = GameManager.Instance.gravities.FindIndex(x => x == cols[i]);

                    Debug.LogWarning(findIndex);

                    if (findIndex == 0)
                    {
                        constant.force = GameManager.Instance.gravities[findIndex].GetComponent<GravityDir>().gravityDir;
                    }
                }
            }
        }
        

        if(cols.Count <= 0 && GameManager.Instance.isStageSelect)
        {
            rb.gravityScale = 1;
            constant.force = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GravityDir col = collision.GetComponent<GravityDir>();
        if (collision.CompareTag("Gravity") && col.isTrigger)
        {
            rb.velocity = Vector2.zero;

            cols.Insert(0, collision.gameObject);

            SoundManager.Instance.PlaySFXSound("InGravity", 5f);
        }
        else if(collision.CompareTag("Map") || collision.CompareTag("obstacle"))
        {
            SoundManager.Instance.PlaySFXSound("dropBall");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Gravity"))
        {
            rb.gravityScale = 0;
            constant.force = collision.GetComponent<GravityDir>().gravityDir;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GravityDir col = collision.GetComponent<GravityDir>();
        if (collision.CompareTag("Gravity"))
        {
            col.isTrigger = false;
            cols.Remove(collision.gameObject);

            SoundManager.Instance.StopSfx();
        }
    }



}
