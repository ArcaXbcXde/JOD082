using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

//esse script contei as variaveis usada no animator behevior e algumas funçoes "globais" para essa classe 
public class EntitieGuard : EntitiesBasic {
    public enum IAStates { Patrol, Alert, Search, kill }
    public IAStates enumIAStates = IAStates.Patrol;

    public float m_AlertLevel = 0;
    public float m_maxAlertLevel = 100;
    public float m_AlertMultEnter = 40;
    public float m_AlertMultExit = 2;
    float time = 0;

    #region class
    [System.Serializable]
    public class Patrol
    {
        public bool reverseEnabled;
        public bool reverse;
        public Transform pathGroup;
        public int pathIdex = -1;
    }
    [SerializeField]
    public Patrol m_patrol;
    [System.Serializable]
    public class Vision
    {
        public float viewAngle = 90;
        public float viewDistance = 5;
        public bool PlayerInSigth;
        public Detection detection;
    }
    [SerializeField]
    public Vision m_vision;
    #endregion

    public GameObject handSword;
    public GameObject hipSword;

    public NavMeshAgent m_navMeshAgent;
    public Animator m_anim;

    public bool isTheCaptain;


    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.autoBraking = true;
    }

    void Update()
    {
        AlertLevel();
    }

    //almentar o nivel de alerta quando o player estar na area de dectecção do quarda
    void AlertLevel()
    {
        if (m_vision.detection.playerInSight)
        {
            m_AlertLevel += Time.deltaTime * m_AlertMultEnter;
            m_AlertLevel = Mathf.Clamp(m_AlertLevel, 0, 100);            
        }
        if (!m_vision.detection.playerInSight)
        {
            m_AlertLevel -= Time.deltaTime * m_AlertMultExit;
            m_AlertLevel = Mathf.Clamp(m_AlertLevel, 0, 100);
        }
        m_anim.SetFloat("AlertLevel", m_AlertLevel);
    }

    public void Death()
    {        
        m_anim.SetTrigger("Death");
        GetComponent<NavMeshAgent>().speed = 0;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<EntitieGuard>().enabled = false;
        m_vision.detection.enabled = false;
        OnDeath();
        if (isTheCaptain)
        {
            SceneManagement.Instance.VictoryScene(3);
        }
    }
}
