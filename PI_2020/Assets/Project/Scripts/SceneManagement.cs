using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    private static SceneManagement instance;
    public static SceneManagement Instance { get { return instance; } }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            BtOldGame();
        }
    }


    //Ao ser chamado vai para
    public void BtMenu() {

        SceneManager.LoadScene("StartMenu");
    }

    //Ao ser chamado vai para 
    public void BtContinue () {
        
        SceneManager.LoadScene("LevelSelection");
    }

    // Ao ser chamado vai para
    public void BtNewGame()
    {

        SceneManager.LoadScene("Post");
    }

    public void BtOldGame()
    {

        SceneManager.LoadScene("DevRoom");
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

    public void VictoryScene(float _t)
    {
        StartCoroutine(DelayedVictoryCall(_t));
    }
    public IEnumerator DelayedVictoryCall(float _t)
    {
        yield return new WaitForSecondsRealtime(_t);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("VictoryScene");
    }

    public void DefeatScene(float _t)
    {
        StartCoroutine(DelayedDefeatCall(_t));
    }
    public IEnumerator DelayedDefeatCall(float _t)
    {
        yield return new WaitForSecondsRealtime(_t);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("DefeatScene");
    }

    //Ao ser chamado fecha a aplicação
    public void BtQuit () {
        
        Application.Quit();
    }
}
