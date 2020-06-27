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

    // "Start"
    protected override void Awake()
    {
        base.Awake();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_animator = gameObject.GetComponent<Animator>();
    }

    // Update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.D))
        {
            Move();
        }
        
        Jump(KeyCode.Space);
        m_animator.SetBool("IsGround", isGround);

    }

    #region Movement
    // Method for basic movement
    private void Move() {
        // Movement variables
        float moveAxisX = Input.GetAxis("Horizontal");
        float moveAxisZ = Input.GetAxis("Vertical");

        // Updating the movement variables
        m_animator.SetFloat("moveAxisX", moveAxisX);
        m_animator.SetFloat("moveAxisZ", moveAxisZ);

        // Sets the rotation of the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MoveVector(moveAxisZ,moveAxisX)), m_rotationSpeed);
        // Sets the speed of the player based on the X and Z movement variables
        m_rigidbody.velocity = (MoveVector(moveAxisZ, moveAxisX) * m_speed);

    }

    // Method to jump
    private void Jump(KeyCode _jumpButton) {

        // If the jump button is pressed, the rigidbody applies a force upwards
        if (Input.GetKeyDown(_jumpButton)) {
            m_rigidbody.velocity = Vector3.up * m_jumpForce;
        }
    }

    #endregion

    #region Vectors

    //get the forward position of the camera
    private Vector3 CameraFowardVector() {


        Vector3 camForward = m_camera.forward;
        camForward.y = 0f;
        camForward.Normalize();
        //Vector3 camFowardVector = new Vector3(camForward.x, 0, camForward.z);
        return camForward;
    }

    // Get the side position of the camera 
    private Vector3 CameraRightVector() {

        Vector3 camRight = m_camera.right;
        camRight.y = 0f;
        camRight.Normalize();
        //Vector3 camRightvector = new Vector3(camRight.x, 0, camRight.z);

        return camRight;

    }

    // 
    private Vector3 CameraPositionVector() {

        Vector3 camPosition = transform.localPosition - m_camera.position;
        camPosition.y = 0f;
        return camPosition;
    }

    Vector3 MoveVector(float _axisZ, float _axisX) {

        Vector3 moveVector = (CameraFowardVector() * _axisZ) + (CameraRightVector() * _axisX) + VerticalVector();

        return moveVector;
    }

    Vector3 VerticalVector() {

        return new Vector3(0,m_Verticalvelocity , 0); 
    }

    #endregion

    #region Ground Check

    // Checks whatever entered the player's trigger
    private void OnTriggerEnter(Collider col) {

        // Checks if what entered in the trigger was the ground, so marks the player as on ground
        if (col.gameObject.tag.Equals("Ground")) {

            Debug.Log("Hit Ground");
            isGround = true;
        }
    }

    // Checks whatever exited the player's trigger
    private void OnTriggerExit(Collider col) {

        // Checks if what exited the trigger was the ground, so marks the player as out of ground
        if (col.gameObject.tag.Equals("Ground")) {

            Debug.Log("Nope");
            isGround = false;
        }
    }
    #endregion

    #region Gizmos
    // Gizmos to mark stuffs in editor
    private void OnDrawGizmos() {

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, CameraFowardVector());

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, CameraRightVector());
    }
    #endregion
}
