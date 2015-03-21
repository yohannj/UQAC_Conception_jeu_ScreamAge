using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour {

	private int currentPos;
	private int currentPosSubMenu;
	private bool inSubMenu = false;
	public Sprite[] normal;
	public Sprite[] selected;

	// Use this for initialization
	void Start () {
		currentPos = 0;
		currentPosSubMenu = 0;
		changeSprite ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getCurPos(){
		return currentPos;
	}

	public bool isInSubMenu(){
		return inSubMenu;
	}

	public void changePos(int side){
		if (inSubMenu) {
			currentPos += side;
			if(currentPos > 2)
				currentPos = 0;
			else if(currentPos < 0)
				currentPos = 2;
		} else {
			currentPos += side;
			if(currentPos > 2)
				currentPos = 0;
			else if(currentPos < 0)
				currentPos = 2;
		}
		changeSprite ();
	}

	public void changeMenu(){
		if (inSubMenu) {
			inSubMenu = false;
			currentPos = 0;
		} else {
			inSubMenu = true;
			currentPos = 0;
		}
		changeSprite ();
	}

	void changeSprite(){
		foreach (Transform child in transform) {
			if(child.name.Equals("upStats")){
				if(!inSubMenu && currentPos == 0)
					child.GetComponent<Image>().color = new Color(0, 0.7f, 0.1f);
				else
					child.GetComponent<Image>().color = Color.white;
			}else if(child.name.Equals("nextRound")){
				if(!inSubMenu && currentPos == 1)
					child.GetComponent<Image>().color = new Color(0, 0.7f, 0.1f);
				else
					child.GetComponent<Image>().color = Color.white;
			}else if(child.name.Equals("quit")){
				if(!inSubMenu && currentPos == 2)
					child.GetComponent<Image>().color = new Color(0, 0.7f, 0.1f);
				else
					child.GetComponent<Image>().color = Color.white;
			}if(child.name.Equals("mSpeed")){
				if(inSubMenu && currentPos == 0)
					child.GetComponent<Image>().color = new Color(0, 0.7f, 0.1f);
				else
					child.GetComponent<Image>().color = Color.white;
			}else if(child.name.Equals("bSpeed")){
				if(inSubMenu && currentPos == 1)
					child.GetComponent<Image>().color = new Color(0, 0.7f, 0.1f);
				else
					child.GetComponent<Image>().color = Color.white;
			}else if(child.name.Equals("eAndRSpeed")){
				if(inSubMenu && currentPos == 2)
					child.GetComponent<Image>().color = new Color(0, 0.7f, 0.1f);
				else
					child.GetComponent<Image>().color = Color.white;
			}
		}
	}
}
