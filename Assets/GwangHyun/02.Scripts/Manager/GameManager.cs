using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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

    private void Start()
    {
    }

    public void LoadScene(int mapindex)
    {
        StartCoroutine(LoadSceneCo(mapindex));
    }

    public IEnumerator LoadSceneCo(int mapindex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(mapindex);

        operation.allowSceneActivation = false;
        float timer = 0f;
        while (true)
        {
            yield return null;
            timer += Time.deltaTime;

            Debug.Log(operation.allowSceneActivation);
            if (!operation.isDone)
            {
                if (operation.progress < 0.9f)
                {

                }
                else
                {

                    operation.allowSceneActivation = true;
                }
            }

            if (operation.isDone)
            {
                operation.allowSceneActivation = true;

                players = GameObject.FindGameObjectsWithTag("Player");

                GameObject map = Instantiate(MapManager.Instance.mapList[mapindex-1]);

                for (int i = 0; i < GameManager.Instance.players.Length; i++)
                {
                    players[i].GetComponent<PlayerMove>().rb.gravityScale = 0;
                }

                yield break;
            }
        }
    }
}
