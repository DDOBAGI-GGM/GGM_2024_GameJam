using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (chase && GameManager.Instance.Is3D && target != null)
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
            //Debug.Log("서포터 초기화 " + gameObject.name);
            chase = false;      // i吏�留?
            target = null;
            //agent.isStopped = true;       //硫덉땄?쇰줈 ?댁＜湲?
            agent.SetDestination(orginPos);        // 蹂몃옒 ?ъ??섏쑝濡??ㅼ젙?댁꽌 硫덉＜寃?
            transform.position = orginPos;     // 蹂몃옒 ?ъ??섏쑝濡??대룞
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
