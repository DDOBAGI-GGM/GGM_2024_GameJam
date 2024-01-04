using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    [SerializeField] private List<GameObject> _walls;
    [SerializeField] private List<GameObject> _bridges;
    private bool isChecking = false;

    private void Start()
    {

        for (int i = 0; i < _bridges.Count; i++)
        {
            _bridges[i].SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isChecking == false)         // 3D �϶��� ������.
        {
            if (StageManager.Instance.IsClear)
            {
                //PlayerStar.Instance.UseStar();

                StageManager.Instance.NextStage();

                foreach (GameObject obj in _walls)
                {
                    obj.gameObject.transform.DOScaleY(0, 2f);
                }

                foreach (GameObject obj in _bridges)
                {
                    obj.SetActive(true);
                }

                isChecking = true;
            }
        }
    }
}
