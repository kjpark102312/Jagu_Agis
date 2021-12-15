using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    public bool isSelect;

    [SerializeField]
    private GameObject selectPanel;

    private Transform mainSell;

    private RectTransform rect;

    private PlayerInput playerInput;

    public GameObject outline;

    private void Start()
    {
        rect = selectPanel.GetComponent<RectTransform>();
        playerInput = FindObjectOfType<PlayerInput>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 100f, 1 << 6);

            if (hit && !GameManager.Instance.isStageSelect)
            {
                selectPanel.SetActive(true);
                outline.SetActive(true);

                rect.position = Camera.main.WorldToScreenPoint(hit.transform.gameObject.transform.position);
                outline.transform.SetParent(hit.transform.GetChild(hit.transform.childCount - 1).gameObject.transform);
                outline.transform.localPosition = new Vector3(0, 0, 0);
                mainSell = hit.transform;

                float shortdis = Vector2.Distance(hit.point, GameManager.Instance.players[0].transform.position);

                playerInput.mainPlayer = GameManager.Instance.players[0];

                for (int i = 0; i < GameManager.Instance.players.Length; i++)
                {
                    playerInput.subPlayers.Add(GameManager.Instance.players[i]);
                    float distance = Vector2.Distance(hit.point, GameManager.Instance.players[i].transform.position);

                    if (distance < shortdis)
                    {
                        playerInput.mainPlayer = GameManager.Instance.players[i];
                        playerInput.subPlayers.Remove(GameManager.Instance.players[i]);
                    }
                }
            }
        }
    }

    public void SelectSell()
    {
        GameManager.Instance.isStageSelect = true;

        EventSystem.current.currentSelectedGameObject.SetActive(false);

        GameObject[] stages = GameObject.FindGameObjectsWithTag("Map");

        DrawGravity drawGravity = GetComponent<DrawGravity>();

        drawGravity.mainMap = mainSell;
        outline.SetActive(false);

        for (int i = 0; i < stages.Length; i++)
        {
            drawGravity.subMap.Add(stages[i].transform);
        }

        for (int i = 0; i < stages.Length; i++)
        {
            drawGravity.subMap.Remove(drawGravity.mainMap);
        }
    }
}