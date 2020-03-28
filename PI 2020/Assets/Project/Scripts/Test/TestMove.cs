using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float moveSpeed = 12;

    Rigidbody rigidBody;
    float moveX;
    float moveZ;

    bool isTouching = false;
    public float jumpPower;

    public Transform _camera;

    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        if (rigidBody == null)
            Debug.LogError("RigidBody could not be found.");
    }


    void FixedUpdate()
    {

        Move();
        
    }

    void Move()
    {
       float moveX = Input.GetAxis("Horizontal");
       float moveZ = Input.GetAxis("Vertical");


        Vector3 moveVector = new Vector3(transform.position.x * moveX, 0f, (transform.position.z - _camera.transform.position.z) * moveZ);

        if (rigidBody != null)
        {
            rigidBody.velocity = new Vector3(moveX, 0f, moveZ) * moveSpeed;
        }
        if (isTouching && Input.GetKeyDown("space"))
        { //jump
            rigidBody.AddForce(new Vector3(0, jumpPower, 0) * 50, ForceMode.Acceleration);
        }
    }   

    private void OnCollisionEnter(Collision collision)
    {
        isTouching = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isTouching = false;
    }
}


