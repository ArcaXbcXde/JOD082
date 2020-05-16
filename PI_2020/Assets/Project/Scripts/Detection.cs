using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class Detection : MonoBehaviour
{
    public string m_hitTag = "Player";
    public bool playerInSight;
    public Vector3 lastPlayerSighPosition;

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
            Debug.DrawLine(transform.position + transform.up, col.transform.position, Color.white);
            Vector3 _direction = col.transform.position - transform.position;
            float _angle = Vector3.Angle(_direction, transform.forward);
            if (_angle < m_angleView * 0.5f)
            {
                //Debug.DrawLine(transform.position + transform.up, col.transform.position, Color.cyan);
                //RaycastHit _hit;
                if (Physics.Raycast(transform.position + transform.up, _direction.normalized, out RaycastHit _hit, m_sphereCollider.radius))
                {
                    Debug.DrawLine(transform.position + transform.up, _hit.transform.position, Color.cyan);

                    if (_hit.transform.CompareTag(m_hitTag))
                    {
                        playerInSight = true;
                        lastPlayerSighPosition = col.transform.position;
                        Debug.DrawLine(transform.position + transform.up, _hit.transform.position, Color.red);
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
    private void OnDrawGizmos()
    {
        //Quaternion leftRayRotation = Quaternion.AngleAxis(-m_angleView * 0.5f, Vector3.up);
        //Quaternion rightRayRotation = Quaternion.AngleAxis(m_angleView * 0.5f, Vector3.up);

        //Vector3 leftRayDirection = leftRayRotation * transform.forward;
        //Vector3 rightRayDirection = rightRayRotation * transform.forward;
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawRay(transform.position + transform.up, transform.forward * m_radius);
        ////Gizmos.color = Color.yellow;
        //Gizmos.DrawRay(transform.position + transform.up, leftRayDirection * m_radius);
        ////Gizmos.color = Color.green;
        //Gizmos.DrawRay(transform.position + transform.up, rightRayDirection * m_radius);
    }
}
