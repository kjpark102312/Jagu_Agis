using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameClearManager : MonoBehaviour
{
    public GameObject[] playerBalls;

    [SerializeField] GameObject backgroundPanel;
    [SerializeField] GameObject gameClearPanel;


    void Awake()
    {
        //playerBalls = t
    }

    public void GameClearCheck()
    {
        int ballCount = 0;
        for(int i = 0; i < playerBalls.Length; i++)
        {
            if (!playerBalls[i].activeSelf)
                ballCount++;
        }

        if (ballCount == playerBalls.Length)
        {
            backgroundPanel.SetActive(true);
            gameClearPanel.transform.DOScale(1, 0.3f);
            GameManager.Instance.IsPause = false;
        }
    }
}
