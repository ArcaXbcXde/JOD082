using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    #region Variáveis Públicas
    public bool isGrounded;
    public bool isMoving;
    public bool isJumping;
    public bool isSprinting;
    public bool roll;
    public float desiredRotationSpeed;
    public float allowPlayerRotation;
    public float jumpforce;
    public float gravity;
    public float maxSpeed;
    #endregion

    #region Variáveis Privadas
    Animator anim;
    Camera cam;
    CharacterController controller;
    Vector2 speedVec;
    Vector3 moveVector;
    Vector3 desiredMoveDirection;
    Vector3 colExtents;
    float InputX;
    float InputZ;
    float rollClick;
    float speed;
    public float moveSpeed;
    float verticalVel; 
    #endregion

    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
        colExtents = GetComponent<Collider>().bounds.extents;
    }

    void Update()
    {
        InputMagnitude();
        Grounded();
        AnimatorBools();
        Jump();
        Roll();
        OnAnimatorMove();
        moveVector = new Vector3(desiredMoveDirection.x * moveSpeed, verticalVel, desiredMoveDirection.z * moveSpeed);
        controller.Move(moveVector * Time.deltaTime);
        #region Anular velocidade durante rolamento
        if (roll == true)
        {
            moveSpeed -= moveSpeed - 1;
        }
        #endregion

    }

    void InputMagnitude() // Método de movimento do Player
    {
        #region Calcular os vetores dos Inputs 
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        anim.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2f);
        anim.SetFloat("InputX", InputX, 0.0f, Time.deltaTime * 2f);
        #endregion

        #region Calcular o Speed e moveSpeed

        speedVec = new Vector2(InputX, InputZ);
        speed = speedVec.sqrMagnitude;
        moveSpeed = speed * maxSpeed;
        #endregion

        #region Limite maximo da float MoveSpeed e prevençãod e erros do rolamento // Limite máximo da float speed
        if (moveSpeed > maxSpeed)
            moveSpeed = maxSpeed;
        if (speed > 1)
            speed = 1;
        #endregion

        #region Checar a bool IsMoving quando houver algum input do inputX ou inputZ 
        if (InputX > 0.1f || InputZ > 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        #endregion

        #region Mover fisicamente o jogador
        if (speed > allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (speed < allowPlayerRotation)
        {
            anim.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
        }
        #endregion
    }

    void PlayerMoveAndRotation() // Método para transformar a direção e rotação do Player a partir da camera //chamado no InputMagnitude
    {
        #region Setar valores iniciais para adaptar rotação do player
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        #endregion

        #region Descobrir a direção desejada e rotacionar o player de maneira suave
        desiredMoveDirection = (forward * InputZ) + (right * InputX);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        #endregion
    }

    void AnimatorBools() // Método que define o valor dos parâmetros do animator
    {
        GroundedBool();
        anim.SetBool("IsJumping", isJumping);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsSprinting", isSprinting);
        anim.SetBool("Roll", roll);
    }

    bool Grounded() // Método para checar se o player está no chão ou não
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 2 * colExtents.x, Vector3.down);
        return Physics.SphereCast(ray, colExtents.x, colExtents.x + 0.1f);
    }

    void GroundedBool() // Método para validar variável isGrounded referente ao método Grounded()
    {
        if (Grounded())
        {
            isGrounded = true;
            isJumping = false;
            verticalVel = 0; 
        }
        if (!Grounded())
        {
            isGrounded = false;
            verticalVel -= gravity * Time.deltaTime;
        }
    }

    void Jump() // Método referente ao pulo do Player
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && !isJumping && !roll)
        {
            verticalVel = jumpforce;
            isJumping = true;
        }
            
    }

    void Roll() // Método referente ao Rolamento do Player
    {
        if (Input.GetKeyDown(KeyCode.C) && isGrounded == true)
        {
            rollClick++;
            if (rollClick == 1f)
            {
                roll = true;
                StartCoroutine(EndRoll());
            }
            if (rollClick > 1)
            {
                rollClick = 1;
            }
        }
    }

    IEnumerator EndRoll() // Método para finalizar rolamento em determinado tempo
    {
        yield return new WaitForSeconds(1.3f);
        roll = false;
        rollClick = 0f;
    }

    void OnAnimatorMove() // Método para permitir Root Motion ao rolamento
    {
        string rollName = ("Roll");
        AnimatorStateInfo stateInfo;
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsTag(rollName))
        {
            anim.ApplyBuiltinRootMotion();
            if (InputX == 0 && InputZ == 0)
            {
                anim.ApplyBuiltinRootMotion();
            }
        }
    }
}