using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EntitiesPlayer : EntitiesComplex 
{

    public GameObject blood;

    
    public string m_backTag = "Back";
    public LayerMask m_enemyLayer;
    [Space]
    //obsoleto   
    //public RadiusOfAssasination m_RadiusOfAssasination;
    //public Transform m_raycastPoint;  
    [Space]
    Animator m_anim;
    //public Transform m_dagger;
    bool canAssasinate = false;
    [Space]
    public GameObject m_assasinationTarget;
    public GameObject headUpsTest;

    // Awake
    protected override void Awake()
    {
        base.Awake();
        m_anim = GetComponent<Animator>();
    }

    // Update
    private void Update()
    {
        Assasination();
    }

    #region Attack

    public void Assasination()
    {
        if (canAssasinate)
        {
            headUpsTest.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                m_anim.SetTrigger("Assasination");
                m_assasinationTarget.GetComponent<Animator>().SetTrigger("Death_Assassination");
            }
        }
        if (!canAssasinate)
        {
            headUpsTest.SetActive(false);
        }
        if (m_assasinationTarget == null)
        {
            headUpsTest.SetActive(false);
        }
        
        
    }

    // kill guard by melee
    //obsoleto
    /*IEnumerator MeleeAttack(float _assasinationTime) 
    {

        if (Input.GetMouseButtonDown(0)) 
        {

            if (canAssasinate) 
            {

                gameObject.GetComponent<PlayerMovement>().canMove = false;
                m_anim.SetTrigger("isAssasinating");
                m_dagger.DOPunchRotation(new Vector3(360, 0, 0), _assasinationTime);

                m_assasinationTarget.GetComponent<TestGuard>().enabled = false;

                blood.gameObject.SetActive(true);
                blood.transform.position = m_assasinationTarget.transform.position;

                yield return new WaitForSeconds(_assasinationTime);
                m_assasinationTarget.GetComponent<EntitiesComplex>().TakeDamage(10); 
                gameObject.GetComponent<PlayerMovement>().canMove = true;
            }
        }
    }*/

    // To Do: kill guard on distance
    void RangeAttack() 
    {

    }
    #endregion


    //obsoleto
    /*void CheckDistanceToAssasinate(float _distance)
    {
        if (m_RadiusOfAssasination.objectsList.Count > 0)
        {
            m_assasinationTarget = m_RadiusOfAssasination.objectsList[0];
        }
        if (m_RadiusOfAssasination.objectsList.Count == 0)
        {
            m_assasinationTarget = null;
        }

        if (Physics.CheckSphere(transform.position, _distance, m_enemyLayer))
        {
            if (m_RadiusOfAssasination.objectsList.Count > 0)
            {
                Ray _playerToEnemy = new Ray(m_raycastPoint.position, m_assasinationTarget.transform.position);
                RaycastHit _hitInfo;

                if (Physics.Linecast(m_raycastPoint.position, m_assasinationTarget.transform.position, out _hitInfo))
                {

                    Debug.Log("you hit... " + _hitInfo.collider.name);
                    if (_hitInfo.collider.tag.Equals("Back"))
                    {
                        Debug.DrawLine(transform.position, m_assasinationTarget.transform.position, Color.red);
                        canAssasinate = true;
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, m_assasinationTarget.transform.position, Color.white);
                        canAssasinate = false;
                    }
                    


                }
            }
            
        }
    }*/

    #region Colliders
    private void OnTriggerEnter(Collider other)
    {
        Transform _obj;
        if (other.CompareTag("Back"))
        {
            canAssasinate = true;
            _obj = other.transform.parent;
            m_assasinationTarget = _obj.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Back"))
        {
            canAssasinate = true;
            headUpsTest.transform.position = other.transform.position + (transform.up * 2.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Back"))
        {
            canAssasinate = false;
            m_assasinationTarget = null;
        }
    }

    #endregion
}
