using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	static public int player1 = 0;
	static public int player2 = 0;
    public double time = 0;

    void Update()
    {
        time += Time.deltaTime;
    }

	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width/2-Screen.width/10, 0, Screen.width/5, Screen.height/15), "Time: " + time.ToString("0.00"));
	}
}
