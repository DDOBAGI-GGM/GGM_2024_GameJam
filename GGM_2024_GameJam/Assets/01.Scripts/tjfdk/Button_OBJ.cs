using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using DG.Tweening;

public class Button_OBJ : MonoBehaviour, IReset
{
    private MeshRenderer renderer;

    [Header("Ray")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;

    [Header("Position")]
    [SerializeField] private float upPos;
    [SerializeField] private float downPos; 

    [Header("Color")]
    [SerializeField] private Material upColor;
    [SerializeField] private Material downColor;

    [Header("Interactiond")]
    [SerializeField] private List<GameObject> objs = new List<GameObject>();
    [SerializeField] private bool isDown = false;
    public bool IsDown { get { return isDown; } }

    private Transform body;
    private bool isCollision = false;

    private void Awake()
    {
        body = transform.GetChild(0).GetComponent<Transform>();
        renderer = body.GetComponent<MeshRenderer>();
    }

    public void Reset()
    {
        isDown = false;
    }

    private void Update()
    {
        Ray();

        if (isDown)
        {
            foreach (GameObject obj in objs)
                obj.SetActive(true);
        }
        else
        {
            foreach (GameObject obj in objs)
                obj.SetActive(false);
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
                    isDown = true;
                    body.DOKill();
                    body.DOLocalMoveY(downPos, 0.5f);
                    renderer.material = downColor;
                }
            }
        }
        else
        {
            if (isCollision)
            {
                isDown = false;
                isCollision = false;
                body.DOKill();
                body.DOLocalMoveY(upPos, 0.5f);
                renderer.material = upColor;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
