using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    //Ao ser chamado vai pra o menu inicial
    public void BtMenu() {

        SceneManager.LoadScene("StartScene");
    }

    //Ao ser chamado vai pra a DevRoom (alterar depois)
    public void BtContinue () {
        
        SceneManager.LoadScene("DevRoom");
    }

    //Ao ser chamado vai pra o menu de opções
    public void BtOptions() {

        SceneManager.LoadScene("OptionsScene");
    }

    //Ao ser chamado fecha a aplicação
    public void BtQuit () {
        
        Application.Quit();
    }
}
