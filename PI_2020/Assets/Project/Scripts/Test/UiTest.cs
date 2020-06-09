using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTest : MonoBehaviour {

	public Image hpBar;
	
	private EntitiesPlayer playerScript;

    void Start(){
        
		playerScript = FindObjectOfType<EntitiesPlayer>();
    }

    // Update is called once per frame
    void Update() {
        
		hpBar.fillAmount = (playerScript.m_HP / playerScript.m_MaxHP);
    }
}
