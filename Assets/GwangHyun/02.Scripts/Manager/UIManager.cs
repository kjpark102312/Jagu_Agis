using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{

    public DrawGravity drawGravity;
    [SerializeField]
    private Text gravityCountText;


    [Header("옵션관련")]
    [SerializeField]
    private GameObject OptionPanel;
    private bool isOnPanel = false;

    private void Update()
    {
        gravityCountUpdate();
    }
    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SelectMainMap()
    {

    }

    public void OptionBtn()
    {
        if(isOnPanel == false)
        {
            OptionPanel.transform.DOScaleX(1f, 0.4f);

            for(int i = 0; i < OptionPanel.transform.childCount; i++)
            {
                OptionPanel.transform.GetChild(i).gameObject.SetActive(true);
            }
        }    
        else
        {
            OptionPanel.transform.DOScaleX(0.35f, 0.4f);
            for (int i = 0; i < OptionPanel.transform.childCount; i++)
            {
                OptionPanel.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        isOnPanel = !isOnPanel;

    }

    public void gravityCountUpdate()
    {
        gravityCountText.text = $"{drawGravity.gravityCount}개";
    }
}
