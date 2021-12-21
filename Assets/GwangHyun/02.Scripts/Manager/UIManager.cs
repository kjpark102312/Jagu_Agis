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

    [SerializeField]
    private Text warningTxt;


    [Header("�ɼǰ���")]
    [SerializeField]
    private GameObject OptionPanel;
    private bool isOnPanel = false;

    [Header("���� ����")]
    [SerializeField]
    private GameObject soundManager;
    private bool isOnSound = true;

    public GameObject Soundbtn;

    public Sprite[] onoffSound;

    private void Start()
    {
        soundManager = GameObject.Find("SoundManager");
    }

    private void Update()
    {
        gravityCountUpdate();
    }

    public void ReStart()
    {
        GameManager.Instance.LoadScene(GameManager.Instance.nowStageIndex);
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

        //SoundManager.Instance.PlaySFXSound()
    }

    public void gravityCountUpdate()
    {
        gravityCountText.text = $"{drawGravity.gravityCount}";
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene("Title");
    }

    public void SoundBtn()
    {
        isOnSound = !isOnSound;
        soundManager.SetActive(isOnSound);

        if(isOnSound == true)
        {
            Soundbtn.GetComponent<Image>().sprite = onoffSound[0];
            Debug.Log(Soundbtn.GetComponent<Image>().sprite.name);
        }
        else if(isOnSound == false)
        {
            Soundbtn.GetComponent<Image>().sprite = onoffSound[1];
            Debug.Log(Soundbtn.GetComponent<Image>().sprite.name);
        }
    }
}





