using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntitiesBasic : MonoBehaviour
{
    public float m_HP = 0;
    public float m_MaxHP = 1;
    
    [System.Serializable]
    class M_Event
    {
        public UnityEvent damage;
        public UnityEvent death;
        public UnityEvent heal;
        public UnityEvent spawn;

    }
    [SerializeField]
    M_Event m_events;

    protected bool IsAlive()
    {
        if (m_HP <= 0)
        {
            return false;
        }
        return true;
    }

    protected virtual void Awake()
    {
        m_HP = m_MaxHP;
    }

    //in progress
    public virtual void TakeHeal(float _takeHeal)
    {
        if (IsAlive())
        {
            m_HP = +_takeHeal;
            if (m_HP > m_MaxHP)
            {
                m_HP = m_MaxHP;
            }
            OnHeal();
        }
        if (!IsAlive())
        {
            return;
        }

    }

    public virtual void TakeDamage(float _takeDamage)
    {

        if (IsAlive())
        {
            m_HP = m_HP - _takeDamage;
            OnDamage();
            
        }
        if (!IsAlive())
        {
            OnDeath();
        }
    }


    void OnDeath()
    {
        m_events.death.Invoke();
        Destroy(this.gameObject, 3f);
    }

    void OnDamage()
    {
        m_events.damage.Invoke();
    }

    void OnHeal()
    {
        m_events.heal.Invoke();
    }

    void OnSpawn()
    {
        m_events.spawn.Invoke();
    }
}
