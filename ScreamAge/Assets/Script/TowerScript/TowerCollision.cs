using UnityEngine;
using System.Collections;

public class TowerCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter( Collider other)
	{
		//Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Enemy") {
			transform.parent.GetComponent<Towers>().addTarget(other.gameObject.transform);
		}
		/*if (other.gameObject.tag.Equals("Player")) {
			Debug.Log("Player In");
			float dist = Vector3.Distance(transform.position, other.transform.position);
			if(dist < 10.0f){
				Debug.Log("T'es dedant");
				other.gameObject.GetComponent<PlayerController>().getTriggerInfo(transform.parent.gameObject);
			}
		}*/
	}
	
	void OnTriggerExit(Collider other)
	{
        if (other.gameObject.tag == "Enemy")
        {
            transform.parent.GetComponent<Towers>().removeTarget(other.gameObject.transform);
        }
		/*if (other.gameObject.tag == "Player") {
			Debug.Log("Player Out");
			float dist = Vector3.Distance(transform.position, other.transform.position);
			if(dist >= 10.0f)
				other.gameObject.GetComponent<PlayerController>().getTriggerInfo(null);
		}*/
	}
}
