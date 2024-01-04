using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MovingPlaform : MonoBehaviour
{
    [SerializeField] private WaypointPath _waypointPath;
    [SerializeField] private float _speed;

    private PlayerMovement _player;

    public bool IsOn = false;
    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWayPoint;
    private float _elapsedTime;

    public static MovingPlaform Instance;

    private void Awake()
    {
        Instance = this;
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        TargetNextWaypoint();
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPercentage = _elapsedTime / _timeToWayPoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);

        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWayPoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWayPointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWayPoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWayPoint = distanceToWaypoint / _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ãæµ¹µÊ");
        _player.OnPlatform = true;
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        _player.OnPlatform = false;
        other.transform.SetParent(null);
    }
}
