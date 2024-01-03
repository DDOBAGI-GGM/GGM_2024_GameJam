using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSupporter : MonoBehaviour
{
    [SerializeField] private List<Supporter> supportersList = new List<Supporter>();        // SerializeField �� ������ �ǰ�~ �ִ� �ܼ� Ȯ�ο�!
    private Queue<Supporter> supportersQueue = new Queue<Supporter>();
    
    [SerializeField] private GameObject supporterEdgePrefab;            // ��ġ �����ϴٰ� ǥ������ ������
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
        if (Physics.Raycast(playerPos, _playerInput.Move, out hit,  3, supporterLayer))           // ��������Ʈ ���ְ�
        {
            Debug.Log("������ ����");
            // ���� �����ͼ� Supporter ���� �Լ� ���
            // ť�� �־��ְ� �� �� ģ�� ���̻� �浹���� �ʵ��� ���� �����ͷ� ���̾�? �ٲ��ֱ�
            if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.Is3D)       // 3D ���� ��.
            {
                var supporter = hit.collider.GetComponent<Supporter>();
                if (supporter != null && !supportersQueue.Contains(supporter))      // ť�� �������
                {
                    supporter.ChaseStart(lastFollow);
                    lastFollow = supporter.transform;
                    supportersQueue.Enqueue(supporter);
                    supportersList.Add(supporter);
                    CreateSupporterEdge();
                }
                return;     // �ڿ��ִ°� �۵� ����
            }
        }

        if (Input.GetKeyDown (KeyCode.E) && GameManager.Instance.Is3D)      // 3D �϶�
        {
            int xOffset = 2;
            int yOffset = 2;        // �� ģ���� �� ����ϱ�!
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

        //if ()     // ���⼭ �����͸� ����� �� �ִ��� ������ Ȯ�� ��� ���ֱ�
        // �Ʒ����� ������. �ϴ� ������ �صΰ�
        if (supportersQueue.Count != 0 && GameManager.Instance.Is3D)        // 3D �϶���
        {
            Vector3 dir = GameManager.Instance.PlayerMovement.GetComponent<PlayerInput>().Move.normalized;     // ��ֶ������.
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
        if (supportersQueue.Count != 0 && GameManager.Instance.Is3D)        // 3D �϶���
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
