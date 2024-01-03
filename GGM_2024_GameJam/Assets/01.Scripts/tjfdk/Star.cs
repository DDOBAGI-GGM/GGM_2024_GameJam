using UnityEngine;
using DG.Tweening;

public class Star : MonoBehaviour, IReset
{
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;
    private bool isCollision = false;

    [Header("Reset")]
    [SerializeField] private bool isInteraction = false;

    public void Reset()
    {
        if (isInteraction)
        {
            this.gameObject.SetActive(true);
            isCollision = false;
            isInteraction = false;
        }
    }

    private void Update()
    {
        Ray();
        Size();
    }

    private void Ray()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, layer);

        if (colliders.Length > 0)
        {
            if (!isCollision)
            {
                isCollision = true;

                foreach (Collider collider in colliders)
                {
                    StageManager.Instance.GetDust(gameObject);
                    StageManager.Instance.GetStar(gameObject);
                    this.gameObject.SetActive(false);

                    isInteraction = true;
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

    private void Size()
    {
        transform.DOScale(new Vector3(2f, 2f, 2f), 0.5f).OnComplete(() 
            => { transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f); });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
