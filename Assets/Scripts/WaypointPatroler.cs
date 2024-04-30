using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatroler : MonoBehaviour
{
    private NavMeshAgent agent;
    public List<Transform> waypoints;
    private int waypointIndex;
    private Transform currentWaypoint;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        waypointIndex = 0;
        GoToWaypoint();
    }

    void Update()
    {
        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance){
            GoToWaypoint();
            if(waypointIndex == waypoints.Count - 1){
                waypointIndex = 0;
            }
            else{
                waypointIndex++;
            }
        }
    }

    private void GoToWaypoint(){
        if(currentWaypoint != null){
            currentWaypoint.GetComponent<MeshRenderer>().enabled = false;
        }
        currentWaypoint = waypoints[waypointIndex];
        Debug.Log("Current Waypoint: "+waypoints[waypointIndex]);
        agent.SetDestination(currentWaypoint.position);
        currentWaypoint.GetComponent<MeshRenderer>().enabled = true;
        
    }
}
