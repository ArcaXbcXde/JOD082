using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour {
    
    public Image barraHp;
    public Image barraStamina;
    public float maxHp = 100.0f;
    public float maxStamina = 100.0f;
    public float velRegenHp = 0.5f;
    public float velDrenoStamina = 20.0f;
    public float velRegenStamina = 30.0f;
    public float velCorrida = 20;
    public float hp;
    public float stamina;

    private float velAndar;
    public bool correndo = false;

    private void Awake() {

        hp = maxHp;
        stamina = maxStamina;

        velAndar = GetComponent<JogadorControl>().vel;
        
    }

    private void Update() {

        //Controle de corrida
        ControleCorrida();

        // Regeneração de Hp
        //RegenRecurso(hp, maxHp, velRegenHp);
        if (hp < maxHp) {

            hp += velRegenHp * Time.deltaTime;
        } else if (hp > maxHp) {

            hp = maxHp;
        }

        // Controle das barras
        ControleBarra(barraHp, hp, maxHp);
        ControleBarra(barraStamina, stamina, maxStamina);
    }

    // Método que controla a corrida do jogador
    private void ControleCorrida () {

        if (Input.GetKey(KeyCode.LeftShift)) {
            if (stamina > 0) {

                correndo = true;

                // Gasto de estamina
                //RegenRecurso(stamina, maxStamina, -velDrenoStamina);
                if (stamina <= maxStamina) {

                    stamina += -velDrenoStamina * Time.deltaTime;
                } else if (stamina > maxStamina) {

                    stamina = maxStamina;
                }

                // Muda a velocidade do jogador para ele correr
                GetComponent<JogadorControl>().vel = velCorrida;
            } else {

                correndo = false;

                // Muda a velocidade do jogador para ele correr
                GetComponent<JogadorControl>().vel = velAndar;
            }
        } else {

            correndo = false;

            // Regeneração de estamina
            //RegenRecurso(stamina, maxStamina, velRegenStamina);
            if (stamina < maxStamina) {

                stamina += velRegenStamina * Time.deltaTime;
            } else if (stamina > maxStamina) {

                stamina = maxStamina;
            }

            // Muda a velocidade do jogador para ele andar
            GetComponent<JogadorControl>().vel = velAndar;

        }
    }

    // Método que controla regeneração dos recursos
    /*private void RegenRecurso (float recurso, float maxRecurso, float velRegenRecurso) {

        // Se o recurso não está no máximo, vai enchendo aos poucos dependendo da sua velocidade de regeneração, caso contrário trava no máximo
        if (recurso <= maxRecurso) {

            recurso += velRegenRecurso * Time.deltaTime;
        } else if (recurso > maxRecurso) {

            recurso = maxRecurso;
        }
    }*/

    // Método para controlar o quão cheio as barras da IU estão
    private void ControleBarra (Image barra, float recurso, float maxRecurso) {

        // Se o recurso não está abaixo de 0, deixa em 0 para evitar que a barra cresça para o outro lado
        if (recurso < 0) {

            recurso = 0;
        }
        // Enche a barra de acordo com o quão cheio o recurso está
        barra.fillAmount = (recurso / maxRecurso);
    }
}
