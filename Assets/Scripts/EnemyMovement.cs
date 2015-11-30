using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    private float speed = 3f;
    private float rotateSpeed = 3f;
    private CharacterController cont;
    private Vector3 simGravity;

    void Start()
    {
        simGravity = new Vector3(0, -9.8f, 0);
    }

    void Update()
    {
        cont = GetComponent<CharacterController>();
        cont.SimpleMove(simGravity);
    }
}
