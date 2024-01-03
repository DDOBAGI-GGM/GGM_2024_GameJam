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

    [SerializeField] private float _3DY, _2DY;
    //[SerializeField] bool test = false;

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
