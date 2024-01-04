using UnityEngine;
using DG.Tweening;

public class Star : MonoBehaviour, IReset
{
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;
    private bool isCollision = false;

    [Header("Reset")]
    [SerializeField] private bool isInteraction = false;
    private Vector3 originPos;
    private Transform parent;

    private void Start()
    {
        originPos = transform.position;
        parent = transform.parent;
    }

    public void Reset()
    {
        if (isInteraction)
        {
            isCollision = false;
            isInteraction = false;
            transform.position = originPos;
            transform.SetParent(parent);
            gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        Ray();
    }

    private void Ray()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, layer);

        if (colliders.Length > 0 && !isInteraction)
        {
            if (!isCollision)
            {
                isCollision = true;

                foreach (Collider collider in colliders)
                {
                    Debug.Log("먹어져야하는 거리");
                    if (StageManager.Instance.GetStar())
                    {
                        PlayerStar.Instance.StarAdd(this.transform);
                        SoundManager.Instance?.PlaySFX("star");
                        isInteraction = true;
                    }
                }
            }
        }
        else
        {
            if (isCollision)
            {
                isCollision = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
