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
            isCollision = false;
            isInteraction = false;
        }
    }

    private void Update()
    {
        Ray();
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
                    isInteraction = true;
                    if (StageManager.Instance.GetStar())
                    {
                        Debug.Log("����");
                        PlayerStar.Instance.StarAdd(this.transform);
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
