﻿using UnityEngine;
using System.Collections;
using System;
using Assets.FuseModel.Scripts;

public class SideScrollCharacterController : MonoBehaviour
{
    public float inputDeadzone = 0.1f;
    public float forwardVel = 1;
    public float gravityTurnTime = 1;

    private float forwardInput;
    private Rigidbody rBody;
    private Animator animator;
    private bool grounded;

    private float startRotationTime = 0;
    //private float rotationParam = 0;

    private Vector3 lastVelocity;
    private RotationDirection lastTurnDirection;
    private Direction gravityDir;
    private float distToGround = 0.0f;
    private bool gravityChanged = false;
    private bool facingForward = true;

    public enum RotationDirection
    {
        Clockwise,
        CounterClockwise
    }

    public enum Direction
    {
        Down,
        Right,
        Up,
        Left
    }

    public RotationDirection LastTurnDirection
    {
        get
        {
            return lastTurnDirection;
        }
        private set
        {
            lastTurnDirection = value;
        }
    }
    public Direction GravityDirection
    {
        get { return gravityDir; }
        set
        {
            Direction oldDir = gravityDir;
            gravityDir = value;

            if (oldDir != gravityDir)
            {
                gravityChanged = true;
            }
        }
    }

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
        facingForward = Mathf.Abs(transform.rotation.eulerAngles.y) > 180 ? false : true;
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Horizontal");

        if (grounded)
        {
            if (facingForward && forwardInput < 0)
            {
                transform.rotation *= Quaternion.AngleAxis(-180, Vector3.up);
                facingForward = false;
            }
            else if (!facingForward && forwardInput > 0)
            {
                transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
                facingForward = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            //Changing GravityDirection sets gravityChanged to true
            GravityDirection = GravityDirection.TurnClockwise();
            lastTurnDirection = RotationDirection.Clockwise;
            startRotationTime = Time.realtimeSinceStartup;

            if (grounded)
                transform.position += transform.up.normalized;

            lastVelocity = rBody.velocity;
            rBody.velocity = Vector3.zero;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            //Changing GravityDirection sets gravityChanged to true
            GravityDirection = GravityDirection.TurnCounterClockwise();
            lastTurnDirection = RotationDirection.CounterClockwise;
            startRotationTime = Time.realtimeSinceStartup;

            if (grounded)
                transform.position += transform.localScale.y * transform.up.normalized;

            lastVelocity = rBody.velocity;
            rBody.velocity = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gravityChanged)
            GetInput();
    }

    void FixedUpdate()
    {
        if (animator)
        {
            CheckIfGrounded();
            animator.SetFloat("Speed", Mathf.Abs(forwardInput) * 3);
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("AirVelocity", rBody.velocity.y); //local y
        }

        if (gravityChanged)
        {
            AnimateRotation();
        }
        else
        {
            UpdateCharacterPosition();
        }
    }

    private void AnimateRotation()
    {
        //Two seconds for turning animation
        if (Time.realtimeSinceStartup - startRotationTime < gravityTurnTime)
        {
            //Vector3 angles = transform;
            float turnAngle = facingForward ? -90.0f : 90.0f;

            if (lastTurnDirection == RotationDirection.Clockwise)
            {
                turnAngle = -turnAngle;
            }

            //Rotate around character's local x axis (global z axis)
            //angles.x += (turnAngle / gravityTurnTime) * Time.fixedDeltaTime; // (angles / time) * time = angles, dimensions match

            transform.rotation *= Quaternion.AngleAxis((turnAngle / gravityTurnTime) * Time.fixedDeltaTime, Vector3.right); //Quaternion.Euler(angles);
        }
        else
        {
            //This locks the rotation with the right orientation
            UpdateCharacterRotation();
            gravityChanged = false;
            rBody.velocity = lastVelocity;
        }
    }

    private void UpdateCharacterRotation()
    {
        Vector3 angles = transform.eulerAngles;
        float angleY = facingForward ? 90 : -90;

        //Rotate around character's local x axis (global z axis)
        switch (GravityDirection)
        {
            case Direction.Down:
                angles = new Vector3(0, angleY, 0);
                break;

            case Direction.Right:
                angles = facingForward ? new Vector3(270, 90, 0) : new Vector3(90, angleY, 0);
                break;

            case Direction.Up:
                angles = new Vector3(180, angleY, 0);
                break;

            case Direction.Left:
                angles = facingForward ? new Vector3(90, angleY, 0) : new Vector3(270, angleY, 0);
                break;
        }

        transform.rotation = Quaternion.Euler(angles);
    }

    private void CheckIfGrounded()
    {
        //RaycastHit[] hits = Physics.RaycastAll(rBody.position - GravityDirection.ToVector3() * .05f, GravityDirection.ToVector3(), distToGround + 10f);

        //foreach (var hit in hits)
        //{
        //    Debug.Log(hit.collider.name + " " + hit.distance);
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

    void UpdateCharacterPosition()
    {
        Vector3 forward = GravityDirection.TurnCounterClockwise().ToVector3();

        if (Mathf.Abs(forwardInput) > inputDeadzone)
        {
            float gravityVel = Vector3.Dot(rBody.velocity, GravityDirection.ToVector3());
            rBody.velocity += forward * forwardVel * (facingForward ? 1 : -1) * 10 * Time.fixedDeltaTime;
            float moveSpeed = Vector3.Dot(rBody.velocity, forward);

            if (grounded)
            {
                if (Mathf.Abs(moveSpeed) > forwardVel)
                {
                    rBody.velocity = gravityVel * GravityDirection.ToVector3() + forward * forwardVel * (facingForward ? 1 : -1);
                }
            }
            else
            {
                if (moveSpeed > 0 && forwardInput > 0 || moveSpeed < 0 && forwardInput < 0)
                {
                    rBody.velocity += forward * forwardVel * (facingForward ? 1 : -1) * (1.0f / 320f) * Time.fixedDeltaTime;

                    if (Mathf.Abs(moveSpeed) > forwardVel)
                    {
                        rBody.velocity = gravityVel * GravityDirection.ToVector3() + forward * forwardVel;
                    }
                }
                else if (moveSpeed < 0 && forwardInput > 0 || moveSpeed > 0 && forwardInput < 0)
                {
                    Vector3 moveFactor = moveSpeed * forward * (1 - 32 * Time.fixedDeltaTime);
                    float newMoveSpeed = Vector3.Dot(moveFactor, forward);

                    if (Math.Abs(newMoveSpeed) > forwardVel / 4.0)
                    {
                        rBody.velocity = gravityVel * GravityDirection.ToVector3() + moveFactor;
                    }
                }
            }
        }
        else
        {
            if (grounded)
            {
                float gravityVel = Vector3.Dot(rBody.velocity, GravityDirection.ToVector3());
                rBody.velocity = gravityVel * GravityDirection.ToVector3();
            }
        }

        rBody.AddForce(9.8f * GravityDirection.ToVector3(), ForceMode.Acceleration);
    }

    public void Reset()
    {
        rBody.rotation = Quaternion.AngleAxis(90, Vector3.up);
        rBody.position = new Vector3(0, 25, -10);
        rBody.velocity = Vector3.zero;
        GravityDirection = Direction.Down;
    }
}