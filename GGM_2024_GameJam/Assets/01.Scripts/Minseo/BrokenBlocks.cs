using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class BrokenBlocks : MonoBehaviour, IReset
{
    [SerializeField] private float delay = 1;

    private Vector3 originalPosition;

    bool isCollision = false;
    bool isOrigin = false;

    private void Start()
    {
        originalPosition = transform.position;
        isOrigin = gameObject.activeSelf;
    }

    public void Reset()
    {
        gameObject.SetActive(isOrigin);
    }

    private void Update()
    {
        Ray();
    }

    private void Ray()
    {
        RaycastHit hit;
        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.up, out hit, transform.rotation, 0.3f);

        //Debug.DrawRay(transform.position, transform.forward * 1f, Color.red);

        if (isHit)
        {
            //Debug.Log("ef3refvfkr");
            if (!isCollision)
            {
                isCollision = true;

                StartCoroutine(ShakeRoutine());
                Invoke("DestroyBlock", delay);
            }
        }
    }

    private IEnumerator ShakeRoutine()
    {
        while (isCollision)
        {
            ShakeBlock();
            //Debug.Log("ef");
            yield return null;
        }
    }

    private void ShakeBlock()
    {
        float offsetX = UnityEngine.Random.Range(-0.1f, 0.1f);
        float offsetY = UnityEngine.Random.Range(-0.1f, 0.1f);
        float offsetZ = UnityEngine.Random.Range(-0.1f, 0.1f);

        transform.position = originalPosition + new Vector3(offsetX, offsetY, offsetZ);
    }

    private void DestroyBlock()
    {
        transform.position = originalPosition;

        isCollision = false;

        gameObject.SetActive(false);
    }
}
