using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMove pM;
    private List<OtherPlayer> others = new List<OtherPlayer>();
    private bool isPlaying = true;

    public Vector3 moveDir;


    public GameObject mainPlayer;
    public List<GameObject> subPlayers = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < subPlayers.Count; i++)
        {
            others.Add(subPlayers[i].GetComponent<OtherPlayer>());
        }
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
                Time.timeScale = 1;

                Vector3 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                moveDir = (inputPos - mainPlayer.transform.position).normalized;

                mainPlayer.GetComponent<PlayerMove>().Move(moveDir);

                for(int i = 0; i < subPlayers.Count; i++)
                {
                    subPlayers[i].GetComponent<PlayerMove>().Move(moveDir);
                }

                isPlaying = false;
            }
        }
    }
}
