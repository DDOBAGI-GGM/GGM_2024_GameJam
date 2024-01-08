    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    [SerializeField] private float startdelay;
    [SerializeField] private float speed;
    [SerializeField] private float numToSubtract;

    [SerializeField] private bool isStart = false;

    [SerializeField] private GameObject player;

    private Rigidbody _rigidbody;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerMovement = FindObjectOfType<PlayerMovement>();

        Invoke("StartTracking", startdelay);
    }

    private void Update()
    {
        if(isStart)
        {
            _rigidbody.velocity = Vector3.right * speed;
        }
    }

    public void StartTracking()
    {
        isStart = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isStart = false;
            if (GameManager.Instance.cannotAttack == false)
                _playerMovement.IsDead = true;
            Debug.Log("플레이어 충돌");
        }
    }

    public void PlayerDead()
    {
        transform.position = player.transform.position + new Vector3(-numToSubtract, 0, 0);
        isStart = true;
    }
}
