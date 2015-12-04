using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class LoadLevel : MonoBehaviour
{
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "enemy")
            GameManager.instance.KillPlayer("Eraser");

        if ((coll.gameObject.tag == "Level") && coll.relativeVelocity.magnitude > 5)
        {
            Debug.Log("Relative velocity magnitude: " + coll.relativeVelocity.magnitude);

            ContactPoint contact = coll.contacts[0];
            Vector3 dir = coll.gameObject.transform.position - rBody.position;
            //dir = dir.normalized;
            //print("Collision's position = " + coll.gameObject.transform.position);
            //print("RigidBody's position = " + rBody.position);
            //print("Rigidbody's velocity = " + rBody.velocity);
            //print("Normalized difference = " + dir);
            //print(contact.thisCollider.name + " hit " + contact.otherCollider.name + "\n" + "Velocity in [x,y,z] = " + rBody.velocity);
            //foreach (ContactPoint contact in coll.contacts)
            //{
            //    print(contact.thisCollider.name + " hit " + contact.otherCollider.name + "\n" + "Velocity in [x,y,z] = " + rBody.velocity);

            //    Debug.DrawRay(contact.point, contact.normal, Color.red);
            //}

            //GameManager.instance.KillPlayer("Impact");
        }
            
        //Application.LoadLevel(Application.loadedLevel);
    }
}
