using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	private bool inventory = false;
	private bool pause = false;

	private GameObject player;
	public GameObject pauseScreen;
	public GameObject camera3D;
	public GameObject cameraInventory;
	public GameObject canvasInventory;

	private void Awake () {

		// Procura o jogador através da tag "Player"
		player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update () {

		// trava e destrava o cursor com a tecla tab
		if (Input.GetKeyDown(KeyCode.Tab)) {

			// Inverte a trava no cursor do mouse, para aparecer ou desaparecer à vontade
			TogglePointer();
			// Botões para pausar ou abrir inventário, escolher qual irá fazer a ação
		} else if (Input.GetKeyDown(KeyCode.I)) {

			// Chama o método que vê se o inventário vai abrir
			ToggleInventory();
			// Botão para pausar o jogo
		} else if (Input.GetKeyDown(KeyCode.P)) {

			// Chama o método que vê se vai pausar o jogo
			TogglePause();
		}
	}

	// trava e destrava o cursor no jogo
	private void TogglePointer () {

		if (Cursor.lockState == CursorLockMode.Locked) { // Destrava mouse

			UnlockPointer();
		} else if (Cursor.lockState == CursorLockMode.None) { // Trava mouse

			LockPointer();
		}
	}

	// Apenas trava o cursor do mouse
	private void LockPointer () {

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// Apenas destrava o cursor do mouse
	private void UnlockPointer () {

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void ToggleInventory () {

		// Abre o inventário, e se já estiver aberto fecha
		inventory = !inventory;

		// Se estiver aberto fecha...
		if (inventory == false) {

			LockPointer();
			player.GetComponent<PlayerMovement>().enabled = true;
			camera3D.gameObject.SetActive(true);
			canvasInventory.gameObject.SetActive(false);
			cameraInventory.gameObject.SetActive(false);

			// Se não estiver aberto abre...
		} else {

			UnlockPointer();
			player.GetComponent<PlayerMovement>().enabled = false;
			camera3D.gameObject.SetActive(false);
			canvasInventory.gameObject.SetActive(true);
			cameraInventory.gameObject.SetActive(true);
		}
	}

	public void TogglePause () {

		pause = !pause;

		if (pause == false) {

			LockPointer();
			player.GetComponent<JogadorControl>().enabled = true;
			Time.timeScale = 1;
			pauseScreen.SetActive(false);
		} else {

			UnlockPointer();
			player.GetComponent<JogadorControl>().enabled = false;
			Time.timeScale = 0;
			pauseScreen.SetActive(true);
		}
	}
}
