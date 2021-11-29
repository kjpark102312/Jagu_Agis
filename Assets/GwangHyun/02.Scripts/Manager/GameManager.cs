using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gameClearCount;
    public bool isStageSelect;

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("instance°¡ nullÀÔ´Ï´Ù.");

                return null;
            }

            return instance;
        }

        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Áßº¹µÈ instance ÀÔ´Ï´Ù.");
            Destroy(this);
            return;
        }

        Instance = this;
    }
}
