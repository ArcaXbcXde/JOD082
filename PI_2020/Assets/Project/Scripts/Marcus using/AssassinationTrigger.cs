using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinationTrigger : MonoBehaviour {
	
	private JogadorControl controls;

	private void Awake () {

		controls = GetComponentInParent<JogadorControl>();
	}

	private void OnTriggerEnter (Collider other) {

		if (other.gameObject.name == "BackStabArea") {

			Debug.Log ("got: " + other);
			Debug.Log("parent: " + other.transform.parent.gameObject);
			controls.assassinavel = true;
			controls.alvoAtual = other.transform.parent.gameObject;
		}
	}

	private void OnTriggerExit (Collider other) {

		if (other.gameObject.name == "BackStabArea") {

			controls.assassinavel = false;
			controls.alvoAtual = null;
		}
	}
}
