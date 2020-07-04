using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinationTrigger : MonoBehaviour {
	
	private JogadorControl controls;

	private void Start () 
	{
		controls = JogadorControl.Instance.gameObject.GetComponent<JogadorControl>();
	}

	private void OnTriggerEnter (Collider other) 
	{
		if (other.gameObject.name == "BackStabArea") 
		{
			if (other.transform.parent.GetComponent<EntitieGuard>())
			{
				if (other.transform.parent.GetComponent<EntitieGuard>().enabled)
				{
					controls.assassinavel = true;
					controls.alvoAtual = other.transform.parent.gameObject;
				}
			}
		}
	}

	private void OnTriggerExit (Collider other) 
	{
		if (other.gameObject.name == "BackStabArea") 
		{

			controls.assassinavel = false;
			controls.alvoAtual = null;
		}
	}
}
