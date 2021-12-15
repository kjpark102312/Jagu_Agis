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

    private void Start()
    {

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

    public void LoadScene(int mapindex)
    {
        StartCoroutine(LoadSceneCo(mapindex));
        isStageSelect = false;
    }

    public IEnumerator LoadSceneCo(int mapindex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");

        nowStageIndex = mapindex;

        if(mapindex > 8)
        {
            SceneManager.LoadScene("CommingSoon");
        }

        operation.allowSceneActivation = false;
        float timer = 0f;
        while (true)
        {
            yield return null;
            timer += Time.deltaTime;

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

                GameObject map = Instantiate(MapManager.Instance.mapList[mapindex-1]);

                Debug.Log(mapindex);

                stageSelectPanels = map.GetComponents<StageSelectPanel>();
                players = GameObject.FindGameObjectsWithTag("Player");
                
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].GetComponent<Rigidbody2D>().gravityScale = 0;
                }

                yield break;
            }
        }
    }
}
