using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Transform bridge;
    private bool isChecking = false;

    [SerializeField] private List<Transform> walls;

    private void Awake()
    {
        bridge = transform.GetChild(0).GetComponent<Transform>();
        bridge.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isChecking == false && GameManager.Instance.Is3D)         // 3D 일때만 감지함.
        {
            if (StageManager.Instance.IsClear)
            {
                bridge.gameObject.SetActive(true);
                StageManager.Instance.NextStage();
                isChecking = true;

                PlayerStar.Instance.UseStar();

                foreach (Transform obj in walls)
                    obj.gameObject.SetActive(false);
            }
        }
    }
}
