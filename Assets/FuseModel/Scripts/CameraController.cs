using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {
    
    public MonoBehaviour target;
    public float lookSmooth = 0.09f;
    public Vector3 offsetFromTarget = new Vector3(0, 1, -10);
    public float roll = 0;
    Vector3 destination = Vector3.zero;

    public float Roll
    {
        get { return roll; }
    }

	// Use this for initialization
	void Start () {

	}
	
    // Update is called once per frame
	void Update () {
        //moving
        MoveToTarget();
        //rotating
        LookAtTarget();
	}
    
    void MoveToTarget()
    {
        destination = target.transform.position + Quaternion.Euler(0,0, ((SideScrollCharacterController)target).Roll)*offsetFromTarget;
        transform.position = destination;
        transform.rotation = Quaternion.Euler(0, 0, ((SideScrollCharacterController)target).Roll);
    }

    void LookAtTarget()
    {

    }
}
