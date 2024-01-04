using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColliderController : MonoBehaviour
{
    private BoxCollider _boxCol;

    private void Awake()
    {
        _boxCol = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (GameManager.Instance.Is3D)
            _boxCol.isTrigger = false;
        else
            _boxCol.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.CanConvert = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.CanConvert = true;
    }
}
