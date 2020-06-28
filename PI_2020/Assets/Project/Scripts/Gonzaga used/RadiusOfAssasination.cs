using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusOfAssasination : MonoBehaviour {

    public List<GameObject> objectsList = new List<GameObject>();
    public string m_objectTag = "Guard";

    private void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag.Equals(m_objectTag)) {

            objectsList.Add(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col) {

        if (col.gameObject.tag.Equals(m_objectTag)) {

            objectsList.Remove(col.gameObject);
        }
    }
	
}
