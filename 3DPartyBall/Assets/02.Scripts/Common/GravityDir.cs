using UnityEngine;

public class GravityDir : MonoBehaviour
{
    public float gravityScale = 1f;

    public Vector3 gravityDir;
    public BoxCollider cols;

    public int createOrder;
    public bool isTrigger { get; set; }

    private void Start()
    {
        cols = GetComponent<BoxCollider>();
    }
}
