using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public float speed = 3f;
    public float gravity = 9.8f;
    public Transform target;    //set target from inspector instead of looking in Update
    private CharacterController cont;


    // Use this for initialization
    void Start()
    {
        cont = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //make sure object does not change position on z-axis
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        //roate to look at the player
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);   //correcting the original rotation

        Vector3 moveDirection = target.position - transform.position;
        //moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.y -= gravity * Time.deltaTime;
        cont.Move(moveDirection.normalized * Time.deltaTime * speed);
        //cont.Move(moveDirection * speed);





        //if(cont.isGrounded)
        //{
        //    //move towards player
        //    if (Vector3.Distance(transform.position, target.position) > .01f)
        //    {
        //        //move if distance from target is greater than 1
        //        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        //    }
        //}

        ////move towards player
        //if (Vector3.Distance(transform.position, target.position) > .01f)
        //{
        //    //move if distance from target is greater than 1
        //    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        //}

        //transform.Translate(new Vector3(0,-gravity,0) * Time.deltaTime);

    }
}



//using UnityEngine;
//using System.Collections;

//public class EnemyScript : MonoBehaviour {

//    private Transform target;       //enemy's target
//    private float moveSpeed = 3;    //movement speed
//    private float rotateSpeed = 3;  //speed of turning
//    private float minRange = 10f;
//    private float maxRange = 10f;
//    private float stop = 0;
//    private Transform curTransform; //current transform data of enemy

//    void Awake()
//    {
//        curTransform = transform;   //cache transform data for easy access/performance
//    }

//	void Start()
//    {
//        target = GameObject.FindWithTag("Player").transform;    //target player
//    }

//	void Update ()
//    {
//        //rotate to look at player
//        float distance = Vector3.Distance(curTransform.position, target.position);

//        if (distance <= maxRange && distance >= minRange)
//        {
//            curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(target.position - curTransform.position), rotateSpeed * Time.deltaTime);
//        }

//        else if (distance <= minRange && distance > stop)
//        {
//            //move towards player
//            curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(target.position - curTransform.position), rotateSpeed * Time.deltaTime);
//            curTransform.position += curTransform.forward * moveSpeed * Time.deltaTime;
//        }

//        else if (distance <= stop)
//        {
//            curTransform.rotation = Quaternion.Slerp(curTransform.rotation, Quaternion.LookRotation(target.position - curTransform.position), rotateSpeed * Time.deltaTime);
//        }
//	}
//}
