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
    public GameObject sleepMode;
    public float sightDistance;
    protected NavMeshAgent agent;
    private float agentSpeed;

    //Waypoint behaviour variables
    public List<Transform> waypoints;
    protected int waypointIndex;
    protected Transform currentWaypoint;
    protected virtual void Start()
    {
        state = State.Patrol;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
        waypointIndex = 0;
        GoToWaypoint();
        StartCoroutine("GoToSleep");
    }

    protected virtual void Update() {
        switch (state)
        {
            case State.Patrol: 
                if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance){
                    Stop();
                    GoToWaypoint();
                    if(waypointIndex == waypoints.Count - 1){
                        waypointIndex = 0;
                    }
                    else{
                        waypointIndex++;
                    }
                }

            break;
            case State.Chase: agent.SetDestination(target.position);
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

    IEnumerator GoToSleep(){
        yield return new WaitForSecondsRealtime(Random.Range(10f,21f));
        if(state == State.Chase){
            StopCoroutine("GoToSleep");
            StartCoroutine("GoToSleep");
            yield return null;
        }
        state = State.Sleep;
        sleepMode.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        state = State.Patrol;
        sleepMode.SetActive(false);
        StartCoroutine("GoToSleep");
    }

    private void Stop(){
        if(agent.speed > 0){
            agent.speed = 0;
            StartCoroutine("WaitAndResumePatrol");
        }
    }

    IEnumerator WaitAndResumePatrol(){
        yield return new WaitForSecondsRealtime(5f);
        agent.speed = agentSpeed;
    }


}
