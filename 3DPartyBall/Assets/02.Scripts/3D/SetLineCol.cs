using UnityEngine;

public class SetLineCol : MonoBehaviour
{
    BoxCollider col;

    void Start()
    {
        col = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
