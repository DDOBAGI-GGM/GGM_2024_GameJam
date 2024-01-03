using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.Experimental.GraphView.GraphView;
using Unity.VisualScripting;

public class Weigh_OBJ : MonoBehaviour
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

    //[Header("Position")]
    //[SerializeField] private float upPos;
    //[SerializeField] private float downPos;

    //[Header("Color")]
    //[SerializeField] private Material upColor;
    //[SerializeField] private Material downColor;

    [Header("Count")]
    [SerializeField] private int cnt;

    //private Transform btn;
    public PlayerMovement player;
    private Transform originPos;
    public bool isDown = false;
    public bool IsDown { get { return isDown; } }

    private void Awake()
    {
        //btn = this.transform.GetChild(0).GetComponent<Transform>();
        //renderer = btn.GetComponent<MeshRenderer>();
        originPos = transform;
    }

    public void Reset()
    {
        transform.position = originPos.position;
        isCollision = false;
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

            //player.SetMovement(dir);

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
                        //btn.DOKill();
                        ////btn.DOLocalMoveY(downPos, 0.5f);
                        //renderer.material = downColor;

                        //if (player == null)
                        //    player = collider.transform.GetComponent<PlayerMovement>();
                        //playerTrm = collider.gameObject.transform;
                        //collider.transform.SetParent(btn);
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
                    //btn.DOKill();
                    //btn.DOLocalMoveY(upPos, 0.5f);
                    //renderer.material = upColor;
                    //btn.GetChild(0).SetParent(null);
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
