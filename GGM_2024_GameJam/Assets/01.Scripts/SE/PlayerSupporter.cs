using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSupporter : MonoBehaviour
{
    [SerializeField] public List<Supporter> supportersList = new List<Supporter>();     // 나에게 있는 지지자들
    //private Queue<Supporter> supportersQueue = new Queue<Supporter>();

    [SerializeField] private GameObject supporterEdgePrefab;            // 배치 가능하다고 표시해줄 프리텝
    private LineRenderer lineRenderer;
    [SerializeField] private List<GameObject> supporterEdgeList = new List<GameObject>();
    [SerializeField] private LayerMask WallOrObstacleLayer;
    private bool is_showEdge = false;

    [SerializeField] private LayerMask supporterLayer;
    private RaycastHit hit;

    private PlayerInput _playerInput;
    private Transform lastFollow, lastlastFollow;

    private bool reverse = false;

    public static PlayerSupporter Instance;

    private void Start()
    {
        _playerInput = GameManager.Instance.PlayerMovement.gameObject.GetComponent<PlayerInput>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lastFollow = transform;
        Instance = this;
    }

    private void Update()
    {
        // 지지자 너 내 것이 되라
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z * 0.7f);
        if (Physics.Raycast(playerPos, _playerInput.Move, out hit, 3, supporterLayer))           // 레이퀘스트 해주고
        {
            // 변수 가져와서 Supporter 내의 함수 사용
            // 큐에 넣어주고 끝 이 친구 더이상 충돌하지 않도록 나의 서포터로 레이어? 바꿔주기
            if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.Is3D)       // 3D 여야 함.
            {
                var supporter = hit.collider.GetComponent<Supporter>();
                //if (supporter != null && !supportersQueue.Contains(supporter))      // 큐에 없어야행
                if (supporter != null && !supportersList.Contains(supporter))      // 리스트에 없어야행
                {
                    supporter.ChaseStart(lastFollow);
                    lastFollow = supporter.transform;
                    //supportersQueue.Enqueue(supporter);
                    supportersList.Add(supporter);
                    //supporter.FollowNum = supportersQueue.Count;
                    supporter.FollowNum = supportersList.Count;
                    CreateSupporterEdge();
                }
                return;     // 뒤에있는거 작동 ㄴㄴ
            }
        }


        for (int i = 0; i < supportersList.Count; i++)
        {
            if (GameManager.Instance.Is3D)
            {
                supportersList[i].ChaseStart(supportersList[i].Target);
            }
            else
            {
                supportersList[i].UseMe(lineRenderer.GetPosition(i + 1));
            }
        }


        // 지지자 방출
       /*
        if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.Is3D)      // 3D 일때
        {
            //while (supportersQueue.Count > 0)
            //    {
            //        supportersList.Remove(supportersQueue.Peek());
            //        supporterEdgeList.Remove(supportersQueue.Peek().gameObject);
            //        supportersQueue.Peek().UseMe(lineRenderer.GetPosition(supportersQueue.Peek().FollowNum));      // 서포터의 팔로우 넘버 사용해서 바꾸어 주기
            //        supportersQueue.Dequeue();
            //    }

            //for (int i = 0; i < supportersList.Count; i++)
            //{
            //    supportersList[i].UseMe(lineRenderer.GetPosition(supportersList[i].FollowNum));
            //}

            //lastFollow = transform;

            for (int i = 0; i < supporterEdgeList.Count; i++)
            {
                supportersList[i].UseMe(lineRenderer.GetPosition(supportersList[i].FollowNum));
                supporterEdgeList[i].gameObject.SetActive(false);
            }
            lineRenderer.gameObject.SetActive(false);
        }
    */

        // 지지자와 라인렌더러의 위치 정해주기
        //if (supportersQueue.Count != 0 && GameManager.Instance.Is3D)        // 3D 일때만
        if (supportersList.Count != 0 && GameManager.Instance.Is3D)        // 3D 일때만
        {
            if (!is_showEdge)       // 지금 그려주는 것이 처음이면
            {
                lineRenderer.gameObject.SetActive(true);

                for (int i = 0; i < supporterEdgeList.Count; i++)
                {
                    supportersList[i].ChaseStart(supportersList[i].Target);
                    supporterEdgeList[i].gameObject.SetActive(true);
                }
                is_showEdge = true;
            }


            Vector3 dir = GameManager.Instance.PlayerMovement.GetComponent<PlayerInput>().Move.normalized;     // 노멀라이즈하고

            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, (lineRenderer.positionCount - 1 * 1.5f) + 1.5f, WallOrObstacleLayer))            // 벽에 닿았다면 
            {
                Vector3 pos = hit.point;
                if (dir.x != 0) pos.x += dir.x * -1;
                if (dir.y != 0) pos.y += dir.y * -1;

                if (Vector3.Distance(pos, lineRenderer.GetPosition(lineRenderer.positionCount - 2)) < 1f || reverse)
                {
                    if (dir.x != 0) pos.x += dir.x * 1.5f * -1;
                    if (dir.y != 0) pos.y += dir.y * 1.5f * -1;
                    reverse = true;
                }

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, pos);
                for (int i = 2; i < lineRenderer.positionCount; i++)
                {
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i - 1) + dir * 1.5f * -1);      // 하나를 정해주면 그것을 따라서 움직임. 이건 전에 꺼 따라서 움직이게 한 것임.
                    //lineRenderer.SetPosition(i, pos);
                }
            }
            else
            {
                reverse = false;
                lineRenderer.SetPosition(0, transform.position);
                Vector3 lineEnd = new Vector3(transform.position.x + dir.x * 1.5f, transform.position.y + dir.y * 1.5f, transform.position.z);
                lineRenderer.SetPosition(1, lineEnd);
                for (int i = 2; i < lineRenderer.positionCount; i++)
                {
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i - 1) + dir * 1.5f);      // 하나를 정해주면 그것을 따라서 움직임. 이건 전에 꺼 따라서 움직이게 한 것임.
                }
            }
            for (int i = 1; i < supporterEdgeList.Count + 1; i++)
            {
                supporterEdgeList[i - 1].transform.position = lineRenderer.GetPosition(i);      // 포지션은 1부터, 엣지는 0부터
                                                                                                //Debug.Log(lineRenderer.GetPosition(i));
            }
        }
        else
        {
            if (!GameManager.Instance.Is3D)         // 2D 일 때
            {
                is_showEdge = false;
                for (int i = 0; i < supporterEdgeList.Count; i++)
                {
                    supporterEdgeList[i].gameObject.SetActive(false);
                }
            }
        }
    }

    private void CreateSupporterEdge()      // 생성하고 추가만 해줌.
    {
        Quaternion rotation = Quaternion.Euler(-90f, 90f, -90f);
        //if (supportersQueue.Count != 0 && GameManager.Instance.Is3D)        // 3D 일때만
        if (supportersList.Count != 0 && GameManager.Instance.Is3D)        // 3D 일때만
        {
            GameObject supporterEdge = Instantiate(supporterEdgePrefab, transform.position, rotation, transform);
            supporterEdgeList.Add(supporterEdge);
        }
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
    }

    private void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            /*Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z * 0.7f);
            Debug.DrawLine(playerPos, new Vector3(transform.position.x + _playerInput.Move.x, transform.position.y + _playerInput.Move.y, playerPos.z), Color.red);

            if (supportersQueue.Count > 0)
            {
                bool isHit = Physics.SphereCast(transform.position, 1f, GameManager.Instance.PlayerMovement.GetComponent<PlayerInput>().Move, out hit, 2f);
                if (isHit)
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(transform.position, transform.up * hit.distance);
                    Gizmos.DrawWireSphere(transform.position + transform.up * hit.distance, 1);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(transform.position, transform.up);
                }
            }*/

            Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z * 0.7f);       // 플레이어가 지지자 얻으려고 하는 것
            if (Physics.Raycast(playerPos, _playerInput.Move, out hit, 3, supporterLayer))           // 레이퀘스트 해주고
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(playerPos, hit.point);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(playerPos, new Vector3(playerPos.x + _playerInput.Move.x, playerPos.y + _playerInput.Move.y, playerPos.z));
            }

        }
    }

    /*    public void ChangeState(bool is3D)
        {
            if (is3D)
            {
                for (int i = 0; i < supportersList.Count; i++)
                {
                    supportersList[i].ChaseStart(lastFollow);
                    CreateSupporterEdge();
                }
            }
        }*/

    public void ReStart()
    {
        for (int i = 0; i < supportersList.Count; i++)
        {
            if (supportersList[i].Stage == StageManager.Instance.CurrentStage)
            {
                Debug.Log($"{supportersList[i].FirstGetMe} {i} ");
                if (supportersList[i].FirstGetMe)
                {
                    supportersList[i].FirstGetMe = false;
                    supportersList.Remove(supportersList[i]);
                    Destroy(supporterEdgeList[i].gameObject);
                    supporterEdgeList.Remove(supporterEdgeList[i]);
                }
            }
        }
        if (supportersList.Count <= 0) lastFollow = transform;      // 만약에 0이면

        lineRenderer.positionCount = supportersList.Count + 1;
    }

}
