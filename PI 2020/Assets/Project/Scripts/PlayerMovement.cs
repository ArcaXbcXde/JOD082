using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement: MonoBehaviour
{
    public enum MoveStatus { Run,Walk,Crouch }

    MoveStatus enumMoveState = MoveStatus.Run;

    Rigidbody m_rigidbody;
    Animator m_animator;
    public Transform m_camera;

    public float m_speedRun = 0.125f;
    public float m_speedCrouch = 0;
    [Range(1,10)]
    public float m_speedMultiplier = 0;
    public float m_jumpForce = 40;

    public float m_rotationSpeed = 50;


    #region Privates
    [SerializeField]
    bool isGround;
    bool IsGround()
    {
        RaycastHit _hit;
        Ray _dir = new Ray(transform.position, transform.up * -1);
        if (Physics.Raycast(_dir, out _hit, 0.7f))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    bool isJumping = false;

    float InputX;
    float InputZ;

    #endregion

    void Awake()
    {
        m_speedCrouch = m_speedRun * 0.6f;

        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        jump();
        Rotation();
        AnimParameters();

        if (IsGround())
        {
            isGround = true;
        }
        if (!IsGround())
        {
            isGround = false;
        }
    }

    #region Movement
    void Move()
    {
            
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(MoveVector() * m_speedRun);
            enumMoveState = MoveStatus.Run;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(MoveVector() * (m_speedRun / m_speedMultiplier));
                enumMoveState = MoveStatus.Walk;
            }
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(MoveVector() * m_speedCrouch);
            enumMoveState = MoveStatus.Crouch;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(MoveVector() * (m_speedCrouch / m_speedMultiplier));
                enumMoveState = MoveStatus.Crouch;
            }
        }

        switch (enumMoveState)
        {
            case MoveStatus.Run:
                m_animator.SetInteger("MoveState", 0);
                break;
            case MoveStatus.Walk:
                m_animator.SetInteger("MoveState", 1);
                break;
            case MoveStatus.Crouch:
                m_animator.SetInteger("MoveState", 2);
                break;
            default:
                break;
        }

        m_animator.SetFloat("moveAxisX", InputX);
        m_animator.SetFloat("moveAxisZ", InputZ);
    }

    void jump()
    {
        if (IsGround())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
                Debug.Log("Ground");
                m_animator.SetTrigger("isJumping");
            }
        }
        if (!IsGround())
        {

        }
        

       
        
    }

    void Rotation()
    {
        
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MoveVector()), m_rotationSpeed);
    }

    #endregion

    #region Animator

    void AnimParameters()
    {
        

        
    }

    #endregion

    #region Vectors

    #region Camera


    //get the foward position of the camera
    Vector3 CameraFowardVector()
    {
        Vector3 camForward = m_camera.forward;
        camForward.y = 0f;
        camForward.Normalize();
        //Vector3 camFowardVector = new Vector3(camForward.x, 0, camForward.z);
        return camForward;
    }

    Vector3 CameraRigthVector()
    {
        Vector3 camRight = m_camera.right;
        camRight.y = 0f;
        camRight.Normalize();
        //Vector3 camRightvector = new Vector3(camRight.x, 0, camRight.z);

        return camRight;

    }

    Vector3 CameraPositionVector()
    {
        Vector3 camPosition = transform.localPosition - m_camera.position;
        camPosition.y = 0f;
        return camPosition;
    }
    #endregion

    Vector3 MoveVector()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Vector3 moveVector = (CameraFowardVector() * InputZ) + (CameraRigthVector() * InputX);

        return moveVector;
    }


    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, CameraFowardVector());

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, CameraRigthVector());


        Ray _dir = new Ray(transform.position, transform.up * -1);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_dir);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,transform.position + Vector3.down);
    }
    #endregion
}
