using DG.Tweening;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private PlayerMovement _player;
    public PlayerMovement PlayerMovement { get { return _player; } private set { } }
    private Camera _cam;
    private Light _light; 
    
    private bool _canConvert = true;
    public bool CanConvert
    {
        get => _canConvert;
        set => _canConvert = value;
    }


    // 이게 지금 3D인지 2D인지 확인해주는 불변수임
    [HideInInspector] public bool Is3D = false;

    [SerializeField] private float _3DY, _2DY, orthographicSize = 8.5f;
    [SerializeField] bool test = false;

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

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K) && CanConvert)
        {
            Debug.Log("시점변경");
            Is3D = !Is3D;
            GravityConvert();
            CamAngleChange();
        }
        // 이거 위에 이프문 안으로 넣어두기!
        Cam.transform.DOMoveX(_player.transform.position.x, 2f);
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

        if (Is3D == false)
        {
            Cam.transform.DOMoveY(_3DY, 1f); //= new Vector3(0, 30, 0);
            //Cam.transform.DOMoveY(26, 1f); //= new Vector3(0, 30, 0);
            Cam.transform.DORotate(new Vector3(0, 0, 0), 1f); //= Quaternion.Euler(90, 0, 0);
            _light.transform.rotation = Quaternion.Euler(10, -20, 0); //new Vector3(140, 0, 0)
            Cam.orthographic = true;
            Cam.orthographicSize = orthographicSize;
        }
        else
        {
            Cam.transform.DOMoveY(_2DY, 1f); //= new Vector3(0, 25, -15);
            //Cam.transform.DOMoveY(-13f, 1f); //= new Vector3(0, 25, -15);
            Cam.transform.DORotate(new Vector3(-45, 0, 0), 1f); //= Quaternion.Euler(65, 0, 0);
            _light.transform.rotation = Quaternion.Euler(-10, 30, 0); //new Vector3(130, 30, 0)
            Cam.orthographic = false;
            if (test)
            {
                Cam.fieldOfView = 13;
            }
        }
    }
}
