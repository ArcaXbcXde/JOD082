using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JogadorControl : MonoBehaviour
{

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
    public float hpCurrent = 10;

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


    private static JogadorControl instance;
    public static JogadorControl Instance { get { return instance; } }

    // Start
    private void Awake()
    {
        instance = this;
        rigid = GetComponent<Rigidbody>();
        anima = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked; // trava o cursor na tela
        Cursor.visible = false; // deixa o cursor invisivel
    }

    private void FixedUpdate()
    {
        // Movimento
        if (assassinando == false)
        {
            Movimenta();
            Pular();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AudioManager.Instance.PlaySFX(0);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            AudioManager.Instance.StopSFX(0);
        }
        // Caso vá assassinar
        Assassinar();

        // Variáveis de animação
        Animacao();
    }

    private void Movimenta()
    {
        // atualiza variáveis de movimento dos eixos X e Y
        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        float velAtual = 0;

        if (movX != 0 || movZ != 0)
        {
            // crouch run
            if ((Input.GetKey(KeyCode.LeftControl)) && (Input.GetKey(KeyCode.LeftShift)))
            {

                velAtual = vel * Time.deltaTime * multiplicadorAndar * 1.2f;
                estadoMovimento = 2;

                // crouch
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {

                velAtual = vel * Time.deltaTime * multiplicadorAndar * 0.3f;
                estadoMovimento = 2;

                // run
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {

                velAtual = vel * Time.deltaTime;
                estadoMovimento = 1;

                // walk
            }
            else if ((!Input.GetKey(KeyCode.LeftControl)) && (!Input.GetKey(KeyCode.LeftShift)))
            {

                velAtual = vel * Time.deltaTime * multiplicadorAndar;
                estadoMovimento = 0;
            }
            // se está parado ele pode estar ou na pose de agachado ou idle
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {

            velAtual = vel * Time.deltaTime * multiplicadorAndar * 0.5f;
            estadoMovimento = 2;

            // idle
        }
        else
        {

            velAtual = vel * Time.deltaTime * multiplicadorAndar;
            estadoMovimento = 0;
        }

        transform.Translate(movX * velAtual, 0, movZ * velAtual);
    }

    private void Pular()
    {

        if (noChao == true)
        {

            if (Input.GetButtonDown("Jump"))
            {

                rigid.AddForce(Vector3.up * forcaPulo);
                noChao = false;
            }
        }
    }

    private void Animacao()
    {
        if (estadoMovimento == 0)
        {
            anima.SetFloat("moveAxisZ", movZ/2);
        }
        else
        {
            anima.SetFloat("moveAxisZ", movZ);
        }
        anima.SetFloat("moveAxisX", movX);
        anima.SetBool("onGround", noChao);
        anima.SetInteger("moveState", estadoMovimento);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            noChao = true;
        }
    }

    private void Assassinar()
    {
        if (assassinavel == true)
        {
            dicaParaAssassinar.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                dicaParaAssassinar.SetActive(false);
                if (alvoAtual.GetComponent<EntitieGuard>())
                {
                    alvoAtual.GetComponent<EntitieGuard>().Death();
                }
                anima.SetTrigger("assassination");
                Invoke("TerminouAssassinato", 1.2f);
                assassinando = true;
                assassinavel = false;
            }
        }
        else
        {
            dicaParaAssassinar.SetActive(false);
        }
    }

    private void TerminouAssassinato()
    {
        assassinando = false;
    }

    public void TakeDamage(float _dmg)
    {
        hpCurrent -= _dmg;
        PlayerHpBarUI.Instance.AttHealth();
        if (hpCurrent <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        if (hpCurrent <= 0)
        {
            Invoke("Vanish", 2);
        }
    }

    private void Vanish()
    {
        gameObject.SetActive(false);
        SceneManagement.Instance.DefeatScene(0);
    }
}