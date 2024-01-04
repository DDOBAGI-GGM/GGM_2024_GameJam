using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class Supporter : MonoBehaviour, IReset
{
    [TextArea] private string description;

   [SerializeField] private Transform target;
    public Transform Target { get { return target; } private set { } }

    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField] private bool chase = false;

    private int followNum = 0;
    public int FollowNum { get { return followNum; } set {  followNum = value; } }

    private bool firstGetMe = false;
    
    [SerializeField] private bool is_tutorial = false;


    [SerializeField] private Vector3 orginPos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        orginPos = gameObject.transform.position;
    }

    public void ChaseStart(Transform _target)
    {
     //   agent.isStopped = false;
        target = _target;
        chase = true;
        agent.stoppingDistance = 2.5f;

        if (!firstGetMe)
        {
            if (!is_tutorial)
            {
                StageManager.Instance.GetDust();
            }
            firstGetMe = true;
        }
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

    public void Reset()
    {
        Debug.Log("지지자 초기화");
        target = null;
        chase = false;      // 쫒지마
       // agent.isStopped = true;       //멈춤으로 해주기
        agent.SetDestination(orginPos);        // 본래 포지션으로 설정해서 멈주게
        transform.position = orginPos;     // 본래 포지션으로 이동
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
