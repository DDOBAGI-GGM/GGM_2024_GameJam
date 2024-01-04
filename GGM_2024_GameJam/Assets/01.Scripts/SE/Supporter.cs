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
    public bool FirstGetMe { get {  return firstGetMe; } set {  firstGetMe = value; } }
    
    [SerializeField] private bool is_tutorial = false;


    [SerializeField] private Vector3 orginPos;
    //[SerializeField] private bool isInteraction = false;
    //public bool IsInteraction { get { return isInteraction;} set { isInteraction = value; } }

    [SerializeField] private int stage = 0;
    public int Stage { get { return stage; } private set { } }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        orginPos = gameObject.transform.position;
    }

    public void ChaseStart(Transform _target)
    {
        target = _target;
        chase = true;
        agent.stoppingDistance = 2.5f;
        //agent.isStopped = false;

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
        if (firstGetMe)
        {
            Debug.Log("������ �ʱ�ȭ");
            target = null;
            chase = false;      // i지�?
            //agent.isStopped = true;       //멈춤?�로 ?�주�?
            agent.SetDestination(orginPos);        // 본래 ?��??�으�??�정?�서 멈주�?
            transform.position = orginPos;     // 본래 ?��??�으�??�동
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
