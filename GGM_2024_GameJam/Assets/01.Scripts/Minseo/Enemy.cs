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
            // �÷��̾�� �浸���� �� ��ġ �� �ʱ�ȭ ���ִ°Ÿ� ����Ʈ�� ��ġ �������ְ� ���� ���� ����� ���� ����ġ ������ֱ�
        }
    }
}
