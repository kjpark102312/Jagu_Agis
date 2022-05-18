using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDir : MonoBehaviour
{
    private Rigidbody2D rb;
    public float gravityScale = 1f;

    public Vector3 gravityDir;
    public BoxCollider cols;

    public int createOrder;
    public bool isTrigger { get; set; }

    private void Start()
    {
        cols = GetComponent<BoxCollider>();
    }
}
