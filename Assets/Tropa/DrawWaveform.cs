using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GUITexture))]
[RequireComponent (typeof (AudioSource))]

public class DrawWaveform : MonoBehaviour
{
	public int width = 500;
	public int height = 100;
	public Color backgroundColor = Color.black;
	public Color waveformColor = Color.green;
	public int size = 2048;
	
	
	Color[] blank;
	Texture2D texture;
	float[] samples;
	
	
	// Use this for initialization
	void Start()
	{
		samples = new float[size];
		
		// create the texture and assign to the guiTexture:
		texture = new Texture2D(width, height);
		GetComponent<GUITexture>().texture = texture;
		
		// create a 'blank screen' image
		blank = new Color[width * height];
		
		for(var i = 0; i<blank.Length; i++)
		{
			blank[i] = backgroundColor;    
		}
		
		// refresh the display each 100mS
		StartCoroutine(UpdateWaveForm());
	}
	
	IEnumerator UpdateWaveForm()
	{
		while (true)
		{
			GetCurWave();
			yield return new WaitForSeconds(0.1f);
		}    
	}
	
	void GetCurWave()
	{
		// clear the texture
		texture.SetPixels(blank, 0);
		
		// get samples from channel 0 (left)
		GetComponent<AudioSource>().GetOutputData(samples, 0);
		
		// draw the waveform
		for(var i = 0; i < size; i++)
		{
			texture.SetPixel( (int)((width * i) / size), (int)(height * (samples[i]+1f)/2), waveformColor);
		}
		// upload to the graphics card
		texture.Apply();
	}
	
}