using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    [SerializeField] private List<GameObject> walls;
    [SerializeField] private GameObject _bridge;
    private bool isChecking = false;

    private void Start()
    {
        _bridge.gameObject.SetActive(false);
    }

    public void ActiveBridge()
    {
        _bridge.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isChecking == false && GameManager.Instance.Is3D)         // 3D �϶��� ������.
        {
            if (StageManager.Instance.IsClear)
            {
                PlayerStar.Instance.UseStar();

                StageManager.Instance.NextStage();

                foreach (GameObject obj in walls)
                {
                    obj.gameObject.transform.DOScaleZ(0, 2f).OnComplete(ActiveBridge);
                }

                isChecking = true;
            }
        }
    }
}
