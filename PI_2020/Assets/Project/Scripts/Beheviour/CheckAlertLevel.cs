using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAlertLevel : StateMachineBehaviour
{
    EntitieGuard m_entitieGuard;
    public string boolParameter = "Searching";
    public float m_alertLevelThreshold = 25;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_entitieGuard = animator.GetComponent<EntitieGuard>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_entitieGuard.m_AlertLevel > m_alertLevelThreshold)
        {
            animator.SetBool(boolParameter, true);
        }
        if (m_entitieGuard.m_AlertLevel < m_alertLevelThreshold)
        {
            animator.SetBool(boolParameter, false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
