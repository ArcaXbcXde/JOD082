using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class Detection : MonoBehaviour
{
    public string m_hitTag = "Player";
    public bool playerInSight;
    public Vector3 lastPlayerSighPosition;
    public GameObject playerObj;

    SphereCollider m_sphereCollider;
    EntitieGuard m_entitieGuard;
    [SerializeField]
    float m_angleView;
    private void Awake()
    {
        m_sphereCollider = GetComponent<SphereCollider>();
        m_entitieGuard = transform.GetComponentInParent<EntitieGuard>();

        m_angleView = m_entitieGuard.m_vision.viewAngle;

        m_sphereCollider.radius = m_entitieGuard.m_vision.viewDistance;
        m_sphereCollider.isTrigger = true;

       
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag(m_hitTag))
        {
            playerInSight = false;
            //Debug.DrawLine(transform.position + transform.up, col.transform.position, Color.white);
            Vector3 _direction = col.transform.position - transform.position;
            float _angle = Vector3.Angle(_direction, transform.forward);
            if (_angle < m_angleView * 0.5f)
            {
                //Debug.DrawRay(transform.position, _direction, Color.white);
                //RaycastHit _hit;
                if (Physics.Raycast(transform.position, _direction.normalized, out RaycastHit _hit, m_sphereCollider.radius))
                {
                    Debug.DrawRay(transform.position, _direction, Color.cyan);

                    if (_hit.transform.CompareTag(m_hitTag))
                    {
                        playerInSight = true;
                        lastPlayerSighPosition = col.transform.position;
                        playerObj = col.gameObject;
                        Debug.DrawRay(transform.position, _direction, Color.red);
                    }
                    
                }
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag.Equals(m_hitTag))
        {
            playerInSight = false;
        }
            
    }
}
