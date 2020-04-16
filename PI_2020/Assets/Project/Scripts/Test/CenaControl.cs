using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenaControl : MonoBehaviour {

	void Update () {
        
        // Trava do cursor do mouse
        TravaCursor();
    }

    // trava o cursor no jogo
    private void TravaCursor() {

        // trava e destrava com a tecla tab
        if (Input.GetKeyDown(KeyCode.Tab)) {
            
            if (Cursor.lockState == CursorLockMode.Locked) { // destrava mouse

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else if (Cursor.lockState == CursorLockMode.None) { // trava mouse

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
