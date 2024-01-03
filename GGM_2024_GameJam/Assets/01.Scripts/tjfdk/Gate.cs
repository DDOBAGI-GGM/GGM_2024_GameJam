using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private GameObject bridge;
    private bool isChecking = false;

    private void Awake()
    {
        bridge = transform.GetChild(0).GetComponent<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isChecking == false)
        {
            if (StageManager.Instance.IsClear)
            {
                bridge.SetActive(true);
                StageManager.Instance.NextStage();
                isChecking = true;
            }
        }
    }
}
