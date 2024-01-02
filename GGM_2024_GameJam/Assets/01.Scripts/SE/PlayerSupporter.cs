using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSupporter : MonoBehaviour
{
    [SerializeField] private List<Supporter> supportersList = new List<Supporter>();        // SerializeField 는 지워도 되고~
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
        if (Physics.Raycast(transform.position, _playerInput.Move, out hit,  2, supporterLayer))           // 레이퀘스트 해주고
        {
            Debug.Log("dk");
            // 변수 가져와서 Supporter 내의 함수 사용
            // 큐에 넣어주고 끝 이 친구 더이상 충돌하지 않도록 나의 서포터로 레이어? 바꿔주기
            if (Input.GetKeyDown(KeyCode.E))
            {
                var supporter = hit.collider.GetComponent<Supporter>();
                if (supporter != null && !supportersQueue.Contains(supporter))      // 큐에 없어야행
                {
                    supporter.ChaseStart(lastFollow);
                    lastFollow = supporter.transform;
                    supportersQueue.Enqueue(supporter);
                    supportersList.Add(supporter);
                }
                return;     // 뒤에있는거 작동 ㄴㄴ
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
