using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	Transform tower = null;
	bool showConstructionUI;
	private bool axisInUse;
    private ushort towerInUse = 0;

    public Vector2 speed = new Vector2(1, 0);
    public GameObject mGui;
    public GameObject[] spawnableTowers;

	private GameMaster gm;
	
	void Start(){
		showConstructionUI = false;
		axisInUse = false;
		gm = GameMaster.instance;
	}
	
	void Update () {
		float inputX = Input.GetAxis("Horizontal");
		float inputZ = Input.GetAxis("Vertical");
		
		Vector3 movement = new Vector3(speed.x*inputX, 0, speed.y*inputZ);

        //Update movement speed with stats
        movement *= (gameObject.GetComponent<CharacterStats>().getMovementSpeed() / 100.0f);

        movement *= Time.deltaTime;
		transform.Translate(movement);

		if(!movement.Equals(new Vector3(0,0,0))){
	        Quaternion rot = new Quaternion();
	        rot.SetLookRotation(movement.normalized);
	        transform.GetChild(0).rotation = rot;
		}

		float dist = 9000.0f;
		if(gm.towers.Count > 0){
			foreach(Transform t in gm.towers){
				float tmp = Vector3.Distance(t.position, transform.position);
				if(tmp < dist){
					if(tower)
						tower.FindChild("Spotlight").GetComponent<Light>().enabled = false;
					dist = tmp;
					tower = t;
					if(dist < 5.0f)
						tower.FindChild("Spotlight").GetComponent<Light>().enabled = true;
				}
			}
		}else{
			tower = null;
		}
		if (Input.GetButtonDown("Fire1"))
        {
			if(tower != null){
				if(dist > 5.0f){
					//On construit
                    GameObject t = (GameObject)Instantiate(spawnableTowers[towerInUse], transform.position, spawnableTowers[towerInUse].transform.rotation);
					gm.towers.Add(t.transform);
					AudioManager.instance.playBuildSound();
				}else{
					//On construit pas
				}
			}else{
				//On construit
				AudioManager.instance.playBuildSound();
                GameObject t = (GameObject)Instantiate(spawnableTowers[towerInUse], transform.position, spawnableTowers[towerInUse].transform.rotation);
				gm.towers.Add(t.transform);

			}
		}
		if (Input.GetButtonDown ("Fire2")) {
            if(tower != null && dist < 5.0f)
            {
				AudioManager.instance.playReloadSound();
                tower.GetComponent<Towers>().reload();
            }
		}

		if (Input.GetButtonDown ("Fire3")) {
			if(tower != null && tower.GetComponent<Towers>().canUpgrade() && dist < 5.0f){
				AudioManager.instance.playEnhanceSound();
				tower.GetComponent<Towers>().upgrade();
			}
		}

		if (Input.GetAxisRaw ("LTrigger") != 0 || Input.GetKeyDown(KeyCode.Q)) {
			if(!axisInUse)
            {
                if(towerInUse == 0)
                {
                    towerInUse = 2;
                }
                else
                {
                    towerInUse--;
                }
				mGui.GetComponent<ConstructionGUI>().changeGUI(towerInUse);
				axisInUse = true;
			}
		}

		if (Input.GetAxisRaw ("RTrigger") != 0 || Input.GetKeyDown(KeyCode.E)) {
			if(!axisInUse)
            {
                if (towerInUse == 2)
                {
                    towerInUse = 0;
                }
                else
                {
                    towerInUse++;
                }
				mGui.GetComponent<ConstructionGUI>().changeGUI(towerInUse);
				axisInUse = true;
			}
		}

		if (Input.GetAxisRaw ("RTrigger") == 0 && Input.GetAxisRaw ("LTrigger") == 0) {
			axisInUse = false;
		}
	}
}