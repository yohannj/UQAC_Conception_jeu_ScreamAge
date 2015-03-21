using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConstructionGUI : MonoBehaviour {

	public Sprite[] normal;
	public Sprite[] selected; 
	private int currentPos;
	private SpriteRenderer[] sprites;

	// Use this for initialization
	void Start () {
		currentPos = 0;
		changeGUI (0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeGUI(int towerInUse){
		Vector3 pos = transform.GetChild (0).GetChild (0).GetComponent<RectTransform> ().anchoredPosition;
		Vector3 posT = transform.GetChild (0).GetChild (10).GetComponent<RectTransform> ().anchoredPosition;
		if (towerInUse == 0) {
			transform.GetChild(0).GetChild(0).GetComponent<RectTransform> ().anchoredPosition = new Vector3(-70, pos.y, pos.z);
			transform.GetChild (0).GetChild (10).GetComponent<RectTransform> ().anchoredPosition = new Vector3(-70, posT.y, posT.x);
		} else if (towerInUse == 1) {
			transform.GetChild(0).GetChild(0).GetComponent<RectTransform> ().anchoredPosition = new Vector3(0, pos.y, pos.z);
			transform.GetChild (0).GetChild (10).GetComponent<RectTransform> ().anchoredPosition = new Vector3(0, posT.y, posT.x);
		} else {
			transform.GetChild(0).GetChild(0).GetComponent<RectTransform> ().anchoredPosition = new Vector3(70, pos.y, pos.z);
			transform.GetChild (0).GetChild (10).GetComponent<RectTransform> ().anchoredPosition = new Vector3(70, posT.y, posT.x);
		}

        for (int i = 1; i < 4; i++)
        {
            if (i-1 == towerInUse)
            {
                transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = selected[i-1];
            }
            else
            {
                transform.GetChild(0).GetChild(i).GetComponent<Image>().sprite = normal[i-1];
            }
        }
	}
}
