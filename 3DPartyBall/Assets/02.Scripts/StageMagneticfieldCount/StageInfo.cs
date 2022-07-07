using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    public bool isTwoSell;
    public bool isFourSell;

    public int _gravityCount;
    public int _playerCount;
    public int _sellCount;

    public List<GameObject> players = new List<GameObject>();

    GameClearManager gameClearManager;

    void Start()
    {
        gameClearManager = FindObjectOfType<GameClearManager>();
    }
    public void PlayerActiveCheck()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].activeSelf)
            {
                players.RemoveAt(i);
            }
        }

        Debug.Log(players.Count);
        if (players.Count <= 0)
            gameClearManager.GameClear();
    }
}
