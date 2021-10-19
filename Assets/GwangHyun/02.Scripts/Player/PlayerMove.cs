using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float moveForce = 30f;

    private Vector3 inDir;

    private Collider2D item;

    private Rigidbody2D rb;
    private Rigidbody2D otherRb;

    [SerializeField]
    private GameObject otherPlayer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        otherRb = otherPlayer.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector3 moveDir)
    {
        rb.AddRelativeForce(moveDir * moveForce);
        otherRb.AddRelativeForce(moveDir * moveForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
