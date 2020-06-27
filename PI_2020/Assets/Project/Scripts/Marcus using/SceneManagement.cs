using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    //Ao ser chamado vai para
    public void BtMenu() {

        SceneManager.LoadScene("StartMenu");
    }

    //Ao ser chamado vai para 
    public void BtContinue () {
        
        SceneManager.LoadScene("LevelSelection");
    }

    // Ao ser chamado vai para
    public void BtNewGame() {

        SceneManager.LoadScene("LevelSelection");
    }

    //Ao ser chamado vai para 
    public void BtLoadGame() {

        SceneManager.LoadScene("LevelSelection");
    }


    //Ao ser chamado vai para
    public void BtOptions() {

        SceneManager.LoadScene("OptionsScene");
    }

    //Ao ser chamado vai para
    public void BtDevRoom() {

        SceneManager.LoadScene("DevRoom");
    }

    public void BtDevRoomGonzaga() {

        SceneManager.LoadScene("DevRoom_G");
    }

    //Ao ser chamado vai para
    public void BtTests() {

        SceneManager.LoadScene("Tests");
    }


    //Ao ser chamado fecha a aplicação
    public void BtQuit () {
        
        Application.Quit();
    }
}
