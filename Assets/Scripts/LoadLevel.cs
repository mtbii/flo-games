using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

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
            Application.LoadLevel(Application.loadedLevel);
    }
}
