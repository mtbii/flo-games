using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class LoadLevel : MonoBehaviour
{
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "enemy")
            GameManager.instance.KillPlayer("Eraser");
        //Application.LoadLevel(Application.loadedLevel);
    }
}
