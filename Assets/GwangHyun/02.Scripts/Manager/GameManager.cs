using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gameClearCount;
    public bool isStageSelect;

    public GameObject[] players;
    public List<GameObject> gravities = new List<GameObject>();


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

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerMove>().rb.gravityScale = 0;
        }
    }
}
