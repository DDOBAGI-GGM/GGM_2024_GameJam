using DG.Tweening;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : Singleton<GameManager>
{
    private PlayerMovement _player;
    private Camera _cam;
    private Light _light;

    // 이게 지금 3D인지 2D인지 확인해주는 불변수임
    [HideInInspector] public bool Is3D = false;

    public Camera Cam
    {
        get
        {
            if (_cam == null)
                _cam = Camera.main;
            return _cam;
        }
    }

    private Vector3 _2DGravity = new Vector3(0, 0, -9.8f);
    private Vector3 _3DGravity = new Vector3(0, 0, 0);

    public override void Awake()
    {
        base.Awake();
        _player = FindObjectOfType<PlayerMovement>();
        _light = FindObjectOfType<Light>();
    }

    private void Start()
    {
        Physics.gravity = _2DGravity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Is3D = !Is3D;
        }
        GravityConvert();
        CamAngleChange();
        // 이거 위에 이프문 안으로 넣어두기!
    }

    private void GravityConvert()
    {
        if (Is3D == false)
        {
            Physics.gravity = _2DGravity;
        }
        else
        {
            Physics.gravity = _3DGravity;
        }
    }

    private void CamAngleChange()
    {
        Cam.transform.DOMoveX(_player.transform.position.x, 1f);

        if (Is3D == false)
        {
            Cam.transform.DOMoveY(26, 1f); //= new Vector3(0, 30, 0);
            Cam.transform.DORotate(new Vector3(0, 0, 0), 1f); //= Quaternion.Euler(90, 0, 0);
            _light.transform.rotation = Quaternion.Euler(30, 0, 0); //new Vector3(130, 30, 0)
        }
        else
        {
            Cam.transform.DOMoveY(-0.5f, 1f); //= new Vector3(0, 25, -15);
            Cam.transform.DORotate(new Vector3(-35, 0, 0), 1f); //= Quaternion.Euler(65, 0, 0);
            _light.transform.rotation = Quaternion.Euler(30, 30, 0); //new Vector3(140, 0, 0)
        }
    }
}
