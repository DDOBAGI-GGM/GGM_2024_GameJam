using UnityEngine;
using Cinemachine;

public class FollowXOnly : MonoBehaviour
{
    public Transform target; // 따라다닐 대상

    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (virtualCamera != null && target != null)
        {
            // Follow와 LookAt 설정을 해제합니다.
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
        }
        else
        {
            Debug.LogError("Cinemachine Virtual Camera 또는 Target이 설정되지 않았습니다.");
        }
    }

    private void Update()
    {
        // 대상의 현재 위치를 가져와서 카메라의 위치를 업데이트합니다.
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