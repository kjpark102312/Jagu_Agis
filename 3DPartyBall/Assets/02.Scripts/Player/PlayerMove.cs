using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector]
    public GravityDir curGravityDir;

    protected float moveForce = 15f;

    public Rigidbody2D rb;

    public List<GameObject> cols = new List<GameObject>();

    public bool isGoal;

    Vector2 _touchDir;
    Vector2 gravityDir;

    Vector2 velocity;
    float length = 0.3f;

    bool isOnWall;
    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        length += Time.deltaTime;

        for (int i = 0; i < cols.Count; i++)
        {
            if (cols[i] == null)
                cols.RemoveAt(i);
        }

        if (cols.Count == 1)
            gravityDir = cols[0].GetComponentInParent<GravityDir>().gravityDir.normalized;
        else if (cols.Count >= 2)
        {
            int findIndex = 0;
            for (int i = 0; i < cols.Count; i++)
            {
                int idx = cols[i].GetComponentInParent<GravityDir>().createOrder;

                if (idx >= findIndex)
                {
                    findIndex = idx;
                }
            }

            for (int i = 0; i < cols.Count; i++)
            {
                if (findIndex == cols[i].GetComponentInParent<GravityDir>().createOrder)
                    gravityDir = cols[i].GetComponentInParent<GravityDir>().gravityDir.normalized;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gravity"))
        {
            cols.Insert(0, collision.gameObject);

            velocity = rb.velocity;

            rb.velocity = Vector2.zero;

            rb.gravityScale = 0;

            Vector3 dir = collision.ClosestPoint(transform.position);

            _touchDir = transform.position - dir; // 중력장에 닿았을때 중력장 방향

            //SoundManager.Instance.PlaySFXSound("GravitiFieldUsing");

            Debug.DrawRay(transform.position, _touchDir, Color.black, 4f);

            length = 0.5f;
        }

        if (collision.CompareTag("Obstacle") || collision.CompareTag("Sell"))
        {
            SoundManager.Instance.PlaySFXSound("BumpBall");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") && cols.Count > 0)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (collision.CompareTag("Gravity") && !isGoal)
        {
            rb.velocity = Vector2.Lerp(velocity.normalized * 10, gravityDir * 10, length);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GravityDir col = collision.GetComponentInParent<GravityDir>();
        if (collision.CompareTag("Gravity"))
        {
            col.isTrigger = false;
            rb.gravityScale = 1;

            rb.velocity = gravityDir * 5;

            cols.Remove(collision.gameObject);

        }
    }

}
