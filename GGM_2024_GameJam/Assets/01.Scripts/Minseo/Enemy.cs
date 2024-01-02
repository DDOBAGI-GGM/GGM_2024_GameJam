using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float startdelay;
    [SerializeField] private float speed;

    [SerializeField] private bool isStart = false;

    [SerializeField] private List<Transform> enemyPos = new List<Transform>();

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        Invoke("StartTracking", startdelay);
    }

    private void Update()
    {
        if(isStart)
        {
            _rigidbody.velocity = Vector3.right * speed;
        }

    }

    private void StartTracking()
    {
        isStart = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // 플레이어랑 충돋했을 때 위치 좀 초기화 해주는거를 리스트로 위치 저장해주고 내가 얻은 따까리의 수로 스위치 만들어주기
        }
    }
}
