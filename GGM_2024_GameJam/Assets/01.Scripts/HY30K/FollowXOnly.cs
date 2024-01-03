using UnityEngine;
using Cinemachine;

public class FollowXOnly : MonoBehaviour
{
    public Transform target; // ����ٴ� ���

    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (virtualCamera != null && target != null)
        {
            // Follow�� LookAt ������ �����մϴ�.
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
        }
        else
        {
            Debug.LogError("Cinemachine Virtual Camera �Ǵ� Target�� �������� �ʾҽ��ϴ�.");
        }
    }

    private void Update()
    {
        // ����� ���� ��ġ�� �����ͼ� ī�޶��� ��ġ�� ������Ʈ�մϴ�.
        if (target != null)
        {
            if (GameManager.Instance.Is3D)
            {
                virtualCamera.Follow = null;
                Vector3 newPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
                transform.position = newPosition;
            }
            else
            {
                virtualCamera.Follow = target;
            }
        }
    }
}