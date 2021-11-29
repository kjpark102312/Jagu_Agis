using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    protected float moveForce = 15f;

    protected Rigidbody2D rb;
    protected ConstantForce2D constant;

    public List<GameObject> cols = new List<GameObject>();

    protected PlayerInput input;
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
            Debug.Log(cols[0].GetComponent<GravityDir>().gravityDir);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gravity"))
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;

            cols.Insert(0, collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Gravity"))
        {
            constant.force = collision.GetComponent<GravityDir>().gravityDir;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Gravity"))
        {
            rb.gravityScale = 1;
            constant.force = Vector2.zero;
            cols.Remove(collision.gameObject);
        }
    }

}
