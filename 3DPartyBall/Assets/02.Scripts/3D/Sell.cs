using UnityEngine;

public class Sell : MonoBehaviour
{
    // �� ã������ �� ��ũ��Ʈ
    public Material normalMat;
    public Material outlineMat;

    private void Start()
    {
        normalMat = Resources.Load<Material>("Mat/Normal");
        outlineMat = Resources.Load<Material>("Mat/Solid_Contour_inside");
    }
}
