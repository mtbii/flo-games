﻿using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour {

    public Transform target;    //set target from inspector instead of looking in Update
    public float speed = 3f;


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        //make sure object does not change position on z-axis
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        //roate to look at the player
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);   //correcting the original rotation

        //move towards player
        if(Vector3.Distance(transform.position,target.position) > 1f)
        {
            //move if distance from target is greater than 1
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
	}
}
