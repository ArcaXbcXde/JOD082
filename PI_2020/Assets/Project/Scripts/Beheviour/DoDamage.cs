using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    public string m_hitTag = "Player";
    public float m_damage = 7;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_hitTag))
        {
            other.GetComponent<EntitiesPlayer>().TakeDamage(m_damage);
        }
    }
}
