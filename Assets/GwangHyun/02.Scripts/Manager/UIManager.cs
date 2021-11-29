using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public DrawGravity drawGravity;
    [SerializeField]
    private Text gravityCountText;


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

    public void gravityCountUpdate()
    {
        gravityCountText.text = $"{drawGravity.gravityCount}°³";
    }
}
