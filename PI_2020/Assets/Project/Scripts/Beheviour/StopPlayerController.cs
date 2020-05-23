using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerController : StateMachineBehaviour
{
    PlayerMovement m_playerMovement;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_playerMovement = animator.GetComponent<PlayerMovement>();
        m_playerMovement.canMove = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_playerMovement.canMove = true;
    }
}
