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

    private bool isDown = false;
    public bool IsDown { get { return isDown; } }

    private Transform originPos;
    private bool isCollision = false;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        originPos = transform;
    }

    public void Reset()
    {
        transform.position = originPos.position;
        isCollision = false;
    }

    private void Update()
    {
        Ray();
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
                    transform.DOKill();
                    transform.DOLocalMoveY(downPos, 0.5f);
                    renderer.material = downColor;
                }
            }
        }
        else
        {
            if (isCollision)
            {
                isDown = false;
                transform.DOKill();
                transform.DOLocalMoveY(upPos, 0.5f);
                renderer.material = upColor;
            }
        }
    }
}
