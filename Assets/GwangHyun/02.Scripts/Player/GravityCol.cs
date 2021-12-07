using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCol : MonoBehaviour
{ 
    void Start()
    { 

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GravityDir col = collision.GetComponent<GravityDir>();
        if (collision.CompareTag("Gravity"))
            col.isTrigger = true;
    }
}
