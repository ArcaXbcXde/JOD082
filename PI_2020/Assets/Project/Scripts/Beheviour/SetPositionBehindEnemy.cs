using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionBehindEnemy : StateMachineBehaviour
{
    EntitiesPlayer m_entitiesPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_entitiesPlayer = animator.GetComponent<EntitiesPlayer>();
        Transform _enemyPosition = m_entitiesPlayer.m_assasinationTarget.transform;
        animator.transform.position = _enemyPosition.position + (_enemyPosition.forward * -1.5f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
