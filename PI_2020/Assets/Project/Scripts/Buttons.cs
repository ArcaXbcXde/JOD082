using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    //Ao ser chamado vai pra o menu inicial
    public void BtMenu() {

        SceneManager.LoadScene("StartMenu");
    }

    //Ao ser chamado vai pra a DevRoom (alterar depois)
    public void BtContinue () {
        
        SceneManager.LoadScene("DevRoom");
    }

    //Ao ser chamado vai pra o menu de opções
    public void BtOptions() {

        SceneManager.LoadScene("Options");
    }

    //Ao ser chamado fecha a aplicação
    public void BtQuit () {
        
        Application.Quit();
    }

    public void BtDevRoom() {

        SceneManager.LoadScene("DevRoom");
    }

    public void BtTests() {

        SceneManager.LoadScene("Tests");
    }
}
