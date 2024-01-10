using Unity.VisualScripting;
using UnityEngine;

public class MovingPlaform : MonoBehaviour
{
    [SerializeField] private WaypointPath _waypointPath;
    [SerializeField] private float _speed;
    [SerializeField] private int _Cnt;
    [SerializeField] private LayerMask _layerMask;

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWayPoint;
    private float _elapsedTime;

    private int _objCnt = 0;

    [Header("Collider!")]
    [SerializeField] private BoxCollider _wallCollider;
    [SerializeField] private BoxCollider _myRightCollider;
    [SerializeField] private BoxCollider _myLeftCollider;
    [SerializeField] private bool is_Right = true;
    [SerializeField] private float _openPersent = 0.5f;

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

        //Debug.Log(elapsedPercentage);

        if (elapsedPercentage >= _openPersent/* || elapsedPercentage <= (1 - _openPersent)*/)
        {
            if (is_Right)
            {
                _myRightCollider.enabled = false;
            }
            else
            {
                _myLeftCollider.enabled = false;
            }
        }
        else
        {
            _myRightCollider.enabled = true;
            _myLeftCollider.enabled = true;
        }

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
            is_Right = !is_Right;
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

    private int GetTargetCount()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(5, 5, 5), transform.rotation, _layerMask);
        //Debug.Log(colliders.Length);
        return colliders.Length;
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject);
        GameManager.Instance.PlayerMovement.OnPlatform = true;
        if (GetTargetCount() >= _Cnt)
        {
            //other.transform.SetParent(transform);
            _wallCollider.enabled = false;
            //Debug.Log("들어옴");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.Instance.PlayerMovement.OnPlatform = false;
        //other.transform.SetParent(null);
            _wallCollider.enabled = true;
            //Debug.Log("나가기");
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(5, 5, 5));
    }
}
