using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class MovementController : EntitiesComplex
{
    

    public enum AnimatorEstate { Idle, moving, jumping }
    AnimatorEstate enumAnimatorEstate = AnimatorEstate.Idle;

    Rigidbody m_rigidbody;
    Animator m_animator;
    public Transform m_camera;

    public float m_speed = 10;
    public float m_jumpForce = 40;

    public float m_Gravity = 10;
    public float m_Verticalvelocity;
    
    public float m_rotationSpeed = 50;

    bool isGround = true;

    protected override void Awake()
    {
        base.Awake();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump(KeyCode.Space);
        m_animator.SetBool("IsGround", isGround);

    }

    #region Movement
    void Move()
    {
        float moveAxisX = Input.GetAxis("Horizontal");
        float moveAxisZ = Input.GetAxis("Vertical");

        m_animator.SetFloat("moveAxisX", moveAxisX);
        m_animator.SetFloat("moveAxisZ", moveAxisZ);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MoveVector(moveAxisZ,moveAxisX)), m_rotationSpeed);
        m_rigidbody.velocity = (MoveVector(moveAxisZ, moveAxisX) * m_speed);

    }
    void Jump(KeyCode _jumpButton)
    {
        if (Input.GetKeyDown(_jumpButton))
        {
            m_rigidbody.velocity = Vector3.up * m_jumpForce;
        }
    }

    #endregion

    #region Vectors

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

    Vector3 MoveVector(float _axisZ, float _axisX)
    {
        Vector3 moveVector = (CameraFowardVector() * _axisZ) + (CameraRigthVector() * _axisX) + VerticalVector();

        return moveVector;
    }

    Vector3 VerticalVector()
    {
        return new Vector3(0,m_Verticalvelocity , 0); 
    }

    #endregion

    #region Ground Check

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            Debug.Log("Hit Ground");
            isGround = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            Debug.Log("Nope");
            isGround = false;
        }
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, CameraFowardVector());

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, CameraRigthVector());
    }
    #endregion
}
