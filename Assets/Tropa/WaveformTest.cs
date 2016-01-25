﻿
using UnityEngine;
using System.Collections;

public class WaveformTest : MonoBehaviour {


	int resolution = 60;
	
	float[] waveForm;
	float[] samples;

	public GameObject sampleline;
	public float tempoShit;
	public float offset;
	// Use this for initialization
	void Start () {
		
		resolution = GetComponent<AudioSource>().clip.frequency / resolution;
		
		samples = new float[GetComponent<AudioSource>().clip.samples*GetComponent<AudioSource>().clip.channels];
		GetComponent<AudioSource>().clip.GetData(samples,0);
		
		waveForm = new float[(samples.Length/resolution)];
		
		for (int i = 0; i < waveForm.Length; i++)
		{
			waveForm[i] = 0;
			
			for(int ii = 0; ii<resolution; ii++)
			{
				waveForm[i] += Mathf.Abs(samples[(i * resolution) + ii]);
			}          
			
			waveForm[i] /= resolution;
		}

		for (int i = 0; i < waveForm.Length - 1; i++)
		{

			GameObject g = GameObject.Instantiate(sampleline) as GameObject;
			Vector3 sv = new Vector3(i * .01f, waveForm[i]*10 , 0);
			Vector3 ev = new Vector3(i * .01f, -waveForm[i] * 10, 0);

			g.transform.position = new Vector3(i * .01f,0,0);
			g.transform.localScale = new Vector3(1,  waveForm[i]*500 , 1);
			//Debug.DrawLine(sv, ev, Color.yellow);
		}


		//create tempo line

		float f = (waveForm.Length - 1) / tempoShit;
		for (int i = 0; i < (waveForm.Length - 1) / tempoShit; i++)
		{
			GameObject g = GameObject.Instantiate(sampleline) as GameObject;
			g.GetComponent<SpriteRenderer>().color = Color.blue;
			g.GetComponent<SpriteRenderer>().sortingOrder = 100;
			g.transform.position = new Vector3(((i * tempoShit) + offset) * .01f,0,0);

		}

		sampleline.GetComponent<SpriteRenderer>().color = Color.red;
		sampleline.GetComponent<SpriteRenderer>().sortingOrder = 100;

		Camera.main.transform.parent = sampleline.transform;
	}
	
	// Update is called once per frame
	void Update () {




		int current = GetComponent<AudioSource>().timeSamples / resolution;
		current *= 2;
		Vector3 c = new Vector3(current*.01f,0,0);
		sampleline.transform.position = c;
		return;


//
//		return;
//		for (int i = 0; i < waveForm.Length - 1; i++)
//		{
//			Vector3 sv = new Vector3(i * .01f, waveForm[i]*10 , 0);
//			Vector3 ev = new Vector3(i * .01f, -waveForm[i] * 10, 0);
//			
//			Debug.DrawLine(sv, ev, Color.yellow);
//		}
//		
//		int current = GetComponent<AudioSource>().timeSamples / resolution;
//		current *= 2;
//		
//		Vector3 c = new Vector3(current*.01f,0,0);
//		
//		Debug.DrawLine(c, c + Vector3.up * 10, Color.white);
	}
}
