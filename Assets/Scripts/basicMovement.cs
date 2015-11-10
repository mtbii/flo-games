using UnityEngine;
using System.Collections;

public class basicMovement : MonoBehaviour {

    //Variables
    //public float speed = 6.0F;
    //public float gravity = 20.0F;
    //private Vector3 moveDirection = Vector3.zero;
    private Rigidbody rBody;
    private bool grounded;
    private float distToGround = 0;
    public float jumpSpeed = 8.0F;

    //void Update()
    //{
    //    CharacterController controller = GetComponent<CharacterController>();

    //    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    //    moveDirection = transform.TransformDirection(moveDirection);
    //    moveDirection *= speed;

    //    //moveDirection.y -= gravity * Time.deltaTime;
    //    moveDirection.y -= gravity;
    //    controller.Move(moveDirection * Time.deltaTime);
    //}


    //float gravity = -9.8f;

    //void Update()
    //{
    //    Rigidbody rb = this.GetComponent<Rigidbody>();
    //    rb.velocity.y += gravity * Time.deltaTime;
    //}

    //public void ReverseGravity()
    //{
    //    gravity = -gravity;
    //}

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody rb = this.GetComponent<Rigidbody>();
            if (rb.velocity.x > -5)
                rb.AddForce(new Vector3(-10, 0, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody rb = this.GetComponent<Rigidbody>();
            if (rb.velocity.x < 5)
                rb.AddForce(new Vector3(10, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody rb = this.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0, 8f, 0), ForceMode.Impulse);
            //if (CheckIfGrounded())
            //{
            //    rb.AddForce(new Vector3(0, jumpSpeed, 0));
            //}
        }

    }

    void FixedUpdate()
    {
       
    }

    //private bool CheckIfGrounded()
    //{
    //    Rigidbody rb = this.GetComponent<Rigidbody>();
    //    grounded = Physics.Raycast(rb.position - Physics.gravity.normalized * .05f, Physics.gravity.normalized, distToGround + 0.1f);
    //    return grounded;
    //}

}
