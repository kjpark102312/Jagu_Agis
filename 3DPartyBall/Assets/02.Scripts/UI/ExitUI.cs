using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitUI : MonoBehaviour
{
    [SerializeField] Button exitButton;
    [SerializeField] Button returnButton;
    [SerializeField] GameObject parentButton;

    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            //³ª°¡±â
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            PlayerPrefs.DeleteAll();
            Application.Quit();
        });

        returnButton.onClick.AddListener(() =>
        {
            parentButton.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}
