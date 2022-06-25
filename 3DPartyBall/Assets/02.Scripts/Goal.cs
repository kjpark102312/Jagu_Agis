using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject master;

    bool isGoal = false;
    float timer;
    Vector3 curPos;

    Vector3 posB;
    Vector3 posC;

    private void Start()
    {
        posB = new Vector3(transform.position.x - 2f, transform.position.y - 0.2f, 14.5f);
        posC = new Vector3(transform.position.x + 2f, transform.position.y - 0.2f, 14.5f);
    }

    IEnumerator DrawTrajectory()
    {
        yield return new WaitForSeconds(0.5f);

        while (isGoal)
        {
            timer += Time.deltaTime;

            master.transform.position = FourPointBezier(curPos, posB, posC, transform.position, timer);

            yield return null;
        }
    }

    private Vector3 FourPointBezier(Vector3 p_1, Vector3 p_2, Vector3 p_3, Vector3 p_4, float value)
    {
        Vector3 A = Vector3.Lerp(p_1, p_2, value);
        Vector3 B = Vector3.Lerp(p_2, p_3, value);
        Vector3 C = Vector3.Lerp(p_3, p_4, value);

        Vector3 D = Vector3.Lerp(A, B, value);
        Vector3 E = Vector3.Lerp(B, C, value);

        Vector3 F = Vector3.Lerp(D, E, value);
        return F;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isGoal = true;
            master = other.gameObject;
            curPos = master.transform.position;

            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<PlayerMove>().isGoal = true;

            StartCoroutine(DrawTrajectory());
        }
    }
}
