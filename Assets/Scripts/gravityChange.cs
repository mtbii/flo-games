using UnityEngine;
using System.Collections;

public class gravityChange : MonoBehaviour {

    public Vector3 gravity;

	// Use this for initialization
	void Start () {

        //gravity = Physics.gravity;
	}
	

	void FixedUpdate () {

        Rigidbody rb = this.GetComponent<Rigidbody>();
        gravity = -Vector3.up * 9.8f;
        rb.AddForce(gravity);
        //rb.AddForce(transform.up * -1 * gravity);

        //Physics.gravity = gravity;

        //if(Input.GetKeyUp("UpArrow"))
        //{
        //    gravity.x = 0;
        //    gravity.y = 25;
        //    gravity.z = 0;
        //}

        //if (Input.GetKeyUp("DownArrow"))
        //{
        //    gravity.x = 0;
        //    gravity.y = -25;
        //    gravity.z = 0;
        //}

        //if (Input.GetKeyUp("LeftArrow"))
        //{
        //    gravity.x = -25;
        //    gravity.y = 0;
        //    gravity.z = 0;
        //}

        //if (Input.GetKeyUp("RightArrow"))
        //{
        //    gravity.x = 0;
        //    gravity.y = 25;
        //    gravity.z = 0;
        //}


        //if (Input.GetButtonUp("UpArrow"))
        //{
        //    rBody.position -= Physics.gravity.normalized;
        //    Quaternion delta = facingForward ? Quaternion.Euler(-90, 0, 0) : Quaternion.Euler(90, 0, 0);
        //    Quaternion worldDelta = Quaternion.Euler(0, 0, 90);
        //    Physics.gravity = worldDelta * Physics.gravity;
        //    rBody.rotation *= delta;
        //}
    }
}
