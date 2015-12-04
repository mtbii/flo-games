using UnityEngine;
using System.Collections;
using System;
using Assets.FuseModel.Scripts;

public class SideScrollCharacterController : MonoBehaviour
{
    public float inputDeadzone = 0.1f;
    public float forwardVel = 1;
    public float gravityTurnTime = 1;
    public int slopeAngleLimit = 60;
    public float airSpeedDamping = .1f;

    private float forwardInput;
    private Rigidbody rBody;
    private Animator animator;
    private bool grounded;
    private float terminalVelocity = 7;
    private float terminalLandSpeed = 1;

    private float startRotationTime = 0;
    //private float rotationParam = 0;

    private Vector3 lastVelocity;
    private RotationDirection lastTurnDirection;
    private Direction gravityDir;
    private float distToGround = 0.0f;
    private bool gravityChanged = false;
    private bool facingForward = true;
    private bool isLeavingGround = false;
    private bool wasGrounded = false;
    private Vector3 forwardDir;

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

    public bool IsDead { get; internal set; }
    public bool HasWon { get; internal set; }

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
        forwardInput = 0;// Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            forwardInput = -1;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            forwardInput = 1;
        }

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

        wasGrounded = false;
        if (Input.GetKeyUp(KeyCode.E))
        {
            //Changing GravityDirection sets gravityChanged to true
            GravityDirection = GravityDirection.TurnClockwise();
            lastTurnDirection = RotationDirection.Clockwise;
            startRotationTime = Time.realtimeSinceStartup;

            if (grounded)
            {
                wasGrounded = true;
                transform.position += transform.localScale.y * transform.up.normalized;
            }

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
            {
                wasGrounded = true;
                transform.position += transform.localScale.y * transform.up.normalized;
            }

            lastVelocity = rBody.velocity;
            rBody.velocity = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gravityChanged)
        {
            GetInput();
            UpdateCharacterPosition();
        }
    }

    void FixedUpdate()
    {
        if (animator)
        {
            animator.SetFloat("Speed", Mathf.Abs(forwardInput) * 3);
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("AirVelocity", rBody.velocity.y); //local y
        }

        if (gravityChanged)
        {
            AnimateRotation();
        }
        //Debug.Log(rBody.velocity);
        //else
        //{
        //    GetInput();
        //    UpdateCharacterPosition();
        //}
    }

    private void AnimateRotation()
    {
        //Two seconds for turning animation
        if (Time.realtimeSinceStartup - startRotationTime < gravityTurnTime)
        {
            //Vector3 angles = transform;
            float turnAngle = facingForward ? -90.0f : 90.0f;
            var lastGravityDirection = GravityDirection;

            if (lastTurnDirection == RotationDirection.Clockwise)
            {
                lastGravityDirection = lastGravityDirection.TurnCounterClockwise();
                turnAngle = -turnAngle;
            }
            else
            {
                lastGravityDirection = lastGravityDirection.TurnClockwise();
            }

            //Rotate around character's local x axis (global z axis)
            //angles.x += (turnAngle / gravityTurnTime) * Time.fixedDeltaTime; // (angles / time) * time = angles, dimensions match
            var posDelta = 10 * transform.localScale.y * lastGravityDirection.ToVector3();

            transform.rotation *= Quaternion.AngleAxis((turnAngle / gravityTurnTime) * Time.fixedDeltaTime, Vector3.right); //Quaternion.Euler(angles);
        }
        else
        {
            //This locks the rotation with the right orientation
            UpdateCharacterRotation();
            gravityChanged = false;
            wasGrounded = false;
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Level")
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                var contact = collision.contacts[i];
                if (Vector3.Angle(GravityDirection.ToVector3(), contact.point - rBody.position) >= 90)
                {
                    grounded = false;
                    return;
                }
            }

            isLeavingGround = false;
            grounded = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Level")
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                var contact = collision.contacts[i];
                if (Vector3.Angle(GravityDirection.ToVector3(), contact.point - rBody.position) >= 90 + slopeAngleLimit)
                {
                    grounded = false;
                    return;
                }
            }

            isLeavingGround = false;
            grounded = true;

            //Vector3 normal = new Vector3();
            //foreach (var contact in collision.contacts)
            //{
            //    normal += contact.normal;
            //}
            //normal /= collision.contacts.Length;
            //normal.Normalize();

            //forwardDir = Vector3.Cross(Vector3.back, normal).normalized;

            //if (Vector3.Angle(GravityDirection.ToVector3(), forwardDir) < 150)
            //{
            //    forwardDir = GravityDirection.TurnCounterClockwise().ToVector3();
            //}
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Level")
        {
            isLeavingGround = true;

            //After 1/10 of a second
            StartCoroutine(ExecuteWithDelay(0.1f, () =>
            {
                if (isLeavingGround)
                {
                    grounded = false;
                    isLeavingGround = false;
                }
            }));
        }
    }

    IEnumerator ExecuteWithDelay(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        if (action != null)
        {
            action();
        }
    }

    void UpdateCharacterPosition()
    {
        //if (!grounded)
        forwardDir = GravityDirection.TurnCounterClockwise().ToVector3();

        var forwardForce = forwardInput * forwardVel * forwardDir;
        var gravityForce = 9.8f * GravityDirection.ToVector3();

        Debug.Log(forwardForce);

        if (grounded)
        {
            if (forwardForce.magnitude < .01)
            {
                rBody.velocity = Vector3.zero;
            }
            else
            {
                rBody.velocity += forwardForce;
                rBody.AddForce(gravityForce, ForceMode.Acceleration);

                //Limit speed
                if (rBody.velocity.x > terminalLandSpeed || rBody.velocity.x < -terminalLandSpeed)
                {
                    rBody.velocity = new Vector3(rBody.velocity.x > 0 ? terminalLandSpeed : -terminalLandSpeed, rBody.velocity.y);
                }

                if (rBody.velocity.y > terminalLandSpeed || rBody.velocity.y < -terminalLandSpeed)
                {
                    rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y > 0 ? terminalLandSpeed : -terminalLandSpeed);
                }
            }
        }
        else
        {
            rBody.velocity += forwardForce * airSpeedDamping;
            rBody.AddForce(gravityForce, ForceMode.Acceleration);
        }

        //Limit speed
        if (rBody.velocity.x > terminalVelocity || rBody.velocity.x < -terminalVelocity)
        {
            rBody.velocity = new Vector3(rBody.velocity.x > 0 ? terminalVelocity : -terminalVelocity, rBody.velocity.y);
        }

        if (rBody.velocity.y > terminalVelocity || rBody.velocity.y < -terminalVelocity)
        {
            rBody.velocity = new Vector3(rBody.velocity.x, rBody.velocity.y > 0 ? terminalVelocity : -terminalVelocity);
        }

        //float gravityVel = Vector3.Dot(rBody.velocity, GravityDirection.ToVector3());
        //if (Mathf.Abs(forwardInput) > inputDeadzone)
        //{
        //    rBody.velocity += forwardDir * forwardVel * (facingForward ? 1 : -1) * 10 * Time.fixedDeltaTime;
        //    float moveSpeed = Vector3.Dot(rBody.velocity, forwardDir);

        //    if (grounded)
        //    {
        //        if (Mathf.Abs(moveSpeed) > forwardVel)
        //        {
        //            rBody.velocity = gravityVel * GravityDirection.ToVector3() + forwardDir * forwardVel * (facingForward ? 1 : -1);
        //        }
        //    }
        //    else
        //    {
        //        if (moveSpeed > 0 && forwardInput > 0 || moveSpeed < 0 && forwardInput < 0)
        //        {
        //            rBody.velocity += forwardDir * forwardVel * (facingForward ? 1 : -1) * (1.0f / 320f) * Time.fixedDeltaTime;

        //            if (Mathf.Abs(moveSpeed) > forwardVel)
        //            {
        //                rBody.velocity = gravityVel * GravityDirection.ToVector3() + forwardDir * forwardVel;
        //            }
        //        }
        //        else if (moveSpeed < 0 && forwardInput > 0 || moveSpeed > 0 && forwardInput < 0)
        //        {
        //            Vector3 moveFactor = moveSpeed * forwardDir * (1 - 32 * Time.fixedDeltaTime);
        //            float newMoveSpeed = Vector3.Dot(moveFactor, forwardDir);

        //            if (Math.Abs(newMoveSpeed) > forwardVel / 4.0)
        //            {
        //                rBody.velocity = gravityVel * GravityDirection.ToVector3() + moveFactor;
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    if (grounded)
        //    {
        //        //float gravityVel = Vector3.Dot(rBody.velocity, GravityDirection.ToVector3());
        //        //rBody.velocity = gravityVel * GravityDirection.ToVector3();
        //        rBody.velocity = gravityVel * GravityDirection.ToVector3();
        //    }
        //}
    }

    //public void Reset()
    //{
    //    rBody.rotation = Quaternion.AngleAxis(90, Vector3.up);
    //    rBody.position = new Vector3(0, 25, -10);
    //    rBody.velocity = Vector3.zero;
    //    GravityDirection = Direction.Down;
    //}
}
