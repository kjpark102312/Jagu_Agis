using UnityEngine;

public class PlayerPosCorrection : MonoBehaviour
{

    PlayerMove _playerMove;
    [SerializeField] GravityDir _curGravityDir;
    float speed = 3f;

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (_playerMove.cols.Count > 0)
        {
            _curGravityDir = _playerMove.curGravityDir;


            transform.Translate(new Vector3(transform.position.x, -speed * Time.deltaTime, transform.position.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
