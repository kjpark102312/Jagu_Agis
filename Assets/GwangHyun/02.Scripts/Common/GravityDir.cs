using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDir : MonoBehaviour
{
    private Rigidbody2D rb;
    public float gravityScale = 1f;

    public Vector3 gravityDir;
    private EdgeCollider2D cols;
    private ConstantForce2D constant;


    private void Start()
    {
        cols = GetComponent<EdgeCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb = collision.GetComponent<Rigidbody2D>();
        constant = collision.GetComponent<ConstantForce2D>();

        if (collision.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;

            constant.force = gravityDir;
            collision.GetComponent<PlayerMove>().cols.Insert(0, cols);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            constant.force = gravityDir;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            rb.gravityScale = 1;
            constant.force = Vector2.zero;
            collision.GetComponent<PlayerMove>().cols.Remove(cols);
        }
    }
}
