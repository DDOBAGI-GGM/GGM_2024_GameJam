using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using DG.Tweening;

public class Button_OBJ : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private float upPos;
    [SerializeField] private float downPos; 

    [Header("Color")]
    [SerializeField] private Material upColor;
    [SerializeField] private Material downColor;

    private MeshRenderer renderer;
    private bool isDown = false;
    public bool IsDown { get { return isDown; } }

    [SerializeField] private LayerMask supporterLayerMask;
    private RaycastHit hit;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("test1"))
        {
            isDown = true;
            transform.DOKill();
            transform.DOMoveY(downPos, 0.5f);
            renderer.material = downColor;
        }
    }

    private void OnCollisionExit(Collision collision)   
    {
        if (collision.transform.CompareTag("test1"))
        {
            isDown = false;
            transform.DOKill();
            transform.DOMoveY(upPos, 0.5f);
            renderer.material = upColor;
        }
    }*/

    private void Update()
    {
        /*   Debug.DrawRay(transform.position, new Vector3(0, 0, -2), Color.black);
           //     if (Physics.SphereCast(transform.position, 1f, new Vector3(0, 0, -2), out hit, 2, supporterLayerMask))
           bool isHit = Physics.SphereCast(new Vector2, 1f, Vector3.up, out hit, 2f);
           if (isHit)
           {

           }*/
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        bool isHit = Physics.SphereCast(pos, 1f, Vector3.back, out hit, 2f, supporterLayerMask);

        if (isHit)
            {
            isDown = true;
            transform.DOLocalMoveY(downPos, 0.5f);
            renderer.material = downColor;
        }
        else
        {
            isDown = false;
            transform.DOLocalMoveY(upPos, 0.5f);
            renderer.material = upColor;
        }
    }
    
    /*

    private void OnDrawGizmos()
    {
  *//*      if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.color = Color.red;
            if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 2, supporterLayerMask));
            {
                Gizmos.color = Color.yellow;
            }
            Gizmos.DrawWireSphere(transform.position + transform.up, 1);
            Gizmos.color = Color.white;
        }*//*


       // Debug.DrawRay(transform.position, new Vector3(0, 0, -2), Color.black);
        //     if (Physics.SphereCast(transform.position, 1f, new Vector3(0, 0, -2), out hit, 2, supporterLayerMask))
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        bool isHit = Physics.SphereCast(pos, 1f, Vector3.back, out hit, 2f);
        if (isHit)
        {
            Debug.Log(hit.collider.gameObject.name);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(pos, transform.up * hit.distance);
            Gizmos.DrawWireSphere(pos + transform.up * hit.distance, 1);
        }
        else
        {
            Gizmos.color= Color.green;
            Gizmos.DrawRay(pos, transform.up);
        }
    }*/
}
