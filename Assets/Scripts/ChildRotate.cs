using UnityEngine;
using System.Collections;

public class ChildRotate : MonoBehaviour {

    public Transform target;
    public Transform[] waypoints;
    private Vector3 waypointTarget;
    private int currentWaypoint = 0;
    private float dampLook = 1.5f;
    private CharacterController cont;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
