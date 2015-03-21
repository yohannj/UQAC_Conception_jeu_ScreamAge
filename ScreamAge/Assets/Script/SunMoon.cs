using UnityEngine;
using System.Collections;

public class SunMoon : MonoBehaviour {

	public float darkness = 0.2f;
	public Color dark;
	private bool transitionning = false;
	private float initialIntensity;
	private Color initialColor;

	// Use this for initialization
	void Start () {
		initialIntensity = GetComponent<Light>().intensity;
		initialColor = GetComponent<Light>().color;
	}
	
	// Update is called once per frame
	void Update () {
		if(transitionning){
			GetComponent<Light>().intensity -= darkness * Time.deltaTime * 0.2f;
			GetComponent<Light>().color = Color.Lerp(initialColor, dark, Time.deltaTime * 0.2f);

			if(GetComponent<Light>().intensity <= initialIntensity-darkness)
				transitionning = false;
		}
	}

	public void StartNight(){
		transitionning = true;
	}

	public void reset(){
		GetComponent<Light> ().intensity = initialIntensity;
		GetComponent<Light> ().color = initialColor;
	}
}
