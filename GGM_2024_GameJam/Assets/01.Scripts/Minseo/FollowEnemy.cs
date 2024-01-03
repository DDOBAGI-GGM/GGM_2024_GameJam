using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    [SerializeField] private float startdelay;
    [SerializeField] private float speed;
    [SerializeField] private float numToSubtract;

    [SerializeField] private bool isStart = false;

    private Vector3 originalPosition;

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
            originalPosition = transform.position;

            transform.position = originalPosition + new Vector3(-numToSubtract, 0, 0);
        }
    }
}
