using UnityEngine;
using System.Collections;

public class testRotation : MonoBehaviour {

    public CapsuleCollider capsuleCollider;
    private Rigidbody rBody;
    private Vector3 surfaceNormal;
    private Vector3 myNormal;
    private Transform myTransform;
    private float distToGround;
    private float gravity = 15f;
    private float jumpSpeed = 15f;
    private float doubleGravity;
    private bool gravUp, gravDown, gravLeft, gravRight;
    


	// Use this for initialization
	private void Start () {

        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();

        else
            Debug.LogError("The character needs a rigidBody.");

        rBody.AddForce(-gravity * rBody.mass * myNormal);

        //gravUp = true;
        //gravDown = false;
        //gravLeft = false;
        //gravRight = false;
        //doubleGravity = -gravity * 2 * rBody.mass;
        myNormal = transform.up;
        myTransform = transform;

        //Distance is only from y-axis range of collider to center of collider
        //distToGround = capsuleCollider.bounds.extents.y - capsuleCollider.center.y;

        //Distance is simply .01f
        distToGround = .01f;
    }
	
    private void Update()
    {
        //Makes sure player does not have movement along z-axis
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        if(!isGrounded())
            //rBody.AddForce(new Vector3())
            rBody.AddForce(-gravity * 2 * rBody.mass * myNormal);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
                rBody.velocity += jumpSpeed * myNormal;
        }

        if(Input.GetKey(KeyCode.A))
        {
            if (rBody.velocity.x > -5)
                rBody.AddForce(new Vector3(-60, 0, 0));
        }

        if(Input.GetKey(KeyCode.D))
        {
            if (rBody.velocity.x < 5)
                rBody.AddForce(new Vector3(60, 0, 0));
        }
    }

	private void FixedUpdate () {

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            //transform.Rotate(0, 180, 0);
            rBody.AddForce(gravity * rBody.mass * myNormal);
        }

        //if (Input.GetKeyUp(KeyCode.UpArrow))
        //{
        //    //transform.Rotate(0, 180, 0);
        //    rBody.AddForce(new Vector3(0, 25f, 0));
        //}

        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //    rBody.AddForce(new Vector3(0,-25f, 0));

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    rBody.AddForce(new Vector3(-25f, 0, 0));

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //    rBody.AddForce(new Vector3(25f,0,0));
    }

    private bool isGrounded ()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround))
            return true;

        else
            return false;

        //return Physics.Raycast(transform.position, -Vector3.up, distToGround);
        


        //if (gravLeft == true)
        //    return Physics.Raycast(transform.position, -Vector3.right, distToGround);

        //else if (gravRight == true)
        //    return Physics.Raycast(transform.position, Vector3.right, distToGround);

        //else if (gravUp == true)
        //    return Physics.Raycast(transform.position, Vector3.up, distToGround);

        //else 
        //    return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }
}
