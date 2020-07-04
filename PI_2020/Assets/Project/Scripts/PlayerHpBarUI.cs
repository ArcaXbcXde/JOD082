using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBarUI : MonoBehaviour {

	public Image hpBar;
	
	private JogadorControl playerScript;


    private static PlayerHpBarUI instance;
    public static PlayerHpBarUI Instance { get { return instance; } }

    void Start()
    {
        instance = this;
		playerScript = JogadorControl.Instance;
    }

    public void AttHealth() 
    {
        hpBar.fillAmount = (playerScript.hpCurrent / playerScript.hpMax);
    }
}
