using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntitiesBasic : MonoBehaviour
{
    float m_HP;
    public float m_MaxHP = 1;

    public UnityEvent m_DamageEvent;
    public UnityEvent m_DeathEvent;
    public UnityEvent m_HealEvent;
    public UnityEvent m_SpawnEvent;
    
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
        m_HP =-_takeDamage;

        if (IsAlive())
        {
            OnDamage();
        }
        if (!IsAlive())
        {
            OnDeath();
        }
    }


    void OnDeath()
    {
        m_DeathEvent.Invoke();
        Destroy(this.gameObject, 3f);
    }

    void OnDamage()
    {
        m_DamageEvent.Invoke();
    }

    void OnHeal()
    {
        m_HealEvent.Invoke();
    }

    void OnSpawn()
    {
        m_HealEvent.Invoke();
    }
}
