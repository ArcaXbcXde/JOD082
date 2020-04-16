using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorControl : MonoBehaviour {

    public float vel = 10.0f, alturaPulo = 200.0f;

    private float movX, movZ, cdPegada = 0.5f;

    private bool noChao, carregando = false;

    public Vector3 posicaoRelativa = new Vector3 (0.5f, -0.25f, 0.6f);

    private Rigidbody rigid;

    //private Animator anima;

    public Transform cameraJogador;

    private Transform objetoSegurado;

    private AudioSource passos;
    
    //Start
	void Awake () {

        rigid = GetComponent<Rigidbody>();
        //anima = GetComponent<Animator>();
        passos = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked; // trava o cursor na tela
        Cursor.visible = false; // deixa o cursor invisivel
	}
	
    //Update
	void Update () {

        // Movimento
        Movimenta();

        // Pulo
        Pular();
        
        // Carregar objeto
        //Carregar();

        // Contagem de recargas
        Contagem();
        
        // Variáveis de animação
        //Animacao();

        // Controles de som
        //Sons();
    }

    // Método para controle de movimento do jogador
    private void Movimenta() {
        
        // atualiza variáveis de movimento dos eixos X e Y
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        // efetua movimento com as variáveis de movimento
        transform.Translate(movX * vel * Time.deltaTime, 0, movZ * vel * Time.deltaTime);
    }

    // Método para controle do pulo
    private void Pular() {
        if (noChao == true) {
            if (Input.GetButtonDown("Jump")) {
                rigid.AddForce(Vector3.up * alturaPulo);
                noChao = false;
            }
        }
    }
    
    // Controle das variáveis de animação
    /*private void Animacao() {

        anima.SetFloat ("movX", movX);
        anima.SetFloat ("movZ", movZ);
        anima.SetBool("noChao", noChao);
    }*/

    // Controles para carregar coisas
    private void Carregar() {

        // Confirma se o objeto não foi destruído ou se o jogador realmente está carregando algo
        if (carregando == true && objetoSegurado == null) {
            carregando = false;
        }

        if (carregando == true && Input.GetKeyDown(KeyCode.E) && cdPegada <= 0.0f) {

            Debug.Log("largou");

            carregando = false; // não está mais segurando um objeto
            
            objetoSegurado.SetParent(null); // volta o objeto para o mundo

            // !! As vezes o objeto atravessa as coisas quando cai 
            objetoSegurado.GetComponent<Rigidbody>().isKinematic = false; // volta a física

            objetoSegurado.GetComponent<BoxCollider>().enabled = true; // volta a colisão

            objetoSegurado.transform.rotation = new Quaternion(0, 0, 0, 0); // rotaciona o objeto para a posição padrão

            objetoSegurado = null; // não tem um objeto na mão

            cdPegada = 0.2f;
        }
    }

    // Controles para variáveis de contagem regressiva
    private void Contagem() {

        if (cdPegada > 0) {

            cdPegada -= Time.deltaTime;
        }
    }

    // Controle de sons emitidos pelo ator
    private void Sons() {

        if ((movX > 0.2 || movX < -0.2 || movZ > 0.2 || movZ < -0.2) && noChao == true) {
            passos.enabled = true;
        }else {
            passos.enabled = false;
        }
    }

    // Ao entrar no colisor do jogador
    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Ground") {

            noChao = true;
        }
    }

    // Se tiver um objeto na área do gatilho do jogador
    private void OnTriggerStay(Collider col) {

        if (carregando == false && Input.GetKeyDown(KeyCode.E) && cdPegada <= 0.0f && col.tag == "Carregavel") {

            Debug.Log("pegou");

            carregando = true; // está segurando um objeto
            
            col.GetComponent<Rigidbody>().isKinematic = true; // desativa física

            col.GetComponent<BoxCollider>().enabled = false; // desativa colisão

            col.transform.SetParent(cameraJogador.transform); // transforma o objeto em filho da câmera para seguir todos os movimentos
            
            col.transform.localPosition = posicaoRelativa; // coloca o objeto em uma posição específica relativa à câmera
            
            objetoSegurado = cameraJogador.GetChild(0); // o objeto que está na mão

            cdPegada = 0.2f;
        }
    }

    // Controla o esqueleto do personagem por cima do animator utilizando IK pass
    /*private void OnAnimatorIK(int layerIndex) {

        if (carregando == true) {

            // Mão do jogador segura o objeto
            anima.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anima.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            anima.SetIKPosition(AvatarIKGoal.RightHand, cameraJogador.GetChild(0).transform.position);
            anima.SetIKRotation(AvatarIKGoal.RightHand, cameraJogador.GetChild(0).transform.rotation);
        }else {

            // Mão do jogador solta o objeto
            anima.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            anima.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            anima.SetLookAtWeight(0);
        }


    }*/
}
