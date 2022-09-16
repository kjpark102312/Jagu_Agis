using UnityEngine;

public class Sell : MonoBehaviour
{
    // ºø √£±‚¿ß«— ∫Û Ω∫≈©∏≥∆Æ
    public Material normalMat;
    public Material outlineMat;

    private void Start()
    {
        normalMat = Resources.Load<Material>("Mat/Normal");
        outlineMat = Resources.Load<Material>("Mat/Solid_Contour_inside");
    }
}
