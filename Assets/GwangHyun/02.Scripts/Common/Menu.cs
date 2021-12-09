using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [MenuItem("Scene/Main")]
    static void Main()
    {
        SceneManager.LoadScene("Main");
    }
    [MenuItem("Scene/Title")]
    static void Title()
    {
        SceneManager.LoadScene("Title");
    }
}
