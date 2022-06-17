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
        
    }

    public void CorrectionPlayer(Vector2 dir)
    {
        
    }
}
