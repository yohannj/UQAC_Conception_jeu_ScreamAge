using UnityEngine;
using System.Collections;

public class EndZone : MonoBehaviour {

	private bool lightedUp = false;
	private float timer = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(lightedUp){
			timer -= Time.deltaTime;
			if(timer <= 0)
				lightedUp = false;
			GetComponent<Light>().enabled = true;
		}
		else{
			timer = 1.0f;
			GetComponent<Light>().enabled = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Enemy")){
			AudioManager.instance.playSafeSound();
			lightedUp = true;
			Destroy(other.gameObject);
			GameMaster.instance.safeEnemy();
		}
	}
}
