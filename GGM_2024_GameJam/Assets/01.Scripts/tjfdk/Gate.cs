using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    private Transform bridge;
    private GameObject _gate;
    private bool isChecking = false;

    [SerializeField] private List<GameObject> walls;

    private void Awake()
    {
        bridge = transform.GetChild(0).GetComponent<Transform>();
        bridge.gameObject.SetActive(false);
        _gate = transform.GetChild(1).GetComponent<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isChecking == false && GameManager.Instance.Is3D)         // 3D �϶��� ������.
        {
            if (StageManager.Instance.IsClear)
            {
                bridge.gameObject.SetActive(true);
                StageManager.Instance.NextStage();
                isChecking = true;

                PlayerStar.Instance.UseStar();

                foreach (GameObject obj in walls)
                    obj.gameObject.transform.DOScaleZ(0, 2f);
            }
        }
    }
}
