using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallColliderController : MonoBehaviour
{
    private BoxCollider[] _boxCol;

    private void Awake()
    {
        _boxCol = GetComponents<BoxCollider>();
    }

    private void Update()
    {
        if (GameManager.Instance.Is3D)
        {
            _boxCol.ToList().ForEach(x => x.isTrigger = false);
        }
        else
        {
            _boxCol.ToList().ForEach(x => x.isTrigger = true);
        }
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
