using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float moveForce = 30f;

    protected Rigidbody2D rb;

    public List<EdgeCollider2D> cols = new List<EdgeCollider2D>();

    protected PlayerInput input;
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
    }

    public virtual void Move(Vector3 moveDir)
    {
        moveDir = input.moveDir;
        rb.AddRelativeForce(moveDir * moveForce);
    }

    public virtual void Update()
    {
        if(cols.Count >= 2)
        {
            rb.AddForce(cols[0].GetComponent<GravityDir>().gravityDir * 5f);
        }
    }
}
