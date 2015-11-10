using UnityEngine;
using System.Collections;
using System;

public class newSideScrollBehavior : MonoBehaviour
{

    public float inputDeadzone = 0.1f;
    public float forwardVel = 1;

    private float forwardInput;
    private Rigidbody rBody;
    private Animator animator;
    private bool grounded;
    private float distToGround = 0;
    private bool facingForward = true;

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
        forwardInput = 0;
    }

    void GetInput()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
            Physics.gravity = -Vector3.up * 9.81f;

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            Physics.gravity = -Vector3.right * 9.81f;

        if (Input.GetKeyUp(KeyCode.DownArrow))
            Physics.gravity = Vector3.up * 9.81f;

        if (Input.GetKeyUp(KeyCode.RightArrow))
            Physics.gravity = Vector3.right * 9.81f;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();     //maybe throw this into FixedUpdate()?
    }

    void FixedUpdate()
    {
        if (animator)
        {
            CheckIfGrounded();
            animator.SetFloat("Speed", forwardInput * 3);
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("AirVelocity", rBody.velocity.y);
        }
        Run();
    }

    private void CheckIfGrounded()
    {
        grounded = Physics.Raycast(rBody.position - Physics.gravity.normalized * .05f, Physics.gravity.normalized, distToGround + 0.1f);
    }

    void Run()
    {
        if (Mathf.Abs(forwardInput) > inputDeadzone)
        {
            rBody.position += transform.forward * forwardInput * forwardVel * Time.deltaTime; //new Vector3(forwardInput * forwardVel, rBody.velocity.y, rBody.velocity.z);
        }
        else
        {
            //rBody.velocity = Vector3.zero; //new Vector3(0, rBody.velocity.y, rBody.velocity.z);
        }
    }
}
