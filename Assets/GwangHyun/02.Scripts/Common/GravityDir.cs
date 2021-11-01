using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDir : MonoBehaviour
{

    private bool isGravity = false;
    private Rigidbody2D rb;
    public float gravityScale = 5f;

    public Vector2 gravityDir;

    private void FixedUpdate()
    {
        if(isGravity)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb = collision.GetComponent<Rigidbody2D>();
        if (collision.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            isGravity = true;
            rb.AddForce(gravityScale * gravityDir);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            rb.gravityScale = 1;
            isGravity = false;
        }
    }
}
