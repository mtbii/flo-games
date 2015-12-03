using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
    public double time = 0;
    public bool runTimer = true;
    private float startTime;

    void Update()
    {
        if (runTimer)
        {
            time += Time.deltaTime;
        }
    }

    void OnGUI()
    {
        if (time < 5)
        {
            GUI.Box(new Rect(Screen.width / 2 - Screen.width / 10, 4*Screen.height / 15, Screen.width / 5, Screen.height / 15), "Get to the next page!");
        }

        if (runTimer)
        {
            GUI.Box(new Rect(Screen.width / 2 - Screen.width / 10, 0, Screen.width / 5, Screen.height / 15), "Time: " + time.ToString("0.00"));
        }
    }
}
