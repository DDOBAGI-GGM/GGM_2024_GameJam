using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weigh_OBJ : MonoBehaviour, IReset
{
    private MeshRenderer renderer;

    [Header("Move")]
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    private int idx = 0;

    [Header("Ray")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;
    private bool isCollision = false;

    [Header("Count")]
    [SerializeField] private int cnt;

    public bool isDown = false;
    public bool IsDown { get { return isDown; } }

    [Header("Reset")]
    [SerializeField] private Vector3 originPos;
    [SerializeField] private bool isInteraction = false;

    private void Awake()
    {
        originPos = transform.position;
    }

    public void Reset()
    {
        if (isInteraction)
        {
            transform.position = originPos;
            isInteraction = false;
        }
    }

    private void Update()
    {
        Move();
        Ray();
    }

    private void Move()
    {
        if (isDown)
        {
            Vector3 target = points[idx].position;
            Vector3 dir = (target - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, target);

            if (distance < 0.5f)
                idx = (idx + 1) % points.Length;
        }
        else
        {
            transform.Translate(Vector3.zero);
        }
    }

    private void Ray()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, layer);

        if (colliders.Length > 0)
        {
            if (!isCollision)
            {
                isCollision = true;

                foreach (Collider collider in colliders)
                {
                    cnt--;

                    if (cnt == 0)
                    {
                        isDown = true;
                    }
                }
            }
        }
        else
        {
            if (isCollision)
            {
                isCollision = false;

                cnt++;

                if (cnt != 0)
                {
                    isDown = false;
                    isInteraction = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
