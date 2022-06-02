using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMiddleCol : MonoBehaviour
{

    [SerializeField] BoxCollider _col = null;

    DrawGravityLine _gravityLine;

    GameObject _curLineObj;
    void Start()
    {
        _gravityLine = FindObjectOfType<DrawGravityLine>();
    }

    // Update is called once per frame

    private void Update()
    {
        _curLineObj = _gravityLine.curLineObj;
    }

    public void SetCol(GameObject curLineObj, List<Vector3> linePos)
    {
        _col.size = new Vector3(0.01f, 0.01f, Vector3.Distance(linePos[0], linePos[linePos.Count - 1]));
        _curLineObj.transform.GetChild(1).transform.position = (linePos[linePos.Count - 1] + linePos[0]) / 2;
        _col.transform.LookAt(linePos[linePos.Count - 1]);

        _col.enabled = true;    
    }

    public void SetCloneCol(GameObject curLineObj, List<Vector3> linePos)
    {

        for (int i = 0; i < _gravityLine.cloneGravities.Count; i++)
        {

        }

        _col.size = new Vector3(0.01f, 0.01f, Vector3.Distance(linePos[0], linePos[linePos.Count - 1]));
        _curLineObj.transform.GetChild(1).transform.position = (linePos[linePos.Count - 1] + linePos[0]) / 2;
        _col.transform.LookAt(linePos[linePos.Count - 1]);

        _col.enabled = true;
    }

}
