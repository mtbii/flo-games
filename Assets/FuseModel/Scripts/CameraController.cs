using UnityEngine;
using System.Collections;
using System;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float lookSmooth = 0.09f;
    public Vector3 offsetFromTarget = new Vector3(0, 1, -10);
    public float roll = 0;


    Vector3 destination = Vector3.zero;
    SideScrollCharacterController character;

    public float Roll
    {
        get { return roll; }
    }

	// Use this for initialization
	void Start () {
	
	}

	public void SetCameraTarget(Transform t)
    {
        target = t;

        if (target != null)
        {
            if (target.GetComponent<SideScrollCharacterController>())
            {
                character = target.GetComponent<SideScrollCharacterController>();
            }
            else
                Debug.LogError("The camera's target needs a SideScrollCharacterController.");
        }
        else
            Debug.LogError("The camera needs a target.");
    }
	
    // Update is called once per frame
	void Update () {
        //moving
        MoveToTarget();
        //rotating
        LookAtTarget();
        //re-orient
        Reorient();
	}

    private void Reorient()
    {

    }

    void MoveToTarget()
    {
        destination = target.position + offsetFromTarget;
        transform.position = destination;
    }

    void LookAtTarget()
    {

    }
}
