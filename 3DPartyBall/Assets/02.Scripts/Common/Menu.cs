using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [MenuItem("Scene/Title")]
    static void TitleScene()
    {
        if(EditorSceneManager.GetActiveScene().isDirty)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
        EditorSceneManager.OpenScene("Assets/01.Scene/Title.unity");
    }

    [MenuItem("Scene/Main")]
    static void MainScene()
    {
        if (EditorSceneManager.GetActiveScene().isDirty)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
        EditorSceneManager.OpenScene("Assets/01.Scene/Main.unity");
    }

    [MenuItem("Scene/3DTest")]
    static void Test3DScene()
    {
        if (EditorSceneManager.GetActiveScene().isDirty)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
        EditorSceneManager.OpenScene("Assets/01.Scene/3DTest.unity");
    }
}
