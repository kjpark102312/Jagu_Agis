using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosCorrection : MonoBehaviour
{

    PlayerMove _playerMove;
    [SerializeField] GravityDir _curGravityDir;

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {

        if(_playerMove.cols.Count > 0)
        {
            _curGravityDir = _playerMove.curGravityDir;

            //transform.position = new Vector3();
        }
    }


}
