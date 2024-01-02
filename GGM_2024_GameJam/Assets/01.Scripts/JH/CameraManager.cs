using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    private Camera _cam;

    public Camera Cam
    {
        get
        {
            if (_cam == null)
                _cam = Camera.main;
            return _cam;
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    private void Update()
    {
        CamAngleChange();
    }

    private void CamAngleChange()
    {
        if (GravityManager.Instance.Is3D == false)
        {
            Cam.transform.DOMove(new Vector3(0, 30, 0), 1f); //= new Vector3(0, 30, 0);
            Cam.transform.DORotate(new Vector3(90, 0, 0), 1f); //= Quaternion.Euler(90, 0, 0);

        }
        else
        {
            Cam.transform.DOMove(new Vector3(0, 25, -15), 1f); //= new Vector3(0, 25, -15);
            Cam.transform.DORotate(new Vector3(65, 0, 0), 1f); //= Quaternion.Euler(65, 0, 0);
        }
    }
}
