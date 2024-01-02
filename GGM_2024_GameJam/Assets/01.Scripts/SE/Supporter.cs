using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Supporter : MonoBehaviour
{
    [TextArea] private string description;

   [SerializeField] private Transform target;

    private NavMeshAgent agent;

    [SerializeField] private Material red, before;

    [SerializeField] private bool chase = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        before = GetComponent<MeshRenderer>().material;
    }

    public void ChaseStart(Transform _target)
    {
        GetComponent<MeshRenderer>().material = red;
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
        GetComponent<MeshRenderer>().material = before;
    }
}
