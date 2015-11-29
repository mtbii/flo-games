using UnityEngine;
using System.Collections;

public class basicMovement : MonoBehaviour
{

    //Variables
    //Need to clear out unused variables when script is completed
    private Rigidbody rBody;
    private Animator animator;
    private CapsuleCollider cColl;
    private bool grounded;
    private float distToGround = 0.0f;
    public float jumpSpeed;
    public int jumpHeight;
    //private Vector3 velocity;

/*******Variables for Input.GetAxis********
    //private float forwardInput;
    //private Vector3 movement;
    //private float moveSpeed = 5f;
******************************************/

    // Use this for initialization
    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            rBody = GetComponent<Rigidbody>();
        }

        else
        {
            Debug.LogError("The character needs a rigidBody.");
        }

        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }

        else
        {
            Debug.LogError("The character does not have an Animator.");
        }

        if (GetComponent<CapsuleCollider>())
        {
            cColl = GetComponent<CapsuleCollider>();
        }

        else
        {
            Debug.LogError("The character needs a Capsule Collider.");
        }

        //forwardInput = 0;
    }

    void Update()
    {
        //Makes sure player does not have movement along z-axis
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

/*************distToGround for jumping mechanics*************
        //distToGround = cColl.bounds.extents.y;
************************************************************/

        //******Change to four main 'if' statements: 1 for each gravity direction*********

        //Movement mechanics for gravity-up and gravity-down (they are the same)
        if (Input.GetKey(KeyCode.A))
        {
            if (rBody.velocity.x > -5)
                rBody.AddForce(new Vector3(-60, 0, 0) * 5);
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (rBody.velocity.x < 5)
                rBody.AddForce(new Vector3(60, 0, 0) * 5);
        }

        //Movement mechanics for gravity-right
        if (Physics.gravity == (Vector3.right * 25f))
        {
            if (Input.GetKey(KeyCode.A))
                rBody.AddForce(new Vector3(0, -60, 0));

            if (Input.GetKey(KeyCode.D))
                rBody.AddForce(new Vector3(0, 60, 0));
        }

        //Movement mechanics for gravity-left
        if (Physics.gravity == (-Vector3.right * 25f))
        {
            if (Input.GetKey(KeyCode.A))
                rBody.AddForce(new Vector3(0, 60, 0));

            if (Input.GetKey(KeyCode.D))
                rBody.AddForce(new Vector3(0, -60, 0));
        }

/***********************Testing with Input.GetAxis**********************
        //forwardInput = Input.GetAxis("SketchHoriz");
        //if(Input.GetKey(KeyCode.A))
        //{
        //    if (rBody.velocity.x > -5)
        //        rBody.AddForce(new Vector3(forwardInput, 0, 0));
        //}
***********************************************************************/

/*********************************************************Jumping mechanics***********************************************************
        //    if (Physics.gravity == (Vector3.up * 25f))
        //    {
        //        if ((Input.GetKeyDown(KeyCode.Space)) && Physics.Raycast(transform.position, Vector3.up, distToGround + 0.1f))
        //            rBody.AddForce(-Vector3.up * 1200);
        //    }

        //    if (Physics.gravity == (-Vector3.up * 25f))
        //    {
        //        if ((Input.GetKeyDown(KeyCode.Space)) && Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        //        rBody.AddForce(Vector3.up * 1200);
        //    }

        //    if (Physics.gravity == (Vector3.right * 25f))
        //    {
        //        if ((Input.GetKeyDown(KeyCode.Space)) && Physics.Raycast(transform.position, Vector3.right, distToGround + 0.1f))
        //            rBody.AddForce(-Vector3.right * 1200);
        //    }

        //    if (Physics.gravity == (-Vector3.right * 25f))
        //    {
        //        if ((Input.GetKeyDown(KeyCode.Space)) && Physics.Raycast(transform.position, -Vector3.right, distToGround + 0.1f))
        //            rBody.AddForce(Vector3.right * 1200);
        //    }
****************************************************************************************************************************************/
    }

    void FixedUpdate()
    {
        if (animator)
        {
            CheckIfGrounded();
            animator.SetFloat("Speed", rBody.velocity.x * 6);
            //if(rBody.velocity.x != 0)
            //    animator.SetFloat("Speed", Mathf.Abs(forwardInput) * 3);
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("AirVelocity", rBody.velocity.y);
        }

        UpdateCharacterPosition();
    }

    private void UpdateCharacterPosition()
    {

    }

    private void CheckIfGrounded()
    {
        //if (Physics.gravity == (Vector3.up * 25f))
        //{
        //    if (Physics.Raycast(transform.position, Vector3.up, distToGround + 0.1f))
        //        grounded = true;
        //}

        //if (Physics.gravity == (-Vector3.up * 25f))
        //{
        //    if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        //        grounded = true;
        //}

        //if (Physics.gravity == (Vector3.right * 25f))
        //{
        //    if (Physics.Raycast(transform.position, Vector3.right, distToGround + 0.1f))
        //        grounded = true;
        //}

        //if (Physics.gravity == (-Vector3.right * 25f))
        //{
        //    if (Physics.Raycast(transform.position, -Vector3.right, distToGround + 0.1f))
        //        grounded = true;
        //}
    }

    void OnCollisionEnter()
    {
        grounded = true;
    }

    void OnCollisionExit()
    {
        grounded = false;
    }

    //void OnCollisionEnter(Collision coll)
    //{
    //    if (coll.collider.tag == "ground")
    //        grounded = true;
    //}

    //void OnCollisionExit(Collision coll)
    //{
    //    if (coll.collider.tag == "ground")
    //        grounded = false;
    //}
}
