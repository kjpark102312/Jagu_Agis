using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameClearManager : MonoBehaviour
{
    [SerializeField] GameObject backgroundPanel;
    [SerializeField] GameObject gameClearPanel;

    
    public void GameClear()
    {
        SoundManager.Instance.PlaySFXSound("StageClearSound");
        backgroundPanel.SetActive(true);
        gameClearPanel.transform.DOScale(1, 0.3f);
        GameManager.Instance.IsPause = true;
    }
}
