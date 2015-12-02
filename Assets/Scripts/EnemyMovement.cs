using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    
    public Transform target;
    public Transform[] waypoints;
    private Vector3 waypointTarget;
    private int currentWaypoint = 0;
    private bool waypointLoop = true;
    private float chaseSpeed = 10;
    private float patrolSpeed = 16;
    private float pauseDuration = 1;
    private float dampLook = 5f;
    private float waypointTime;
    private float chaseRange = 1f;
    private CharacterController cont;
    private Vector3 moveDirection = Vector3.zero;
    private float enemyDistance;

    void Start()
    {
        cont = GetComponent<CharacterController>();
    }

    void Update()
    {
        //make sure object does not change position on z-axis
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        enemyDistance = Vector3.Distance(transform.position, target.position);
        Debug.LogError("enemyDistance = " + enemyDistance);

        if (currentWaypoint < waypoints.Length)
        {
            if (enemyDistance > 1.5f)
                Patrol();

            else if ((enemyDistance <= 1.5f) && (target.position.y > transform.position.y))
                Patrol();

            else
                Chase();
        }

        else
        {
            if (waypointLoop)
                currentWaypoint = 0;
        }

    }

    void Patrol()
    {
        waypointTarget = waypoints[currentWaypoint].position;
        waypointTarget.y = transform.position.y;
        moveDirection = waypointTarget - transform.position;

        if (moveDirection.magnitude < 0.5)
        {
            if (waypointTime == 0)
                waypointTime = Time.time;   //Pause over the waypoint

            if ((Time.time - waypointTime) >= pauseDuration)
            {
                currentWaypoint++;
                waypointTime = 0;
            }

        }

        else
        {
            moveDirection = waypointTarget - transform.position;
            Quaternion rotation = Quaternion.LookRotation(waypointTarget - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampLook);
            cont.SimpleMove(moveDirection.normalized * patrolSpeed * Time.deltaTime);
        }
    }

    void Chase()
    {
        float dist1 = Vector3.Distance(transform.position, waypoints[0].position);
        float dist2 = Vector3.Distance(transform.position, waypoints[1].position);
        float minDist = Mathf.Min(dist1, dist2);

        //TODO Fix issue with enemy chasing player, then going out of it's "chase radius" where it is supposed to go back to nearest waypoint
        if (chaseRange > minDist)
        {
            moveDirection = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampLook);
            cont.SimpleMove(moveDirection.normalized * chaseSpeed * Time.deltaTime);
        }

        else
        {
            if (dist1 < dist2)
            {
                moveDirection = waypoints[0].position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(waypoints[0].position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampLook);
                cont.SimpleMove(moveDirection.normalized * patrolSpeed * Time.deltaTime);
            }

            if (dist2 < dist1)
            {
                moveDirection = waypoints[1].position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(waypoints[1].position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampLook);
                cont.SimpleMove(moveDirection.normalized * patrolSpeed * Time.deltaTime);
            }
        }   
    }
}

