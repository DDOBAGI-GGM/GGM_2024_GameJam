using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
using static UnityEditor.Experimental.GraphView.GraphView;
using Unity.VisualScripting;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BrokenBlocks : MonoBehaviour//, IReset
{
    [SerializeField] private float delay = 1;

    private Vector3 originalPosition;

    public bool isBroken = false;
    bool isCollision = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    //public void Reset()
    //{
    //    isBroken = true;
    //}

    private void Update()
    {
        if(isBroken)
            gameObject.SetActive(true);

        Ray();
    }

    private void Ray()
    {
        RaycastHit hit;
        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.up, out hit, transform.rotation, 0.2f);

        Debug.DrawRay(transform.position, transform.forward * 1f, Color.red);

        if (isHit)
        {

            Debug.Log("1"); 
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
            Debug.Log("ef");
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
