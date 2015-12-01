using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    //public int speed = 5;
    //public float gravity = 20;
    //public Vector3 moveSpeed;
    //public Transform target;

    //void Update()
    //{
    //    Move();
    //}

    //void Move()
    //{
    //    CharacterController character = GetComponent<CharacterController>();

    //    if(character.isGrounded)
    //    {
    //        Vector3 delta = target.transform.position - transform.position;
    //        //delta.y = 0;
    //        delta.Normalize();
    //        transform.forward = delta;
    //        moveSpeed = delta * speed;
    //    }

    //    moveSpeed.y -= gravity * Time.deltaTime;
    //    character.Move(moveSpeed * Time.deltaTime);
    //}

    //public float speed = 6f;
    //public float gravity = 20f;
    //public int horizontal;
    //private Vector3 moveDirection = Vector3.zero;

    //void Update()
    //{
    //    CharacterController controller = GetComponent<CharacterController>();

    //    if (Input.GetKey(KeyCode.A))
    //        horizontal = -1;

    //    if (Input.GetKey(KeyCode.D))
    //        horizontal = 1;

    //    if(controller.isGrounded)
    //    {
    //        moveDirection = new Vector3(horizontal, 0, 0);
    //        moveDirection = transform.TransformDirection(moveDirection);
    //        moveDirection *= speed;
    //    }

    //    moveDirection.y -= gravity * Time.deltaTime;
    //    controller.Move(moveDirection * Time.deltaTime);
    //}

    public float speed = 3.0f;
    public float rotateSpeed = 3.0f;
    public float gravity = 9.8f;
    private CharacterController cont;
    public GameObject target;
    private Vector3 direction = Vector3.zero;
    private float distMag;
    private Quaternion rot;
    private float chaseRange = 10f;

    void Start()
    {
        cont = GetComponent<CharacterController>();
        target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        //find player direction
        direction = target.transform.position - transform.position;
        //make it strictly horizontal
        direction.y = 0;
        //measure distance
        distMag = direction.magnitude;

        if (distMag > chaseRange)
        {
            //idle state or patrol area?
            //use waypoints for this
        }

        else
        {
            //chase state
            rot = Quaternion.LookRotation(direction);
            //find target direction and rotate gradually to it
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotateSpeed * Time.deltaTime);
            //cont.SimpleMove(transform.forward * speed);
            cont.Move(transform.forward * speed);

        }
    }

    //private float speed = 3f;
    //private float rotateSpeed = 3f;
    //private CharacterController cont;
    //private Vector3 simGravity;

    //void Start()
    //{
    //    simGravity = new Vector3(0, -9.8f, 0);
    //}

    //void Update()
    //{
    //    cont = GetComponent<CharacterController>();
    //    cont.SimpleMove(simGravity);
    //}
}

