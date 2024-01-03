using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Transform bridge;
    private bool isChecking = false;

    private void Awake()
    {
        bridge = transform.GetChild(0).GetComponent<Transform>();
        bridge.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isChecking == false)
        {
            if (StageManager.Instance.IsClear)
            {
                bridge.gameObject.SetActive(true);
                StageManager.Instance.NextStage();
                isChecking = true;
            }
        }
    }
}
