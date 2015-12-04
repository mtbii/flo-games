using UnityEngine;
using Assets.FuseModel.Scripts;
using System.Collections;
using System;

public class CameraController : MonoBehaviour
{
    public MonoBehaviour target;
    public Vector3 offsetFromTarget = new Vector3(0, 1, -10);

    Vector3 destination;
    private bool isRotating;
    private float startRotationTime = 0;
    private float gravityTurnTime;

    private SideScrollCharacterController targetCharacter;
    private SideScrollCharacterController.Direction targetLastGravityDirection;
    private SideScrollCharacterController.RotationDirection targetLastRotation;

    // Use this for initialization
    void Start()
    {
        isRotating = false;
        destination = Vector3.zero;
        targetCharacter = (SideScrollCharacterController)target;
        gravityTurnTime = targetCharacter.gravityTurnTime;
        targetLastGravityDirection = targetCharacter.GravityDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetCharacter.GravityDirection != targetLastGravityDirection)
        {
            isRotating = true;
            targetLastGravityDirection = targetCharacter.GravityDirection;
            startRotationTime = Time.realtimeSinceStartup;
        }

        if (isRotating)
        {
            AnimateRotation();
        }
        else
        {
            //moving
            MoveToTarget();
        }
    }

    private void AnimateRotation()
    {
        if (Time.realtimeSinceStartup - startRotationTime < gravityTurnTime)
        {
            //Vector3 angles = transform;
            float turnAngle = 90.0f;

            if (targetCharacter.LastTurnDirection == SideScrollCharacterController.RotationDirection.Clockwise)
            {
                turnAngle = -turnAngle;
            }

            transform.rotation *= Quaternion.AngleAxis((turnAngle / gravityTurnTime) * Time.fixedDeltaTime, Vector3.forward);

            var dir = destination - target.transform.position;
            destination = target.transform.position + Quaternion.AngleAxis((turnAngle / gravityTurnTime) * Time.fixedDeltaTime, Vector3.forward) * dir;
            transform.position = destination;
        }
        else
        {
            isRotating = false;
        }
    }

    void MoveToTarget()
    {
        destination = target.transform.position + Quaternion.Euler(0, 0, ((SideScrollCharacterController)target).GravityDirection.ToAngle()) * offsetFromTarget;
        transform.position = destination;
        transform.rotation = Quaternion.Euler(0, 0, ((SideScrollCharacterController)target).GravityDirection.ToAngle());
    }
}
