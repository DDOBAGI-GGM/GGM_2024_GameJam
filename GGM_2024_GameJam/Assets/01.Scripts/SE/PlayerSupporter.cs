using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSupporter : MonoBehaviour
{
    [SerializeField] private List<Supporter> supportersList = new List<Supporter>();        // SerializeField �� ������ �ǰ�~
    [SerializeField] private Queue<Supporter> supportersQueue = new Queue<Supporter>();

    [SerializeField] private LayerMask supporterLayer;
    private RaycastHit hit;

    private PlayerInput _playerInput;

    private Transform lastFollow;

    private void Start()
    {
        _playerInput = GameManager.Instance.PlayerMovement.gameObject.GetComponent<PlayerInput>();
        lastFollow = transform;
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, _playerInput.Move, out hit,  2, supporterLayer))           // ��������Ʈ ���ְ�
        {
            Debug.Log("dk");
            // ���� �����ͼ� Supporter ���� �Լ� ���
            // ť�� �־��ְ� �� �� ģ�� ���̻� �浹���� �ʵ��� ���� �����ͷ� ���̾�? �ٲ��ֱ�
            if (Input.GetKeyDown(KeyCode.E))
            {
                var supporter = hit.collider.GetComponent<Supporter>();
                if (supporter != null && !supportersQueue.Contains(supporter))      // ť�� �������
                {
                    supporter.ChaseStart(lastFollow);
                    lastFollow = supporter.transform;
                    supportersQueue.Enqueue(supporter);
                    supportersList.Add(supporter);
                }
                return;     // �ڿ��ִ°� �۵� ����
            }
        }

        if (Input.GetKeyDown (KeyCode.E))
        {
            int xOffset = 1;
            int yOffset = 1;
            while (supportersQueue.Count > 0)
            {
                supportersList.Remove(supportersQueue.Peek());
                supportersQueue.Peek().UseMe(new Vector3(transform.position.x + xOffset, transform.position.y + yOffset));
                supportersQueue.Dequeue();
                xOffset++; yOffset++;
            }
            xOffset = 0; yOffset = 0;
            lastFollow = transform;
        }
    }

    private void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Debug.DrawLine(transform.position, new Vector3(transform.position.x + _playerInput.Move.x, transform.position.y + _playerInput.Move.y, transform.position.z), Color.red);
        }
    }
}
