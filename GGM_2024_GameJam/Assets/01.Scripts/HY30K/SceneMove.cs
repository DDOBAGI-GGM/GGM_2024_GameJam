using UnityEngine;
using DG.Tweening;

public class SceneMove : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ʃ�� ��� �ʱ�ȭ");
            var supporter = other.gameObject.GetComponent<PlayerSupporter>();
            if (supporter != null)
            {
                supporter.TutorialReSet();
                _wall.transform.DOScaleY(1, 2f);
            }

            Debug.Log("�ƴ�");
            StageManager.Instance.NextStage();
            StageManager.Instance.DustCntClear();
        }
    }
}
