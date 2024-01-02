using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cleaner_OBJ : MonoBehaviour
{
    Rigidbody rb;

    [Header("Move")]
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    private int idx = 0;

    [Header("Entity")]
    [SerializeField] private GameObject dust;
    [SerializeField] private Transform dustPos;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 테스트용...
        if (Input.GetKeyUp(KeyCode.P))
            DropEntity();

        if (dust)
            dust.transform.position = dustPos.position;
        if (player)
            player.transform.position = playerPos.position;

        Vector3 target = points[idx].position;
        Vector3 dir = (target - transform.position).normalized;
        rb.velocity = dir * speed;

        float distance = Vector3.Distance(transform.position, target);

        if (distance < 0.1f)
            idx = (idx + 1) % points.Length;
    }

    public void DropEntity()
    {
        if (dust != null)
        {
            dust.transform.SetParent(null);
            dust = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (dust && collision.transform.CompareTag("test1"))
        {
            player = collision.gameObject;
            collision.transform.SetParent(this.gameObject.transform);
        }

        if (dust == null && collision.transform.CompareTag("test2"))
        {
            dust = collision.gameObject;
            collision.transform.SetParent(this.gameObject.transform);
        }
    }
}
