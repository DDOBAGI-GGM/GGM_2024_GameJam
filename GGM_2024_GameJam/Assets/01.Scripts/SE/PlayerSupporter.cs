using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSupporter : MonoBehaviour
{
    [SerializeField] private List<Supporter> supportersList = new List<Supporter>();        // SerializeField �� ������ �ǰ�~ �ִ� �ܼ� Ȯ�ο�!
    private Queue<Supporter> supportersQueue = new Queue<Supporter>();

    [SerializeField] private GameObject supporterEdgePrefab;            // ��ġ �����ϴٰ� ǥ������ ������
    private LineRenderer lineRenderer;
    [SerializeField] private List<GameObject> supporterEdgeList = new List<GameObject>();
    [SerializeField] private LayerMask WallOrObstacleLayer;
    private bool is_showEdge = false;

    [SerializeField] private LayerMask supporterLayer;
    private RaycastHit hit;

    private PlayerInput _playerInput;
    private Transform lastFollow;

    private bool reverse = false;

    private void Start()
    {
        _playerInput = GameManager.Instance.PlayerMovement.gameObject.GetComponent<PlayerInput>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lastFollow = transform;
    }

    private void Update()
    {
        // ������ �� �� ���� �Ƕ�
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z * 0.7f);
        if (Physics.Raycast(playerPos, _playerInput.Move, out hit, 3, supporterLayer))           // ��������Ʈ ���ְ�
        {
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
                    supporter.FollowNum = supportersQueue.Count;
                    CreateSupporterEdge();
                }
                return;     // �ڿ��ִ°� �۵� ����
            }
        }

        // ������ ����
        if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.Is3D)      // 3D �϶�
        {
            int xOffset = 2;
            int yOffset = 2;        // �� ģ���� �� ����ϱ�!
            while (supportersQueue.Count > 0)
            {
                supportersList.Remove(supportersQueue.Peek());
                supporterEdgeList.Remove(supportersQueue.Peek().gameObject);
                supportersQueue.Peek().UseMe(lineRenderer.GetPosition(supportersQueue.Peek().FollowNum));      // �������� �ȷο� �ѹ� ����ؼ� �ٲپ� �ֱ�
                supportersQueue.Dequeue();
                xOffset++; yOffset++;
            }
            xOffset = 0; yOffset = 0;
            lastFollow = transform;
            for (int i = 0; i < supporterEdgeList.Count; i++)
            {
                Destroy(supporterEdgeList[i].gameObject);
            }
            lineRenderer.positionCount = 1;     // ���������ϱ� ���� ����!
            supporterEdgeList.Clear();
        }

        // �����ڿ� ���η������� ��ġ �����ֱ�
        if (supportersQueue.Count != 0 && GameManager.Instance.Is3D)        // 3D �϶���
        {
            if (!is_showEdge)       // ���� �׷��ִ� ���� ó���̸�
            {
                lineRenderer.gameObject.SetActive(true);
                foreach (var supporter in supporterEdgeList)
                {
                    supporter.SetActive(true);
                }
                is_showEdge = true;
            }

            Vector3 dir = GameManager.Instance.PlayerMovement.GetComponent<PlayerInput>().Move.normalized;     // ��ֶ������ϰ�

            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, (lineRenderer.positionCount - 1 * 1.5f) + 2.5f, WallOrObstacleLayer))            // ���� ��Ҵٸ� 
            {
                Vector3 pos = hit.point;
                Debug.Log(pos);
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
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i - 1) + dir * 1.5f * -1);      // �ϳ��� �����ָ� �װ��� ���� ������. �̰� ���� �� ���� �����̰� �� ����.
                    //lineRenderer.SetPosition(i, pos);
                }
            }
            else
            {
                reverse = false;
                lineRenderer.SetPosition(0, transform.position);
                Vector3 lineEnd = new Vector3(transform.position.x + dir.x * 2, transform.position.y + dir.y * 2, transform.position.z);
                lineRenderer.SetPosition(1, lineEnd);
                for (int i = 2; i < lineRenderer.positionCount; i++)
                {
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i - 1) + dir * 1.5f);      // �ϳ��� �����ָ� �װ��� ���� ������. �̰� ���� �� ���� �����̰� �� ����.
                }
            }
            for (int i = 1; i < supporterEdgeList.Count + 1; i++)
            {
                supporterEdgeList[i - 1].transform.position = lineRenderer.GetPosition(i);      // �������� 1����, ������ 0����
                                                                                                //Debug.Log(lineRenderer.GetPosition(i));
            }
        }
        else
        {
            if (!GameManager.Instance.Is3D)
            {
                lineRenderer.gameObject.SetActive(false);
                foreach (var supporter in supporterEdgeList)
                {
                    supporter.SetActive(false);
                }
                is_showEdge = false;
            }
        }

        // 2D�� �Ǿ��� ��
    }

    private void CreateSupporterEdge()      // �����ϰ� �߰��� ����.
    {
        Quaternion rotation = Quaternion.Euler(-90f, 90f, -90f);
        if (supportersQueue.Count != 0 && GameManager.Instance.Is3D)        // 3D �϶���
        {
            GameObject supporterEdge = Instantiate(supporterEdgePrefab, transform.position, rotation, transform);
            supporterEdge.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
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

            Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z * 0.7f);       // �÷��̾ ������ �������� �ϴ� ��
            if (Physics.Raycast(playerPos, _playerInput.Move, out hit, 3, supporterLayer))           // ��������Ʈ ���ְ�
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
}
