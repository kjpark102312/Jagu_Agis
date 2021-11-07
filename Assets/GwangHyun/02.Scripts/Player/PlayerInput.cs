using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMove pM;

    private bool isPlaying = true;

    public Vector3 moveDir;

    void Awake()
    {
        pM = GetComponent<PlayerMove>();
    }

    void Update()
    {
        MoveInput();
    }

    void MoveInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(isPlaying)
            {
                Vector3 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                moveDir = inputPos - transform.position;

                pM.Move(moveDir);

                isPlaying = false;
            }
        }
    }

    
}
