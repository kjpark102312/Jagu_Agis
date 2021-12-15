using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);

            GameManager.Instance.ClearCheck();
            StageClearManager.Instance.StageClear();

            Debug.Log("이거 몇번도노 ~");
        }
    }
}
