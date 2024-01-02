using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Weigh_OBJ : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private float upPos;
    [SerializeField] private float downPos;

    [Header("Color")]
    [SerializeField] private Material upColor;
    [SerializeField] private Material downColor;

    [Header("Count")]
    [SerializeField] private int cnt;

    private MeshRenderer renderer;
    private bool isDown = false;
    public bool IsDown { get { return isDown; } }

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("test1"))
        {
            cnt--;

            if (cnt == 0)
            {
                isDown = true;
                transform.DOKill();
                transform.DOMoveY(downPos, 0.5f);
                renderer.material = downColor;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("test1"))
        {
            cnt++;

            if (cnt != 0 && isDown == false)
            {
                isDown = false;
                transform.DOKill();
                transform.DOMoveY(upPos, 0.5f);
                renderer.material = upColor;
            }
        }
    }
}
