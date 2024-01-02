using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSupporter : MonoBehaviour
{
    [SerializeField] private List<Supporter> supporters = new List<Supporter>();        // SerializeField �� ������ �ǰ�~
    private Queue<Supporter> supportersQueue = new Queue<Supporter>();

    [SerializeField] private LayerMask supporterLayer;

    private void Update()
    {
        //if ()           // ��������Ʈ ���ְ�
        {
            // ���� �����ͼ� Supporter ���� �Լ� ���
            // ť�� �־��ְ� �� �� ģ�� ���̻� �浹���� �ʵ��� ���� �����ͷ� ���̾�? �ٲ��ֱ�
        }
    }

    private void UseSupporter()
    {
        // ���� ����Ʈ ���� ���� ����� �����ϸ�

        supportersQueue.Dequeue();      // ���ֱ�
    }

    private void OnDrawGizmos()
    {
        //Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Color.red);
    }
}
