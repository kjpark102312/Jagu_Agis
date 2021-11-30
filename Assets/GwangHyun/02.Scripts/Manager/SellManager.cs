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

    private void Start()
    {
        rect = selectPanel.GetComponent<RectTransform>();
        playerInput = FindObjectOfType<PlayerInput>();

        Time.timeScale = 0;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 100f, 1 << 6);

            if (hit && !GameManager.Instance.isStageSelect)
            {
                selectPanel.SetActive(true);
                rect.position = Camera.main.WorldToScreenPoint(hit.transform.gameObject.transform.position);
                mainSell = hit.transform.parent;

                float shortdis = Vector2.Distance(hit.point, GameManager.Instance.players[0].transform.position);

                playerInput.mainPlayer = GameManager.Instance.players[0];

                for (int i = 0; i < GameManager.Instance.players.Length; i++)
                {
                    playerInput.subPlayers.Add(GameManager.Instance.players[i]);
                    float distance = Vector2.Distance(hit.point, GameManager.Instance.players[i].transform.position);

                    if(distance < shortdis)
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

        GetComponent<DrawGravity>().mainMap = mainSell;

    }
}
