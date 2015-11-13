using UnityEngine;
using System.Collections;

public class basicMovement : MonoBehaviour
{

    //Variables
    //Need to clear out unused variables when script is completed
    private Rigidbody rBody;
    private bool grounded;
    private float distToGround;
    public float jumpSpeed;
    public int jumpHeight;
    private Vector3 velocity;

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

    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
       
        //******Change to four main 'if' statements: 1 for each gravity direction*********

        //Movement mechanics for gravity-up and gravity-down (they are the same)
        if (Input.GetKey(KeyCode.A))
        {
            if (rBody.velocity.x > -5)
                rBody.AddForce(new Vector3(-60, 0, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (rBody.velocity.x < 5)
                rBody.AddForce(new Vector3(60, 0, 0));
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

        //Jumping mechanics
        if (Physics.gravity == (Vector3.up * 25f))
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rBody.AddForce(-Vector3.up * 1200);
        }

        if (Physics.gravity == (-Vector3.up * 25f))
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rBody.AddForce(Vector3.up * 1200);
        }


        if (Physics.gravity == (Vector3.right * 25f))
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rBody.AddForce(-Vector3.right * 1200);
        }

        if (Physics.gravity == (-Vector3.right * 25f))
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rBody.AddForce(Vector3.right * 1200);
        }
    }

    //void FixedUpdate()
    //{

    //}

    //private bool CheckIfGrounded()
    //{
    //    //Rigidbody rb = this.GetComponent<Rigidbody>();
    //    grounded = Physics.Raycast(rBody.position - Physics.gravity.normalized * .05f, Physics.gravity.normalized, distToGround + 0.1f);
    //    return grounded;
    //}

    //private bool CheckIfGrounded()
    //{
    //    return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    //}

}
