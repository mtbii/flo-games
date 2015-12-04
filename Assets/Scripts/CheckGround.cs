using UnityEngine;
using System.Collections;
using System.Reflection;

public class CheckGround : MonoBehaviour
{
    public SideScrollCharacterController character;
    public string fieldName = "grounded";

    private FieldInfo fieldInfo;

    // Use this for initialization
    void Start()
    {
        fieldInfo = character.GetType().GetField(fieldName);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Level")
        {
            fieldInfo.SetValue(character, true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Level")
        {
            fieldInfo.SetValue(character, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Level")
        {
            fieldInfo.SetValue(character, false);
        }
    }
}
