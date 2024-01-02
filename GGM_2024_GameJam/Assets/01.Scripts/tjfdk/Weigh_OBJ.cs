using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Weigh_OBJ : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    private int idx = 0;

    [Header("Ray")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;
    private bool isCollision = false;

    [Header("Position")]
    [SerializeField] private float upPos;
    [SerializeField] private float downPos;

    [Header("Color")]
    [SerializeField] private Material upColor;
    [SerializeField] private Material downColor;

    [Header("Count")]
    [SerializeField] private int cnt;

    private MeshRenderer renderer;
    //private Rigidbody rb;

    public bool isDown = false;
    public bool IsDown { get { return isDown; } }

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        //rb = GetComponent<Rigidbody>();
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
            //rb.velocity = dir * speed;
            transform.Translate(dir * speed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, target);

            if (distance < 0.1f)
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
                Debug.Log("µé¾î¿È");

                foreach (Collider collider in colliders)
                {
                    cnt--;

                    if (cnt == 0)
                    {
                        isDown = true;
                        transform.DOKill();
                        transform.DOMoveY(downPos, 0.5f);
                        renderer.material = downColor;

                        collider.transform.SetParent(this.transform);
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
                    transform.DOKill();
                    transform.DOMoveY(upPos, 0.5f);
                    renderer.material = upColor;

                    //collision.transform.SetParent(null);
                }
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("test1"))
    //    {
    //        cnt--;

    //        if (cnt == 0)
    //        {
    //            isDown = true;
    //            transform.DOKill();
    //            transform.DOMoveY(downPos, 0.5f);
    //            renderer.material = downColor;

    //            collision.transform.SetParent(this.transform);
    //        }
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.transform.CompareTag("test1"))
    //    {
    //        cnt++;

    //        if (cnt != 0)
    //        {
    //            isDown = false;
    //            transform.DOKill();
    //            transform.DOMoveY(upPos, 0.5f);
    //            renderer.material = upColor;

    //            collision.transform.SetParent(null);
    //        }
    //    }
    //}
}
