using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy_OBJ : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private float speed;
    [SerializeField] int idx = 0;

    private void Update()
    {
        Vector3 moveDirection = points[idx].position.normalized * speed * Time.deltaTime;
        transform.Translate(moveDirection);

        float distance = Vector3.Distance(transform.position, points[idx].position);

        if (distance < 0.5f)
        {
            if (idx == points.Count - 1)
                idx--;
            else
                idx++;
        }

        //transform.DOMove(points[idx].position, speed).OnComplete(() =>
        //{
        //    idx++;
        //    if (idx == points.Count)
        //        idx = 0;
        //});
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("test1"))
        {
            // 데미지 주기?.. 밀어내기???ㄴ... ＝ 죽이기
        }
    }
}
