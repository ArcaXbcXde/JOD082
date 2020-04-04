using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
    public void BtContinue () {
        
        SceneManager.LoadScene("DevRoom"); //Ao ser chamado vai pra a DevRoom (alterar depois)
    }

    public void BtMenu () {

        SceneManager.LoadScene("StartMenu"); //Ao ser chamado vai pra o Menu inicial
    }

    public void BtQuit () {
        
        Application.Quit();               //Ao ser chamado fecha a aplicação
    }
    
}
