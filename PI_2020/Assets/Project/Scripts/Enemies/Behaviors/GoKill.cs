using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoKill : StateMachineBehaviour {

	public string triggerParameter = "Attacking";
	EntitieGuard m_entitieGuard;
	public GameObject m_tagert;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//m_entitieGuard.m_navMeshAgent.isStopped = false;
		m_entitieGuard = animator.GetComponent<EntitieGuard>();
		//m_entitieGuard.m_navMeshAgent.stoppingDistance = 2f;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		m_tagert = m_entitieGuard.m_vision.detection.playerObj;

		m_entitieGuard.m_navMeshAgent.destination = m_tagert.transform.position;

		if (m_entitieGuard.m_navMeshAgent.remainingDistance < 4f && m_entitieGuard.m_vision.PlayerInSigth) {
			animator.SetTrigger(triggerParameter);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		m_entitieGuard.m_navMeshAgent.stoppingDistance = 1f;
	}
}