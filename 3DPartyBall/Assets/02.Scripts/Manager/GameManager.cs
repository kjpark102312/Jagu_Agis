using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gameClearCount;
    public bool isStageSelect;

    public int nowStageIndex;

    public StageSelectPanel[] stageSelectPanels;

    public GameObject[] players;
    public List<GameObject> gravities = new List<GameObject>();
    public List<GameObject> cloneGravitis = new List<GameObject>();

    public bool isStageClear;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Áßº¹µÈ instance ÀÔ´Ï´Ù.");
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {

        }
        if (Input.GetKeyDown(KeyCode.Menu))
        {

        }
    }
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

    


    public void ClearCheck()
    {
        int playerCount = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].activeSelf == false)
            {
                playerCount++;
                Debug.Log(playerCount);
                if (playerCount == players.Length)
                {
                    Debug.Log("Clear");
                    isStageClear = true;
                    break;
                }
            }
        }
    }
}
