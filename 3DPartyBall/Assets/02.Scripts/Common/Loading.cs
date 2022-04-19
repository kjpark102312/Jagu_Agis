using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Loading : MonoBehaviour
{

    public float timer;
    public Slider slider;
    void Start()
    {
        StartCoroutine(LoadAsynSceneCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadAsynSceneCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("InGame");

        operation.allowSceneActivation = false;

        while (true)
        {
            yield return null;
            timer += Time.deltaTime;

            Debug.Log(operation.allowSceneActivation);

            if (!operation.isDone)
            {
                if (operation.progress < 0.9f)
                {
                    slider.value = timer;
                    if (slider.value >= operation.progress)
                    {
                        timer = 0f;
                    }
                }
                else
                {
                    slider.value = timer;
                    if (slider.value == 1)
                    {
                        operation.allowSceneActivation = true;
                    }
                }
            }

            if (operation.isDone)
            {
                operation.allowSceneActivation = true;

                yield break;
            }
        }
    }
}
