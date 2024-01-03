using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private PlayerMovement _player;
    public PlayerMovement PlayerMovement { get { return _player; } private set { } }
    private Camera _cam;
    private Light _light;
    private CinemachineVirtualCamera _2DCam;
    private CinemachineVirtualCamera _3DCam;

    private float timeSinceLastSwitch = 0f;
    [SerializeField] private float switchCooldown = 1f;

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
        _3DCam = GameObject.Find("3DCam").GetComponent<CinemachineVirtualCamera>();
        _2DCam = GameObject.Find("2DCam").GetComponent<CinemachineVirtualCamera>();
        _player = FindObjectOfType<PlayerMovement>();
        _light = FindObjectOfType<Light>();
    }

    private void Start()
    {
        Physics.gravity = _2DGravity;
        _2DCam.Priority = 1;
        _3DCam.Priority = 0;
    }

    void LateUpdate()
    {
        timeSinceLastSwitch += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.K) && CanConvert && timeSinceLastSwitch >= switchCooldown)
        {
            //Debug.Log("시점변경");
            Is3D = !Is3D;
            GravityConvert();
            SwitchCamera();
            timeSinceLastSwitch = 0f; // Reset the cooldown timer
        }
        // 이거 위에 이프문 안으로 넣어두기!
        //Cam.transform.DOMoveX(_player.transform.position.x, 2f);
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
            #region 레거시 카메라
            /* Cam.transform.DOMoveY(_3DY, 1f); //= new Vector3(0, 30, 0);
             //Cam.transform.DOMoveY(26, 1f); //= new Vector3(0, 30, 0);
             Cam.transform.DORotate(new Vector3(0, 0, 0), 1f); //= Quaternion.Euler(90, 0, 0);
             _light.transform.rotation = Quaternion.Euler(10, -20, 0); //new Vector3(140, 0, 0)
            Cam.orthographic = true;
            Cam.orthographicSize = orthographicSize;*/
            #endregion

            #region vCam 1
            _2DCam.transform.DOMoveY(_2DY, 1f);
            _2DCam.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
            _2DCam.m_Lens.OrthographicSize = 5;
            //_confiner.enabled = true;
            #endregion

            _2DCam.Priority = 10;
            _3DCam.Priority = 0;
        }
        else
        {
            #region 레거시 카메라
            /*Cam.transform.DOMoveY(_2DY, 1f); //= new Vector3(0, 25, -15);
            //Cam.transform.DOMoveY(-13f, 1f); //= new Vector3(0, 25, -15);
            Cam.transform.DORotate(new Vector3(-45, 0, 0), 1f); //= Quaternion.Euler(65, 0, 0);
            _light.transform.rotation = Quaternion.Euler(-10, 30, 0); //new Vector3(130, 30, 0)*/

            /*Cam.orthographic = false;
            if (test)
            {
                Cam.fieldOfView = 13;
            }*/
            #endregion

            #region VCam 1
            _2DCam.transform.DOMoveY(_3DY, 1f);
            _2DCam.transform.DORotate(new Vector3(-25, 0, 0), 0.5f);
            _2DCam.m_Lens.OrthographicSize = 9;
            //_confiner.enabled = false;
            #endregion

            _2DCam.Priority = 0;
            _3DCam.Priority = 10;
        }
    }

    void SwitchCamera()
    {
        // 씨네머신 브레인을 가져옵니다.
        var cineMachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        // 브레인에 블렌딩 설정을 추가합니다.
        cineMachineBrain.m_DefaultBlend = new CinemachineBlendDefinition
        {
            m_Style = CinemachineBlendDefinition.Style.Linear,
            m_Time = 1.0f     // 전환에 걸리는 시간을 조절합니다.
        };

        // 2D 카메라와 3D 카메라의 우선순위를 전환합니다.
        _2DCam.Priority = (_2DCam.Priority + 1) % 2;
        _3DCam.Priority = (_3DCam.Priority + 1) % 2;
    }
}
