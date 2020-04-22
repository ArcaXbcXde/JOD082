using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {

    //Update
	void Update () {
        
        // Trava do cursor do mouse
        LockPointer();
    }

    // trava o cursor no jogo
    private void LockPointer() {

        // trava e destrava com a tecla tab
        if (Input.GetKeyDown(KeyCode.Tab)) {
            
            if (Cursor.lockState == CursorLockMode.Locked) { // Destrava mouse

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else if (Cursor.lockState == CursorLockMode.None) { // Trava mouse

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
