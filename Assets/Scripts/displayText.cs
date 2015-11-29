using UnityEngine;
using System.Collections;

public class displayText : MonoBehaviour {

    private Rigidbody rBody;
    private float velocity;
    private float distToGround = 0.0f;
    private bool grounded;
    private float forwardInput;

    void Start()
    {
        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();

        forwardInput = 0;
    }

    void FixedUpdate()
    {
        velocity = rBody.velocity.magnitude;

        forwardInput = Input.GetAxis("Horizontal");
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

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 180, 100), "Velocity = " + velocity + "\n" + "forwardInput = "  + forwardInput + "\n" + "Grounded = " + grounded);
    }
}
