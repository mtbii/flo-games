using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    public SideScrollCharacterController player;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        //player.Reset();
    }
}
