using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JogadorControl : MonoBehaviour {

	public bool assassinavel = false;

	/* 0 = andando
	 * 1 = correndo
	 * 2 = agachado
	 */
	[HideInInspector]
	public int estadoMovimento = 0;

	public float vel = 15.0f;
	public float multiplicadorAndar = 0.3f;
	public float forcaPulo = 300.0f;
	
	public float hpMax = 10;
	public float hp = 10;

	public Transform cameraJogador;

	public Transform gatilhoAssassinato;

	public GameObject dicaParaAssassinar;
	
	public GameObject alvoAtual = null;

	private bool noChao;
	private bool assassinando = false;

	private float movX;
	private float movZ;

	private Rigidbody rigid;

	private Animator anima;

	// Start
	private void Awake () {

		rigid = GetComponent<Rigidbody>();
		anima = GetComponent<Animator>();

		Cursor.lockState = CursorLockMode.Locked; // trava o cursor na tela
		Cursor.visible = false; // deixa o cursor invisivel
	}

	// Update fixo
	private void FixedUpdate () {

		// Movimento
		if (assassinando == false) {

			Movimenta();
		}

		// Pulo
		Pular();
	}
	
	// Update
	private void Update () {

		// Caso o jogador morra
		//Morrer();

		// Caso vá assassinar
		Assassinar();

		// Variáveis de animação
		Animacao();
	}

	// Método para controle de movimento do jogador
	private void Movimenta () {

		// atualiza variáveis de movimento dos eixos X e Y
		movX = Input.GetAxis("Horizontal");
		movZ = Input.GetAxis("Vertical");

		float velAtual = 0;

		if (movX != 0 || movZ != 0) {
			// crouch run
			if ((Input.GetKey(KeyCode.LeftControl)) && (Input.GetKey(KeyCode.LeftShift))) {

				velAtual = vel * Time.deltaTime * multiplicadorAndar * 1.2f;
				estadoMovimento = 2;

				// crouch
			} else if (Input.GetKey(KeyCode.LeftControl)) {

				velAtual = vel * Time.deltaTime * multiplicadorAndar * 0.3f;
				estadoMovimento = 2;

				// run
			} else if (Input.GetKey(KeyCode.LeftShift)) {

				velAtual = vel * Time.deltaTime;
				estadoMovimento = 1;

				// walk
			} else if ((!Input.GetKey(KeyCode.LeftControl)) && (!Input.GetKey(KeyCode.LeftShift))) {

				velAtual = vel * Time.deltaTime * multiplicadorAndar;
				estadoMovimento = 0;
			}

			// se está parado ele pode estar ou na pose de agachado ou idle
		} else if (Input.GetKey(KeyCode.LeftControl)) {

			velAtual = vel * Time.deltaTime * multiplicadorAndar * 0.3f;
			estadoMovimento = 2;

			// idle
		} else {

			velAtual = vel * Time.deltaTime * multiplicadorAndar;
			estadoMovimento = 0;
		}

		transform.Translate(movX * velAtual, 0, movZ * velAtual);
	}

	// Método para controle do pulo
	private void Pular () {

		if (noChao == true) {

			if (Input.GetButtonDown("Jump")) {

				rigid.AddForce(Vector3.up * forcaPulo);
				noChao = false;
			}
		}
	}

	// Controle das variáveis de animação
	private void Animacao () {

		anima.SetFloat("moveAxisX", movX);
		anima.SetFloat("moveAxisZ", movZ);
		anima.SetBool("onGround", noChao);
		anima.SetInteger("moveState", estadoMovimento);
	}

	// Ao entrar no colisor do jogador
	private void OnCollisionEnter (Collision col) {
		
		if (col.gameObject.tag == "Ground") {

			noChao = true;
		}
	}

	// Para quando o jogador for assassinar
	private void Assassinar () {

		if (assassinavel == true) {

			dicaParaAssassinar.SetActive(true);
			if (Input.GetKeyDown(KeyCode.E)) {

				dicaParaAssassinar.SetActive(false);
				alvoAtual.SetActive(false);
				anima.SetTrigger("assassination");
				Invoke("TerminouAssassinato", 1.2f);
				assassinando = true;
				assassinavel = false;
			}
		} else {

			dicaParaAssassinar.SetActive(false);
		}
	}

	// Quando o jogador termina o assassinato ele pode se mover novamente
	private void TerminouAssassinato () {

		assassinando = false;
	}

	private void Morrer () {

		if (hp <= 0) {

			Invoke("Sumir", 2);
		}
	}

	private void Sumir () {

		gameObject.SetActive(false);
		SceneManager.LoadScene("DefeatScene");
	}
}