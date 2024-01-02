using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Supporter : MonoBehaviour
{
    [TextArea] private string description;

   [SerializeField] private Transform target;

    private NavMeshAgent agent;

    [SerializeField] private bool chase = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void ChaseStart(Transform _target)
    {
        target = _target;
        chase = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (chase && GameManager.Instance.Is3D)
        {
            agent.SetDestination(target.position);
        }
    }

    public void UseMe(Vector3 pos)
    {
        chase = false;
        agent.SetDestination(pos);
    }
}
