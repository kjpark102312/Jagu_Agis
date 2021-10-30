using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float moveSpeed = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(h, v);
        Vector2 movement = inputVector * moveSpeed * Time.deltaTime;

        transform.Translate(movement);
    }
}
