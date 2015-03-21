using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	private bool axis2InUse = false;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if(!transform.GetComponent<EndGameMenu>().isInSubMenu()){
				if(transform.GetComponent<EndGameMenu>().getCurPos() == 0){
					transform.GetComponent<EndGameMenu>().changeMenu();
				}else if(transform.GetComponent<EndGameMenu>().getCurPos() == 1){
					if(GameMaster.instance.isRoundSuccess())
						GameObject.Find("GameMaster").GetComponent<GameMaster>().StartNextLevel(1);
					else
						GameObject.Find("GameMaster").GetComponent<GameMaster>().StartNextLevel(0);
				}else{
					Application.Quit();
				}
			}else{
				if(transform.GetComponent<EndGameMenu>().getCurPos() == 0){
					if(player.GetComponent<CharacterXP>().getStatPointsToSpend() > 0){
						player.GetComponent<CharacterXP>().reducePointsToSpent();
						player.GetComponent<CharacterStats>().addMovementSpeedStat();
					}
				}else if(transform.GetComponent<EndGameMenu>().getCurPos() == 1){
					if(player.GetComponent<CharacterXP>().getStatPointsToSpend() > 0){
						player.GetComponent<CharacterXP>().reducePointsToSpent();
						player.GetComponent<CharacterStats>().addBuildingSpeedStats();
					}
				}else{
					if(player.GetComponent<CharacterXP>().getStatPointsToSpend() > 0){
						player.GetComponent<CharacterXP>().reducePointsToSpent();
						player.GetComponent<CharacterStats>().addEnhancementAndReloadSpeedStat();
					}
				}
			}
		}
        if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.Escape))
        {
			if(transform.GetComponent<EndGameMenu>().isInSubMenu())
				transform.GetComponent<EndGameMenu>().changeMenu();
		}

		if (Input.GetAxis("HDPad") < 0 || Input.GetKeyDown(KeyCode.A)) {
			if(!axis2InUse){
				transform.GetComponent<EndGameMenu>().changePos(-1);
				axis2InUse = true;
			}
		}
        if (Input.GetAxis("HDPad") > 0 || Input.GetKeyDown(KeyCode.D))
        {
			if(!axis2InUse){
				transform.GetComponent<EndGameMenu>().changePos(1);
				axis2InUse = true;
			}
		}
		if (Input.GetAxis ("HDPad") == 0) {
			axis2InUse = false;
		}
	}
}
