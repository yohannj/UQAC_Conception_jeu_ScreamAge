using UnityEngine;
using System.Collections;

public class BuildingBar : MonoBehaviour {

    private Texture2D background;
    private Texture2D foreground;

    private float buildingTime = 0.0f;
    private float currentBuildingTime = 0.0f;
    private float lastTickedTime = 0.0f;

    private bool isCompleted = false;
    private float percentDone = 0;

    public float width = 50;
    public float height = 6;

    void Start()
    {
        //Initialisation
        buildingTime = gameObject.GetComponent<Towers>().getBuildingTime();
        currentBuildingTime = gameObject.GetComponent<Towers>().getCurrentBuildingTime();

        background = new Texture2D(1, 1, TextureFormat.RGB24, false);
        foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);

        background.SetPixel(0, 0, Color.black);
        foreground.SetPixel(0, 0, Color.gray);

        background.Apply();
        foreground.Apply();
    }

    void Update()
    {
        if (!isCompleted)
        {
            percentDone = gameObject.GetComponent<Towers>().getBuildPercent();
            isCompleted = percentDone >= 1;
        }

		if(GameObject.Find("Player")){
	        if (!isCompleted)
	        {
	            GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
	        }
	        else
	        {
	            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
	        }
		}
    }

    void OnGUI()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);// gets screen position.
        screenPosition.y = Screen.height - (screenPosition.y + 1);// inverts y
        if (!isCompleted)
        {
            Rect box = new Rect(screenPosition.x - 20, screenPosition.y - 35, width, height);

            GUI.DrawTexture(new Rect(box.x, box.y, box.width, box.height), background, ScaleMode.StretchToFill);
            GUI.DrawTexture(new Rect(box.x, box.y, box.width * percentDone, box.height), foreground, ScaleMode.StretchToFill);
        }

        Rect boxLevel = new Rect(screenPosition.x - 10, screenPosition.y + 20, 20, 20);

        GUI.Box(new Rect(boxLevel.x, boxLevel.y, boxLevel.width, boxLevel.height), ""+gameObject.GetComponent<Towers>().getLevel());
    }

}

