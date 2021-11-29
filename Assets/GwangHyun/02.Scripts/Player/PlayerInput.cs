using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMove pM;

    private bool isPlaying = true;

    public Vector3 moveDir;

    public OtherPlayer otherPlayer;

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
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.isStageSelect)
        {
            if(isPlaying)
            {
                Vector3 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                moveDir = (inputPos - transform.position).normalized;

                pM.Move(moveDir);
                otherPlayer.Move(moveDir);

                isPlaying = false;
            }
        }
    }

    
}
