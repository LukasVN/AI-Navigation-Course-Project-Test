using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardStates : MonoBehaviour
{
    protected enum State{Patrol,Chase, Sleep}
    protected State state;
    protected Transform target;
    public GameObject chaseMode;
    public float sightDistance;
    protected NavMeshAgent agent;

    //Waypoint behaviour variables
    public List<Transform> waypoints;
    protected int waypointIndex;
    protected Transform currentWaypoint;
    protected virtual void Start()
    {
        state = State.Patrol;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        waypointIndex = 0;
        GoToWaypoint();
    }

    protected virtual void Update() {
        switch (state)
        {
            case State.Patrol: 
                if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance){
                    GoToWaypoint();
                    if(waypointIndex == waypoints.Count - 1){
                        waypointIndex = 0;
                    }
                    else{
                        waypointIndex++;
                    }
                }

            break;
            case State.Chase: agent.SetDestination(target.position); Debug.Log("Chasing");
            break;
            case State.Sleep: 
                if(agent.hasPath){
                    agent.ResetPath();
                }
            break;
            
        }
    }

    private void GoToWaypoint(){
        if(currentWaypoint != null){
            currentWaypoint.GetComponent<MeshRenderer>().enabled = false;
        }
        currentWaypoint = waypoints[waypointIndex];
        agent.SetDestination(currentWaypoint.position);
        currentWaypoint.GetComponent<MeshRenderer>().enabled = true;
        
    }

}
