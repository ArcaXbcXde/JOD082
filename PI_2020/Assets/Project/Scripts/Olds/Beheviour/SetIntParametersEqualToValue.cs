using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIntParametersEqualToValue : StateMachineBehaviour
{
    public string m_intParameter = "AlertLevel";
    EntitieGuard m_entitieGuard;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_entitieGuard = animator.GetComponent<EntitieGuard>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(m_intParameter, m_entitieGuard.m_AlertLevel);
    }
}
