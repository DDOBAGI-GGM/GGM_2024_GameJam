using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSupporter : MonoBehaviour
{
    [SerializeField] private List<Supporter> supportersList = new List<Supporter>();        // SerializeField 는 지워도 되고~ 애는 단순 확인용!
    private Queue<Supporter> supportersQueue = new Queue<Supporter>();
    
    [SerializeField] private GameObject supporterEdgePrefab;            // 배치 가능하다고 표시해줄 프리텝
    private LineRenderer lineRenderer;
    private List<GameObject> supporterEdgeList = new List<GameObject>();
    [SerializeField] private LayerMask WallOrObstacleLayer;

    [SerializeField] private LayerMask supporterLayer;
    private RaycastHit hit;
    private bool is_UseOk = false;

    private PlayerInput _playerInput;
    private Transform lastFollow;

    private void Start()
    {
        _playerInput = GameManager.Instance.PlayerMovement.gameObject.GetComponent<PlayerInput>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lastFollow = transform;
    }

    private void Update()
    {
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z * 0.7f);
        if (Physics.Raycast(playerPos, _playerInput.Move, out hit,  3, supporterLayer))           // 레이퀘스트 해주고
        {
            Debug.Log("지지자 닿음");
            // 변수 가져와서 Supporter 내의 함수 사용
            // 큐에 넣어주고 끝 이 친구 더이상 충돌하지 않도록 나의 서포터로 레이어? 바꿔주기
            if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.Is3D)       // 3D 여야 함.
            {
                var supporter = hit.collider.GetComponent<Supporter>();
                if (supporter != null && !supportersQueue.Contains(supporter))      // 큐에 없어야행
                {
                    supporter.ChaseStart(lastFollow);
                    lastFollow = supporter.transform;
                    supportersQueue.Enqueue(supporter);
                    supportersList.Add(supporter);
                    CreateSupporterEdge();
                }
                return;     // 뒤에있는거 작동 ㄴㄴ
            }
        }

        if (Input.GetKeyDown (KeyCode.E) && GameManager.Instance.Is3D)      // 3D 일때
        {
            int xOffset = 2;
            int yOffset = 2;        // 이 친구들 잘 사용하기!
            while (supportersQueue.Count > 0)
            {
                supportersList.Remove(supportersQueue.Peek());
                supportersQueue.Peek().UseMe(new Vector2(lineRenderer.GetPosition(1).x + 1, lineRenderer.GetPosition(1).y + 1));
                supportersQueue.Dequeue();
                xOffset++; yOffset++;
            }
            xOffset = 0; yOffset = 0;
            lastFollow = transform;
        }

        //if ()     // 여기서 서포터를 사용할 수 있는지 없는지 확인 계속 해주기
        // 아래꺼도 들어가야함. 일단 저렇게 해두고
        if (supportersQueue.Count != 0 && GameManager.Instance.Is3D)        // 3D 일때만
        {
            Vector3 dir = GameManager.Instance.PlayerMovement.GetComponent<PlayerInput>().Move.normalized;     // 노멀라이즈된.
            Vector3 lineEnd = new Vector3(transform.position.x + dir.x * 2, transform.position.y + dir.y * 2, transform.position.z);
       /*     if (Physics.Raycast(transform.position, dir, out RaycastHit hit, 2, WallOrObstacleLayer))
            {
                Debug.Log("k");
                lineEnd = hit.point;
            }*/
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, lineEnd);
            foreach (var supporter in supporterEdgeList)
            {
                supporter.transform.position = lineRenderer.GetPosition(1);
            }
        }
    }

    private void CreateSupporterEdge()
    {
        lineRenderer.positionCount++;
        Quaternion rotation = Quaternion.Euler(-90f, 90f, -90f);
        if (supportersQueue.Count != 0 && GameManager.Instance.Is3D)        // 3D 일때만
        {
            GameObject supporterEdge = Instantiate(supporterEdgePrefab, lineRenderer.GetPosition(1), rotation, transform);
            supporterEdge.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            supporterEdgeList.Add(supporterEdge);
            is_UseOk = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z *0.7f);
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
            }
        }
    }
}
