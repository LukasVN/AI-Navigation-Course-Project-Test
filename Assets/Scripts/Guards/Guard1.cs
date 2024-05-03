using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard1 : GuardStates
{
    protected override void Start(){
        base.Start();
    }

    protected override void Update()
    {
        CheckForPlayer();
        base.Update();
    }

    private void CheckForPlayer(){
        if(state != State.Chase && Vector3.Distance(transform.position,target.position) < sightDistance){
            agent.ResetPath();
            chaseMode.SetActive(true);
            state = State.Chase;
            Debug.Log("Visto");
        }
        else{
            if(state != State.Patrol){
                chaseMode.SetActive(false);
                state = State.Patrol;
            }
        }
    }
}
