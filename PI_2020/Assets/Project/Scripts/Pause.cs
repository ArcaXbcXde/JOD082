using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    private bool paused = false;
    public GameObject player, canvasPause, camera3D, cameraPause;

    void Update() {
        
        // Botões para pausar ou abrir inventário, escolher qual irá fazer a ação
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.E)) {

            // Chama o método que irá pausar o jogo
            TogglePause();
        }
    }


    public void TogglePause() {

        // Pausa e se estiver pausado despausa
        paused = !paused;

        // Se estiver pausado...
        if (paused == true) {

            player.GetComponent<PlayerMovement>().enabled = false;
            canvasPause.gameObject.SetActive(true);
            camera3D.GetComponent<Cinemachine.CinemachineFreeLook>().enabled = false;
            cameraPause.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = true;


            // Se não estiver pausado...
        } else {

            player.GetComponent<PlayerMovement>().enabled = true;
            canvasPause.gameObject.SetActive(false);
            cameraPause.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;
            camera3D.GetComponent<Cinemachine.CinemachineFreeLook>().enabled = true;
        }
    }
}
