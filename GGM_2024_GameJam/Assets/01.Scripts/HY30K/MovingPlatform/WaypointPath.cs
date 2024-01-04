using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform GetWayPoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWayPointIndex(int currentWaypointIndex)
    {
        int nextWaypointInedex = currentWaypointIndex + 1;

        if (nextWaypointInedex == transform.childCount)
        {
            nextWaypointInedex = 0;
        }

        return nextWaypointInedex;
    }
}
