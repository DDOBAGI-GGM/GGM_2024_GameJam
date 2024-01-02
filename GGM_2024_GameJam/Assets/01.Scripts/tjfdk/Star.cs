using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Star : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;
    private bool isCollision = false;

    private void Update()
    {
        //Collider[] colliders = Physics.OverlapSphere(transform.position, distance);

        //foreach (Collider collider in colliders)
        //{
        //    if (collider.CompareTag("test1"))
        //    {
        //        //if (StageManager.Instance.StarCnt < StageManager.Instance.DustCnt)
        //        //{
        //            StageManager.Instance.GetDust(gameObject);
        //            StageManager.Instance.GetStar(gameObject);
        //        this.gameObject.SetActive(false);
        //        //Destroy(this.gameObject);
        //        //StageManager.Instance.GetStar();
        //        //}
        //    }
        //}

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
