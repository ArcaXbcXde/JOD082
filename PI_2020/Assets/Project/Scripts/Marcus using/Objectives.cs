using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objectives : MonoBehaviour {
	
	public GameObject player;
	public GameObject guardCaptain;

	void Update () {
		
		if (player.GetComponent<JogadorControl>().hp <= 0) {
			Invoke ("GoToDefeatScene", 3);
		}
		if (guardCaptain.activeSelf == false) {
			Invoke("GoToVictoryScene", 3);
		}
	}

	private void GoToDefeatScene () {

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		SceneManager.LoadScene ("DefeatScene");
	}

	private void GoToVictoryScene () {

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		SceneManager.LoadScene ("VictoryScene");
	}
}
