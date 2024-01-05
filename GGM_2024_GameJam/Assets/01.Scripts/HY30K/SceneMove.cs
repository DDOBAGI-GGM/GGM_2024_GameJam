using UnityEngine;
using DG.Tweening;

public class SceneMove : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("튜토 따까리 초기화");
            var supporter = other.gameObject.GetComponent<PlayerSupporter>();
            if (supporter != null)
            {
                supporter.TutorialReSet();
                _wall.transform.DOScaleY(1, 2f);
            }

            Debug.Log("아니");
            StageManager.Instance.starCnt = StageManager.Instance.currentStageMax;
            StageManager.Instance.dustCnt = StageManager.Instance.currentStageMax;
            StageManager.Instance.NextStage();
            StageManager.Instance.DustCntClear();
        }
    }
}
