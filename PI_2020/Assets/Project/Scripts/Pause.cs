using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    private bool inventory = false;
	private bool pause = false;
    public GameObject player, pauseScreen, canvasInventory, camera3D, cameraInventory;

    void Update() {
        
        // Botões para pausar ou abrir inventário, escolher qual irá fazer a ação
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.E)) {

            // Chama o método que vê se o inventário vai abrir
            ToggleInventory();
        }else if (Input.GetKeyDown(KeyCode.P)) {

			// Chama o método que vê se vai pausar o jogo
			TogglePause();
		}
    }


    public void ToggleInventory() {

        // Abre o inventário, e se já estiver aberto fecha
        inventory = !inventory;

        // Se estiver aberto...
        if (inventory == false) {
			
			player.GetComponent<PlayerMovement>().enabled = true;
			canvasInventory.gameObject.SetActive(false);
			cameraInventory.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;
			camera3D.GetComponent<Cinemachine.CinemachineFreeLook>().enabled = true;

		// Se não estiver aberto...
		} else {

			player.GetComponent<PlayerMovement>().enabled = false;
			canvasInventory.gameObject.SetActive(true);
			camera3D.GetComponent<Cinemachine.CinemachineFreeLook>().enabled = false;
			cameraInventory.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = true;
        }
    }

	public void TogglePause () {

		pause = !pause;

		if (pause == false) {

			player.GetComponent<PlayerMovement>().enabled = true;
			Time.timeScale = 1;
			pauseScreen.SetActive(false);
		} else {

			player.GetComponent<PlayerMovement>().enabled = false;
			Time.timeScale = 0;
			pauseScreen.SetActive(true);
		}
	}
}
