using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class DetectDamage : MonoBehaviour {

    public GameObject frontCube;
    public GameObject feetCube;
    private Rigidbody rBody;
    private bool feetEnabled = false;
    private bool frontEnabled = false;
    public Vector3 vel;

	void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }
	
    void FixedUpdate()
    {
        vel = rBody.velocity;

        if ((Mathf.Abs(vel.x) > Mathf.Abs(vel.y)) && (Mathf.Abs(vel.x) >= 7))
        {
            frontEnabled = true;
            feetCube.SetActive(false);
            frontCube.SetActive(true);
        }

        if ((Mathf.Abs(vel.x) < Mathf.Abs(vel.y)) && (Mathf.Abs(vel.y) >= 7))
        {
            feetEnabled = true;
            feetCube.SetActive(true);
            frontCube.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if(feetEnabled == true)
            GameManager.instance.KillPlayer("Impact on feet");

        if (frontEnabled == true)
            GameManager.instance.KillPlayer("Impact on front");
    }

    void OnCollisionExit()
    {
        //Vector3 vel = rBody.velocity;

        
    }

	// Update is called once per frame
	void Update () {

        //print("rBody velocity: " + rBody.velocity);
        if (feetEnabled)
            print("feetEnabled = true" + "\n" + "Velocity = " + rBody.velocity);
        if (frontEnabled)
            print("frontEnabled = true" + "\n" + "Velocity = " + rBody.velocity);
	}
}
