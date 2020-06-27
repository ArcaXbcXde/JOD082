using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : StateMachineBehaviour
{
    EntitiesBasic m_entitiesBasic;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_entitiesBasic = animator.GetComponent<EntitiesBasic>();
        m_entitiesBasic.TakeDamage(m_entitiesBasic.m_MaxHP);
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
