using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard2 : GuardStates
{

    protected override void Update()
    {
        CheckForPlayer();
        base.Start();
    }

    private void CheckForPlayer(){
        if(state != State.Chase && Vector3.Distance(transform.position,target.position) < sightDistance){
            agent.ResetPath();
            chaseMode.SetActive(true);
            state = State.Chase;
        }
        else{
            if(state != State.Patrol){
                chaseMode.SetActive(false);
                state = State.Patrol;
            }
        }
    }
}
