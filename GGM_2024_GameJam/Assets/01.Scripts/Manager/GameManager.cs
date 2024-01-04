using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerMovement _player;
    public PlayerMovement PlayerMovement { get { return _player; } private set { } }
    private Camera _cam;
    private Light _light;
    private CinemachineVirtualCamera _2DCam;
    private CinemachineVirtualCamera _3DCam;

  //  [SerializeField] private PlayerSupporter _playerSupporter;      // �÷��̾� �����ͷ� ��. ����

    private float timeSinceLastSwitch = 0f;
    [SerializeField] private float switchCooldown = 1f;

    private bool _canConvert = true;
    public bool CanConvert
    {
        get => _canConvert;
        set => _canConvert = value;
    }

    public static GameManager Instance;


    // �̰� ���� 3D���� 2D���� Ȯ�����ִ� �Һ�����
    [HideInInspector] public bool Is3D = false;

    [SerializeField] private float _3DY, _2DY;
    public bool cannotAttack = false;

    private Vector3 _2DGravity = new Vector3(0, -9.8f, 0);
    private Vector3 _3DGravity = new Vector3(0, 0, 0);

    public void Awake()
    {
        Instance = this;
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

    void Update()
    {
        Key();

        timeSinceLastSwitch += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q) && CanConvert && !_player.OnPlatform && timeSinceLastSwitch >= switchCooldown)
        {
            Is3D = !Is3D;
            GravityConvert();
            SwitchCamera();
            timeSinceLastSwitch = 0f; // Reset the cooldown timer

           // _playerSupporter.ChangeState(Is3D);
        }
        // �̰� ���� ������ ������ �־�α�!
        //Cam.transform.DOMoveX(_player.transform.position.x, 2f);
    }

    void Key()
    {
        if (Input.GetKeyDown(KeyCode.R))
            _player.IsDead = true;

        if (Input.GetKeyDown(KeyCode.T))
            cannotAttack = !cannotAttack;
    }

    public void GravityConvert()
    {
        if (Is3D == false)
        {
            Physics.gravity = _2DGravity;
            _light.transform.DORotate(new Vector3(40, -30, 0), 1f);
        }
        else
        {
            Physics.gravity = _3DGravity;
            _light.transform.DORotate(new Vector3(40, 0, 40), 1f);
            _light.intensity = 0.7f;
        }
    }

    void SwitchCamera()
    {
        // ���׸ӽ� �극���� �����ɴϴ�.
        var cineMachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        // �극�ο� ���� ������ �߰��մϴ�.
        cineMachineBrain.m_DefaultBlend = new CinemachineBlendDefinition
        {
            m_Style = CinemachineBlendDefinition.Style.Linear,
            m_Time = 1.0f     // ��ȯ�� �ɸ��� �ð��� �����մϴ�.
        };

        // 2D ī�޶�� 3D ī�޶��� �켱������ ��ȯ�մϴ�.
        _2DCam.Priority = (_2DCam.Priority + 1) % 2;
        _3DCam.Priority = (_3DCam.Priority + 1) % 2;
    }
}
