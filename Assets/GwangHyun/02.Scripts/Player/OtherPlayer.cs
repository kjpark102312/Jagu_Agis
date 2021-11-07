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
        base.Move(moveDir);
        Debug.Log("¾¾¹ß!");
    }

    public override void Update()
    {
        base.Update();
    }
}
