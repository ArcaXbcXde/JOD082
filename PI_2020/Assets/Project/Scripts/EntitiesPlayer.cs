using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EntitiesPlayer : EntitiesComplex {

    public GameObject blood;

    public GameObject m_assasinationTarget;
    public string m_backTag = "Back";
    public LayerMask m_enemyLayer;
    [Space]
    public RadiusOfAssasination m_RadiusOfAssasination;
    public Transform m_raycastPoint;
    [Space]

    Animator m_anim;
    public Transform m_dagger;
    bool canAssasinate = false;

    // Awake
    protected override void Awake()
    {
        base.Awake();
        m_anim = GetComponent<Animator>();
    }

    // Update
    private void Update()
    {
        StartCoroutine(MeleeAttack(2.5f));

        CheckDistanceToAssasinate(1.5f);

        if (m_assasinationTarget == null) {

            gameObject.GetComponent<PlayerMovement>().canMove = true;
        }

        if (blood.GetComponent<ParticleSystem>().time >= 1) {

            blood.gameObject.SetActive(false);
        }
    }

    #region Attack

    // kill guard by melee
    IEnumerator MeleeAttack(float _assasinationTime) {

        if (Input.GetMouseButtonDown(0)) {

            if (canAssasinate) {

                gameObject.GetComponent<PlayerMovement>().canMove = false;
                m_anim.SetTrigger("isAssasinating");
                m_dagger.DOPunchRotation(new Vector3(360, 0, 0), _assasinationTime);
                
                blood.gameObject.SetActive(true);
                blood.transform.position = m_assasinationTarget.transform.position;

                yield return new WaitForSeconds(_assasinationTime);
                m_assasinationTarget.GetComponent<EntitiesComplex>().TakeDamage(10); 
                gameObject.GetComponent<PlayerMovement>().canMove = true;
            }
        }
    }

    // ToDo: kill guard on distance
    void RangeAttack() {

    }


    #endregion



    void CheckDistanceToAssasinate(float _distance)
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
    }

    #region Colliders


    #endregion
}
