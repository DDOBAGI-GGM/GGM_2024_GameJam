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
    private List<bool> objActive = new List<bool> { false };

    private Transform body;
    private bool isCollision = false;

    private void Awake()
    {
        body = transform.GetChild(0).GetComponent<Transform>();
        renderer = body.GetComponent<MeshRenderer>();

        foreach (GameObject obj in objs)
            objActive.Add(obj.activeSelf);
    }

    public void Reset()
    {
        for (int i = 0; i < objActive.Count - 1; i++)
            objs[i].SetActive(objActive[i]);
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
                    foreach (GameObject obj in objs)
                        obj.SetActive(!obj.activeSelf);

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
                isCollision = false;

                foreach (GameObject obj in objs)
                    obj.SetActive(!obj.activeSelf);

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
