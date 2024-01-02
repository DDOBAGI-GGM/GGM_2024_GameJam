using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Supporter : MonoBehaviour
{
    [TextArea] private string description;

   [SerializeField] private Transform target;

    private NavMeshAgent agent;

    [SerializeField] private Material red;

    [SerializeField] private bool chase = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void ChaseStart()
    {
        GetComponent<MeshRenderer>().material = red;
        chase = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (chase)
        {
            agent.SetDestination(target.position);
        }
    }
}
