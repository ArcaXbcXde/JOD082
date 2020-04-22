using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]



public class PlayerMovement: MonoBehaviour {


    public enum MoveStatus { Run,Walk,Crouch }

    MoveStatus enumMoveState = MoveStatus.Run;

    public bool canMove = true;

    [System.Serializable]
    class PlayerSpeed {

        public float run = 0.125f;
        public float walk = 0.70f;
        public float crouch = 0.50f;
        public float crouchWalk = 0.20f;
    }  
    [System.Serializable]
    class PlayerJump {

        public float upForce = 40;
        public float fallMultiplier = 2.5f;
        public float lowfallMultiplier = 2.5f;
    }

    [SerializeField]
    PlayerJump m_jump;

    [SerializeField]
    PlayerSpeed m_Speed;

    [Space]
    [Space]

    public LayerMask m_groundLayer;

    [Space]


    //public float m_speedRun = 0.125f;
    //public float m_speedCrouch = 0;

    public float m_rotationSpeed = 50;

    public Transform m_camera;
    public Image hpBar;

    private Rigidbody m_rigidbody;
    private Animator m_animator;
    private CapsuleCollider m_capsuleCollider;
    
    #region Privates
    [SerializeField]
    bool isGround;
    bool IsGround() {

        float _radius = 0.15f;
        Vector3 _pos = new Vector3(transform.position.x, (transform.position.y - 1.6f / 2f), transform.position.z);
        if (Physics.CheckSphere(_pos, _radius, m_groundLayer)) {

            return true;
        }else {

            return false;
        }
    }

    bool isJumping = false;

    float InputX;
    float InputZ;

    #endregion

    void Awake() {

        m_Speed.run = m_Speed.run / 1000;
        m_Speed.walk = m_Speed.walk / 1000;
        m_Speed.crouch = m_Speed.crouch / 1000;
        m_Speed.crouchWalk = m_Speed.crouchWalk / 1000;

        m_capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_animator = gameObject.GetComponent<Animator>();
    }

    private void Update() {

        if (canMove) {
            Move();
            jump();
            Rotation();
        }
        
        AnimParameters();

        if (IsGround()) {
            isGround = true;
        }

        if (!IsGround()) {

            isGround = false;
        }
    }

    #region Movement
    private void Move() {
            
        if (!Input.GetKey(KeyCode.LeftControl)) {

            //run
            transform.Translate(MoveVector() * m_Speed.run);
            enumMoveState = MoveStatus.Run;

            if (Input.GetKey(KeyCode.LeftShift)) {

                //walk
                transform.Translate(MoveVector() * m_Speed.walk);
                enumMoveState = MoveStatus.Walk;
            }
        }
        if (Input.GetKey(KeyCode.LeftControl)) {

            //crounch
            transform.Translate(MoveVector() * m_Speed.crouch);
            enumMoveState = MoveStatus.Crouch;

            if (Input.GetKey(KeyCode.LeftShift)) {

                //slowerCounch
                transform.Translate(MoveVector() * m_Speed.crouchWalk);
                enumMoveState = MoveStatus.Crouch;
            }
        }

        switch (enumMoveState) {

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

    private void jump() {

        if (IsGround()) {
            if (Input.GetKeyDown(KeyCode.Space)) {

                m_rigidbody.AddForce(Vector3.up * m_jump.upForce, ForceMode.Impulse);
                Debug.Log("Ground");
                m_animator.SetTrigger("isJumping");
            }
        }
        if (!IsGround()) {

            if (m_rigidbody.velocity.y < 0) {

                m_rigidbody.velocity += Vector3.up * Physics.gravity.y * (m_jump.fallMultiplier - 1) * Time.deltaTime;
            }
            if (m_rigidbody.velocity.y > 0) {

                m_rigidbody.velocity += Vector3.up * Physics.gravity.y * (m_jump.fallMultiplier / 2 - 1) * Time.deltaTime;
            }
        }
    }

    private void Rotation() {
        
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MoveVector()), m_rotationSpeed);
    }

    #endregion

    #region Animator

    void AnimParameters() {
        

        
    }

    #endregion

    #region Vectors

    #region Camera


    // Get the foward position of the camera
    private Vector3 CameraFowardVector() {
        Vector3 camForward = m_camera.forward;
        camForward.y = 0f;
        camForward.Normalize();
        //Vector3 camFowardVector = new Vector3(camForward.x, 0, camForward.z);
        return camForward;
    }

    // Get the side position of the camera
    private Vector3 CameraRigthVector() {
        Vector3 camRight = m_camera.right;
        camRight.y = 0f;
        camRight.Normalize();
        // Vector3 camRightvector = new Vector3(camRight.x, 0, camRight.z);

        return camRight;

    }

    // Get the total position of the camera
    private Vector3 CameraPositionVector() {
        Vector3 camPosition = transform.localPosition - m_camera.position;
        camPosition.y = 0f;
        return camPosition;
    }
    #endregion

    Vector3 MoveVector() {

        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Vector3 moveVector = (CameraFowardVector() * InputZ) + (CameraRigthVector() * InputX);

        return moveVector;
    }


    #endregion

    #region colliders
    private void OnTriggerEnter(Collider col) {

    }
    private void OnTriggerExit(Collider col) {
        
    }

    #endregion
    
    #region Gizmos
    private void OnDrawGizmos() {

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, CameraFowardVector());

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, CameraRigthVector());

        Ray _dir = new Ray(transform.position, transform.up * -1);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_dir);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(transform.position,transform.position + Vector3.down);

        float _radius = 0.15f;
        Vector3 _pos = new Vector3(transform.position.x, (transform.position.y - 1.6f/2f), transform.position.z);
        //Vector3 _pos = transform.position;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_pos, _radius);
    }
    #endregion
}
