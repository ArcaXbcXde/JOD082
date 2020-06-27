using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTest : MonoBehaviour {

	public Image hpBar;
	
	//private EntitiesPlayer playerScript;
	private JogadorControl playerScript;

    void Start(){
        
		//playerScript = FindObjectOfType<EntitiesPlayer>();
		playerScript = FindObjectOfType<JogadorControl>();
    }

    // Update is called once per frame
    void Update() {
        
		//hpBar.fillAmount = (playerScript.m_HP / playerScript.m_MaxHP);
		hpBar.fillAmount = (playerScript.hp / playerScript.hpMax);
    }
}
