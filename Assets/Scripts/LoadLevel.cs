using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class LoadLevel : MonoBehaviour
{

    public Transform target;
    private Rigidbody rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "enemy")
            GameManager.instance.KillPlayer("Eraser");
        //Application.LoadLevel(Application.loadedLevel);
    }
}
