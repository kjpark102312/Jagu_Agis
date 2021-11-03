using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDir : MonoBehaviour
{
    private Rigidbody2D rb;
    public float gravityScale = 5f;

    public Vector2 gravityDir;
    private EdgeCollider2D cols;


    private void Start()
    {
        cols = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb = collision.GetComponent<Rigidbody2D>();
        if (collision.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            rb.AddForce(gravityScale * gravityDir);
            collision.GetComponent<PlayerMove>().cols.Insert(0, cols);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            rb.gravityScale = 1;
            collision.GetComponent<PlayerMove>().cols.Remove(cols);
        }
    }
}
