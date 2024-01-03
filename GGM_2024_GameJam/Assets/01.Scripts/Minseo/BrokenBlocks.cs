using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class BrokenBlocks : MonoBehaviour
{
    [SerializeField] private float delay = 1;

    [SerializeField] LayerMask _playerLayerMask;
    private Vector3 originalPosition;


    public bool isBroken = false;
    private bool isFirst = true;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if(isBroken)
            gameObject.SetActive(true);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("1");
        if (collision.transform.CompareTag("Player") && isFirst == true)
        {
            
            isFirst = false;
            //if (GameManager.Instance.Is3D == false)
            //{
                StartCoroutine(ShakeRoutine());
                Invoke("DestroyBlock", delay);
           // }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Player") && isFirst == true)
        {
            Debug.Log("ef");
            isFirst = false;
            if (GameManager.Instance.Is3D == false)
            {
                StartCoroutine(ShakeRoutine());
                Invoke("DestroyBlock", delay);
            }
        }
    }


    private IEnumerator ShakeRoutine()
    {
        while (!isFirst)
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
        
        isFirst = true;

        gameObject.SetActive(false);
    }
}
