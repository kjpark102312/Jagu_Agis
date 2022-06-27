using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class OptionUI : MonoBehaviour
{
    [SerializeField] Button reStartButton = null;
    [SerializeField] Button optionParentButton = null;
    [SerializeField] Button soundButton = null;
    [SerializeField] Button homeButton = null;

    bool isMute = false;

    private void Awake()
    {
        reStartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        optionParentButton.onClick.AddListener(() =>
        {
            //�ɼ��г� ������
        });

        soundButton.onClick.AddListener(() =>
        {
            if(isMute)
            {
                //���� �ѱ�
            }
            else
            {
                //���� ����
            }
        });

        homeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });
    }
}
