using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : StateMachineBehaviour
{

    EntitieGuard m_entitieGuard;
    NavMeshAgent m_navMeshAgent;
    string BoolParameter = "Patrol";

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_entitieGuard = animator.GetComponent<EntitieGuard>();
        m_navMeshAgent = animator.GetComponent<NavMeshAgent>();
        SetPathIndex();
        GoToPath();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_entitieGuard.m_navMeshAgent.remainingDistance < 2f)
        {
            animator.SetBool(BoolParameter, false);
        }
    }
    
    public void GoToPath()
    {
        if (m_entitieGuard.m_patrol.pathGroup.childCount > 0)
        {
            m_navMeshAgent.destination = m_entitieGuard.m_patrol.pathGroup.GetChild(m_entitieGuard.m_patrol.pathIdex).position;
        }
        else
        {
            m_navMeshAgent.destination = m_entitieGuard.m_patrol.pathGroup.position;
        }
    }

    public void SetPathIndex()
    {
        if (m_entitieGuard.m_patrol.pathGroup.childCount == 0)
        {
            return;
        }

        if (m_entitieGuard.m_patrol.reverseEnabled)
        {
            if (m_entitieGuard.m_patrol.pathIdex == 0)
            {
                m_entitieGuard.m_patrol.reverse = false;
                m_entitieGuard.m_patrol.pathIdex++;
                return;
            }
            if (m_entitieGuard.m_patrol.reverse)
            {
                if (m_entitieGuard.m_patrol.pathIdex - 1 >= 0)
                {
                    m_entitieGuard.m_patrol.pathIdex--;
                    return;
                }
            }
            if (m_entitieGuard.m_patrol.pathIdex + 1 == m_entitieGuard.m_patrol.pathGroup.childCount)
            {
                m_entitieGuard.m_patrol.reverse = true;
                m_entitieGuard.m_patrol.pathIdex--;
                return;
            }
        }

        if (m_entitieGuard.m_patrol.pathIdex + 1 < m_entitieGuard.m_patrol.pathGroup.childCount)
        {
            m_entitieGuard.m_patrol.pathIdex++;
            return;

        }
        else if (m_entitieGuard.m_patrol.pathIdex + 1 == m_entitieGuard.m_patrol.pathGroup.childCount)
        {
            m_entitieGuard.m_patrol.pathIdex = 0;
        }
    }
}