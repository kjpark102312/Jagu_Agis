using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer : PlayerMove
{

    public override void Awake()
    {
        base.Awake();
    }

    public override void Move(Vector3 moveDir)
    {
        rb.AddRelativeForce(moveDir * moveForce, ForceMode2D.Impulse);
        Debug.Log(moveForce);   
    }

    public override void Update()
    {
        base.Update();

    }
}
