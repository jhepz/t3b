using UnityEngine;
using System.Collections;

public class TempoManager : MonoBehaviour {

	public AudioSource targetSource;
	public float offset = 0;
	//show tempo every xxx
	public float tempo;
	public float threshold;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI()
	{
		if (targetSource.time < offset)
			return;

		if (targetSource.time % tempo < threshold)
		{
			GUI.Button(new Rect(0,0,200,200),"BEAT");
		}
	}
}
