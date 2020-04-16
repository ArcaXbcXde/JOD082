using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float sensibilidade = 5.0f, suavidade = 2.0f, limiteSuperior = 80.0f, limiteInferior = -55.0f;
    
    private float camX, camY;

    private Vector2 mouseDir, suaveV, mouseDelta;

    private GameObject persona;

    // Awake
    void Awake () {

        // Identifica em quem a câmera está presa
        persona = this.transform.parent.gameObject;

        // Giro inicial para começar olhando para o lado desejado
        //persona.transform.localRotation = Quaternion.AngleAxis(180, persona.transform.up);
    }
	
	// Update
	void Update () {
        
        // Controle de variáveis de movimento do mouse na câmera para possível uso futuro
        if (Cursor.lockState == CursorLockMode.Locked) {

            camX = Input.GetAxisRaw("Mouse X");
            camY = Input.GetAxisRaw("Mouse Y");
        } else {

            camX = camY = 0;
        }
        
        // Movimento do mouse no atual update
        mouseDelta = new Vector2(camX, camY);

        // Suaviza a câmera
        mouseDelta = Vector2.Scale (mouseDelta, new Vector2 (sensibilidade * suavidade, sensibilidade * suavidade));

        // Interpolação linear para dividir entre os updates
        suaveV.x = Mathf.Lerp (suaveV.x, mouseDelta.x, 1.0f / suavidade);
        suaveV.y = Mathf.Lerp (suaveV.y, mouseDelta.y, 1.0f / suavidade);

        // Efetua a suavidade no movimento do mouse antes de efetuar na rotação do personagem
        mouseDir.x += suaveV.x;

        // Efetua a suavidade no movimento do mouse antes de efetuar na rotação da câmera, com limitação de ângulo
        if ((mouseDir.y < limiteSuperior && mouseDir.y > limiteInferior) || (mouseDir.y >= limiteSuperior && suaveV.y < 0) || (mouseDir.y <= limiteInferior && suaveV.y > 0)) {
            
            mouseDir.y += suaveV.y;
            if (mouseDir.y > limiteSuperior) {

                mouseDir.y = limiteSuperior;
            }else if (mouseDir.y < limiteInferior) {

                mouseDir.y = limiteInferior;
            }
        }

        //efetua o movimento do Y do mouse no Y da câmera
        transform.localRotation = Quaternion.AngleAxis(-mouseDir.y, Vector3.right);
        
        //efetua o movimento no X do personagem de acordo com o X do mouse
        persona.transform.localRotation = Quaternion.AngleAxis (mouseDir.x, persona.transform.up);
	}
    
}
