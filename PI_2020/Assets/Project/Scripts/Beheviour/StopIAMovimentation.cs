using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopIAMovimentation : StateMachineBehaviour
{
    NavMeshAgent m_navMeshAgent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_navMeshAgent = animator.GetComponent<NavMeshAgent>();
        m_navMeshAgent.destination = animator.transform.position;
    }

}
