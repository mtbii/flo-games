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

        Physics.gravity = -Vector3.up * 25f;
        //rBody.AddForce(-Vector3.up * 25f);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Physics.gravity = Vector3.up * 25f;
            //rBody.AddForce(Vector3.up * 25f);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            Physics.gravity = -Vector3.up * 25f;
            //rBody.AddForce(-Vector3.up * 25f);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Physics.gravity = -Vector3.right * 25f;
            //rBody.AddForce(-Vector3.right * 25f);


        if (Input.GetKeyDown(KeyCode.RightArrow))
            Physics.gravity = Vector3.right * 25f;
            //rBody.AddForce(Vector3.right * 25f);
    }
}
