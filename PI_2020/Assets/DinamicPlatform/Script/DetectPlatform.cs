using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DetectPlatform : MonoBehaviour
{
    public string m_tag = "DinamicPlataform";

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals(m_tag))
        {
            col.GetComponent<DinamicTiles>().PlataformOn();
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.Equals(m_tag))
        {
            col.GetComponent<DinamicTiles>().PlataformOff();
        }
    }
}
