using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Supporter : MonoBehaviour
{
    [TextArea] private string description;

   [SerializeField] private Transform target;

    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField] private bool chase = false;

    private int followNum = 0;
    public int FollowNum { get { return followNum; } set {  followNum = value; } }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    public void ChaseStart(Transform _target)
    {
        target = _target;
        chase = true;
        agent.stoppingDistance = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (chase && GameManager.Instance.Is3D)
        {
            agent.SetDestination(target.position);
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("IsMove", false);
        }
        else
        {
            animator.SetBool("IsMove", true);
        }
    }

    public void UseMe(Vector3 pos)
    {
        chase = false;
        agent.stoppingDistance = 0;
        agent.SetDestination(pos);
    }
}
