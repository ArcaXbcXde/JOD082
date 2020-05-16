using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class IA_UI : MonoBehaviour
{
    public Image m_alertBar;
    public Text m_stateText;

    EntitieGuard m_entitieGuard;
    private void Awake()
    {
        m_entitieGuard = gameObject.GetComponentInParent<EntitieGuard>();
        m_alertBar = transform.GetChild(0).GetComponent<Image>();
        m_stateText = transform.GetChild(1).GetComponent<Text>();
    }
    private void Update()
    {
        m_alertBar.fillAmount = m_entitieGuard.m_AlertLevel / 100;
        if (m_entitieGuard.m_AlertLevel >= 0 && m_entitieGuard.m_AlertLevel < 25)
        {
            m_alertBar.color = Color.white;
        }
        if (m_entitieGuard.m_AlertLevel >= 25 && m_entitieGuard.m_AlertLevel < 50)
        {
            m_alertBar.color = new Color(255, 255, 0);
        }
        if (m_entitieGuard.m_AlertLevel >= 50 && m_entitieGuard.m_AlertLevel < 75)
        {
            m_alertBar.color = new Color(255, 100, 0);
        }
        if (m_entitieGuard.m_AlertLevel >= 75 && m_entitieGuard.m_AlertLevel < 100)
        {
            m_alertBar.color = Color.red;
        }
    }
}
