using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard2 : GuardStates
{
    protected override void Start(){
        base.Start();
    }

    protected override void Update()
    {
        if(state != State.Sleep){
            CheckForPlayer();
        }
        base.Update();
    }

    private void CheckForPlayer(){
        if(state != State.Chase && Vector3.Distance(transform.position,target.position) < sightDistance && CanSeePlayer()){
            agent.ResetPath();
            chaseMode.SetActive(true);
            state = State.Chase;
        }
        else if(state == State.Chase && Vector3.Distance(transform.position,target.position) > sightDistance && !CanSeePlayer() ){
            agent.ResetPath();
            chaseMode.SetActive(false);
            state = State.Patrol;
        }
    }

    private bool CanSeePlayer(){
        Vector3 directionToTarget = target.transform.position - transform.position;
        RaycastHit rayCastInfo;
            if (Physics.Raycast(transform.position, directionToTarget, out rayCastInfo)){
                if (rayCastInfo.transform.gameObject.tag == "Player"){
                    return true;
                }
            }
        return false;
    }

}
