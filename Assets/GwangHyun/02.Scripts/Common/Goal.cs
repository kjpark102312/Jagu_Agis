using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private int goalPlayerCount;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {


            goalPlayerCount++;
            collision.gameObject.SetActive(false);
        }
    }
}
