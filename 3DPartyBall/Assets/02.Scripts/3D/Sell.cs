using UnityEngine;

public class Sell : MonoBehaviour
{
    // ºø √£±‚¿ß«— ∫Û Ω∫≈©∏≥∆Æ
    public Material normalMat;
    public Material outlineMat;

    InGameUI uiUpdater;

    StageInfo stageInfo;

    private void Start()
    {
        uiUpdater = FindObjectOfType<InGameUI>();
        stageInfo = transform.parent.GetComponent<StageInfo>();

        normalMat = Resources.Load<Material>("Mat/Normal");
        outlineMat = Resources.Load<Material>("Mat/Solid_Contour_inside");

        uiUpdater.UpdateLineCount(stageInfo._gravityCount);
    }
}
