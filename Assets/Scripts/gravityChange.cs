using UnityEngine;
using System.Collections;

public class gravityChange : MonoBehaviour
{

    public Vector3 gravity;
    private Rigidbody rBody;

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

        gravity = -Vector3.up * 100f;
        //gravity = new Vector3(0,-25,0);                       //use this as 'direction' for Raycasts
        rBody.AddForce(gravity);
    }


    void FixedUpdate()
    {

        //Keep if needed
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    gravity = new Vector3(0,25,0);
        //    rBody.AddForce(gravity);
        //}

        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    gravity = new Vector3(0,-25,0);
        //    rBody.AddForce(gravity);
        //}

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    gravity = -Vector3.right * 9.8f;
        //    rBody.AddForce(gravity);
        //}

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    gravity = Vector3.right * 9.8f;
        //    rBody.AddForce(gravity);
        //}

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gravity = Vector3.up;
            Physics.gravity = gravity * 25f;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gravity = -Vector3.up;
            Physics.gravity = gravity * 25f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gravity = -Vector3.right;
            Physics.gravity = gravity * 25f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gravity = Vector3.right;
            Physics.gravity = gravity * 25f;
        }







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
