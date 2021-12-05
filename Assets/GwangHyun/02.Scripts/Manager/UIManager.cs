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


    [Header("�ɼǰ���")]
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
        }    
        else
        {
            OptionPanel.transform.DOScaleX(0.35f, 0.4f);
        }
        isOnPanel = !isOnPanel;

    }

    public void gravityCountUpdate()
    {
        gravityCountText.text = $"{drawGravity.gravityCount}��";
    }
}
