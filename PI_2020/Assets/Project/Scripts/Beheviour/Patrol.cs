using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour
{
    EntitieGuard m_entitieGuard;
    string BoolParameter = "Patrol";
    int test = 0;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool(BoolParameter, true);
        m_entitieGuard = animator.GetComponent<EntitieGuard>();
        m_entitieGuard.m_navMeshAgent.isStopped = false;
        m_entitieGuard.SetPathIndex();
        m_entitieGuard.GoToPath();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {       

        if (!m_entitieGuard.m_navMeshAgent.pathPending && m_entitieGuard.m_navMeshAgent.remainingDistance < 0.5f)
        {
           animator.SetBool(BoolParameter, false);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_entitieGuard.m_navMeshAgent.isStopped = true;
    }
    

}
