using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : StateMachineBehaviour {

	EntitieGuard m_entitieGuard;
	NavMeshAgent m_navMeshAgent;
	string BoolParameter = "Patrol";

	#region StateMachineBehaviour
	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//animator.SetBool(BoolParameter, true);
		//m_entitieGuard.m_navMeshAgent.isStopped = false;
		m_entitieGuard = animator.GetComponent<EntitieGuard>();
		m_navMeshAgent = animator.GetComponent<NavMeshAgent>();

		SetPathIndex();
		GoToPath();
	}

	//OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
		if (!m_entitieGuard.m_navMeshAgent.pathPending && m_entitieGuard.m_navMeshAgent.remainingDistance < 0.5f) {

			animator.SetBool(BoolParameter, false);
		}

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//m_entitieGuard.m_navMeshAgent.isStopped = true;
	}
	#endregion
	public void GoToPath () {

		if (m_entitieGuard.m_patrol.pathTransform.Length > 0) {

			m_navMeshAgent.destination = m_entitieGuard.m_patrol.pathTransform[m_entitieGuard.m_patrol.pathIdex].position;
		}

	}
	public void SetPathIndex () {

		if (m_entitieGuard.m_patrol.pathTransform.Length == 0) {

			return;
		}

		if (m_entitieGuard.m_patrol.pathIdex != m_entitieGuard.m_patrol.pathTransform.Length) {

			m_entitieGuard.m_patrol.pathIdex++;
			return;

		} else if (m_entitieGuard.m_patrol.pathIdex == m_entitieGuard.m_patrol.pathTransform.Length) {

			m_entitieGuard.m_patrol.pathIdex = 0;
		}
	}

}