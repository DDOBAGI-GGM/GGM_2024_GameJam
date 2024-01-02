using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private float distance;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("test1"))
            {
                //if (StageManager.Instance.StarCnt < StageManager.Instance.DustCnt)
                //{
                    StageManager.Instance.GetDust(gameObject);
                    StageManager.Instance.GetStar(gameObject);
                this.gameObject.SetActive(false);
                //Destroy(this.gameObject);
                //StageManager.Instance.GetStar();
                //}
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, distance);
    }
}
