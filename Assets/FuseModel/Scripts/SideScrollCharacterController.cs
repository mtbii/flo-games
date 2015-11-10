﻿using UnityEngine;
using System.Collections;
using System;

public class SideScrollCharacterController : MonoBehaviour
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
        forwardInput = Input.GetAxis("Horizontal");

        if(facingForward && forwardInput < 0)
        {
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
            facingForward = false;
        }

        else if (!facingForward && forwardInput > 0)
        {
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
            facingForward = true;
        }

        forwardInput = Mathf.Abs(forwardInput);

        if(Input.GetButtonUp("Jump"))
        {
            rBody.position -= Physics.gravity.normalized;
            Quaternion delta = facingForward ? Quaternion.Euler(-90, 0, 0) : Quaternion.Euler(90, 0, 0);
            Quaternion worldDelta = Quaternion.Euler(0, 0, 90);
            Physics.gravity = worldDelta * Physics.gravity;
            rBody.rotation *= delta;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        if (animator)
        {
            CheckIfGrounded();
            animator.SetFloat("Speed", forwardInput*3);
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("AirVelocity", rBody.velocity.y);
        }
        Run();
    }

    private void CheckIfGrounded()
    {
        grounded = Physics.Raycast(rBody.position - Physics.gravity.normalized*.05f, Physics.gravity.normalized, distToGround + 0.1f);
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
