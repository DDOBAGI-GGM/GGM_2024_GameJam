using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;
    private bool isCollision = false;

    public Transform testPos;

    private void Awake()
    {
        testPos = transform;
    }

    private void Start()
    {
        this.gameObject.SetActive(true);
        transform.position = testPos.position;
        isCollision = false;
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
                Debug.Log("µé¾î¿È");

                foreach (Collider collider in colliders)
                {
                    StageManager.Instance.GetDust(gameObject);
                    StageManager.Instance.GetStar(gameObject);
                    this.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (isCollision)
            {
                isCollision = false;
                Debug.Log("³ª°¨");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
