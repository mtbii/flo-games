using UnityEngine;
using System.Collections;
using System;
using Assets.FuseModel.Scripts;

public class SideScrollCharacterController : MonoBehaviour
{
    public float inputDeadzone = 0.1f;
    public float forwardVel = 1;

    private float forwardInput;
    private Rigidbody rBody;
    private Animator animator;
    private bool grounded;

    private Direction gravityDir;
    private float distToGround = 0;
    private bool gravityChanged = false;
    private bool facingForward = true;

    public enum Direction
    {
        Down,
        Right,
        Up,
        Left
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
            //rBody.position -= Physics.gravity.normalized;
            //Quaternion worldDelta = Quaternion.Euler(0, 0, -90);
            //Physics.gravity = worldDelta * Physics.gravity;

            GravityDirection = GravityDirection.TurnClockwise();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            //rBody.position -= Physics.gravity.normalized;
            //Quaternion worldDelta = Quaternion.Euler(0, 0, 90);
            //Physics.gravity = worldDelta * Physics.gravity;

            GravityDirection = GravityDirection.TurnCounterClockwise();
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
            animator.SetFloat("Speed", Mathf.Abs(forwardInput) * 3);
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("AirVelocity", rBody.velocity.y); //local y
        }

        //This prevents a bug with Euler angles
        if (gravityChanged)
        {
            UpdateGravity();
        }

        UpdateCharacter();
    }

    private void UpdateGravity()
    {
        Vector3 angles = transform.eulerAngles;

        //Rotate around character's local x axis (global z axis)
        switch (GravityDirection)
        {
            case Direction.Down:
                angles.x = 0;
                break;

            case Direction.Right:
                angles.x = facingForward ? 270 : 90;
                break;

            case Direction.Up:
                angles.x = 180;
                break;

            case Direction.Left:
                angles.x = facingForward ? 90 : 270;
                break;
        }

        transform.rotation = Quaternion.Euler(angles);
        gravityChanged = false;
    }

    private void CheckIfGrounded()
    {
        grounded = Physics.Raycast(rBody.position - GravityDirection.ToVector3() * .05f, GravityDirection.ToVector3(), distToGround + 0.1f);
    }

    void UpdateCharacter()
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
