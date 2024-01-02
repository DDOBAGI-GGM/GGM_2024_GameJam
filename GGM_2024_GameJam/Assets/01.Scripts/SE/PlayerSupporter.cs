using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSupporter : MonoBehaviour
{
    [SerializeField] private List<Supporter> supporters = new List<Supporter>();        // SerializeField 는 지워도 되고~
    private Queue<Supporter> supportersQueue = new Queue<Supporter>();

    [SerializeField] private LayerMask supporterLayer;

    private void Update()
    {
        //if ()           // 레이퀘스트 해주고
        {
            // 변수 가져와서 Supporter 내의 함수 사용
            // 큐에 넣어주고 끝 이 친구 더이상 충돌하지 않도록 나의 서포터로 레이어? 바꿔주기
        }
    }

    private void UseSupporter()
    {
        // 레이 퀘스트 쏴서 지금 사용이 가능하면

        supportersQueue.Dequeue();      // 빼주기
    }

    private void OnDrawGizmos()
    {
        //Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Color.red);
    }
}
