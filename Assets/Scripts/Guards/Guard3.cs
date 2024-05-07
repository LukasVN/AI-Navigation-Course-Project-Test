using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard3 : GuardStates
{
    public float maxAngle = 60;
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
        if(state != State.Chase && Vector3.Distance(transform.position,target.position) < sightDistance && CanSeePlayerInAngle() ){
            agent.ResetPath();
            chaseMode.SetActive(true);
            state = State.Chase;
        }
        else if(state == State.Chase && Vector3.Distance(transform.position,target.position) > sightDistance && !CanSeePlayerInAngle() ){
            agent.ResetPath();
            chaseMode.SetActive(false);
            state = State.Patrol;
        }
    }

    private bool CanSeePlayerInAngle(){
            Vector3 directionToTarget = target.transform.position - transform.position;
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget <= maxAngle)
            {
                RaycastHit rayCastInfo;
                if (Physics.Raycast(transform.position, directionToTarget, out rayCastInfo))
                {
                    if (rayCastInfo.transform.gameObject.tag == "Player")
                    {
                        return true;
                    }
                }
            }
        return false;
    }

    void OnDrawGizmos(){
        if (target == null){
            return;
        }

        // Draw a line from this object to the target
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.transform.position);

        // Draw the field of view
        Gizmos.color = Color.yellow;
        
        Vector3 directionToTarget = target.transform.position - transform.position;
        Vector3 viewAngleA = DirFromAngle(-maxAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(maxAngle / 2, false);

        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * sightDistance);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * sightDistance);
        }

        Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal){
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
