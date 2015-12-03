using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Goal : MonoBehaviour
{
    public SideScrollCharacterController player;

    void OnTriggerEnter(Collider other)
    {
        GameManager.instance.PlayerWon();
    }
}
