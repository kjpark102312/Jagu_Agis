using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMove pM;

    private bool isPlaying = true;

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
                Vector3 moveDir = inputPos - transform.position;


                Debug.Log(moveDir);
                pM.Move(moveDir);

                isPlaying = false;
            }
        }
    }

    
}
