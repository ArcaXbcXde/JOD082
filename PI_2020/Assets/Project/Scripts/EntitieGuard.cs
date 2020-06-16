using ProGrids;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//esse script contei as variaveis usada no animator behevior e algumas funçoes "globais" para essa classe 
public class EntitieGuard : EntitiesBasic {
    public enum IAStates { Patrol, Alert, Search, kill }
    public IAStates enumIAStates = IAStates.Patrol;

    public float m_AlertLevel = 0;
    public float m_maxAlertLevel = 100;
    public float m_AlertMult = 2;
    float time = 0;

    #region class
    [System.Serializable]
    public class Patrol
    {
        public Transform[] pathTransform;
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

    public NavMeshAgent m_navMeshAgent;
    public Animator m_anim;



    // Start is called before the first frame update
    void Start()
    {
        #region DetectionVision
        m_vision.detection = transform.GetChild(2).GetComponent<Detection>();
        #endregion
        m_anim = GetComponent<Animator>();
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_navMeshAgent.autoBraking = true;
    }

    // Update is called once per frame
    void Update()
    {
        AlertLevel();
    }

    //almentar o nivel de alerta quando o player estar na area de dectecção do quarda
    void AlertLevel()
    {
        if (m_vision.detection.playerInSight)
        {
            m_AlertLevel += Time.deltaTime * m_AlertMult;
            m_AlertLevel = Mathf.Clamp(m_AlertLevel, 0, 100);
            
        }
        if (!m_vision.detection.playerInSight)
        {
            m_AlertLevel -= Time.deltaTime * m_AlertMult;
            m_AlertLevel = Mathf.Clamp(m_AlertLevel, 0, 100);
        }
        m_anim.SetFloat("AlertLevel", m_AlertLevel);
    }

    #region patrol

    //public void GoToPath()
    //{
    //    if (m_patrol.pathTransform.Length > 0)
    //    {
    //        m_navMeshAgent.destination = m_patrol.pathTransform[m_patrol.pathIdex].position;
    //    }
        
    //}
    //public void SetPathIndex()
    //{
    //    if (m_patrol.pathTransform.Length == 0)
    //    {
    //        return;
    //    }

    //    if (m_patrol.pathIdex != m_patrol.pathTransform.Length)
    //    {
    //        m_patrol.pathIdex++;
    //        return;

    //    }
    //    else if (m_patrol.pathIdex == m_patrol.pathTransform.Length)
    //    {
    //        m_patrol.pathIdex = 0;

    //    }
    //}

    #endregion

    #region gizmos 
    private void OnDrawGizmos()
    {

        GizmoDetection();
        if (m_patrol.pathTransform.Length > 0)
        {
            GizmoPath();
        }
        
    }

    void GizmoDetection()
    {
        float _vision = m_vision.viewAngle * 0.5f;
        float _distance = m_vision.viewDistance;

        Quaternion leftRayRotation = Quaternion.AngleAxis(-_vision, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(_vision, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.color = Color.yellow;
        //Gizmos.DrawRay(transform.position , transform.forward * 5);
        //Gizmos.color = Color.cyan;
        Gizmos.DrawRay(m_vision.detection.transform.position, leftRayDirection * _distance);
        //Gizmos.color = Color.cyan;
        Gizmos.DrawRay(m_vision.detection.transform.position, rightRayDirection * _distance);
        Gizmos.DrawWireSphere(m_vision.detection.transform.position, _distance);
    }
    void GizmoPath()
    {
        int _pathLength = m_patrol.pathTransform.Length - 1;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_patrol.pathTransform[0].position, m_patrol.pathTransform[_pathLength].position);
        for (int i = 0; i < m_patrol.pathTransform.Length; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(m_patrol.pathTransform[i].position, Vector3.one * 0.25f);


            if (m_patrol.pathTransform.Contains(m_patrol.pathTransform[i + 1]))
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(m_patrol.pathTransform[i].position, m_patrol.pathTransform[i + 1].position);
            }


        }
    }
    #endregion
}
