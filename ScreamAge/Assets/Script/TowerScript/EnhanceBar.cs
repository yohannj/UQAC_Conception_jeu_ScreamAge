using UnityEngine;
using System.Collections;

public class EnhanceBar : MonoBehaviour {

    private Texture2D background;
    private Texture2D foreground;

    private float enhanceTime = 0.0f;
    private float currentEnhanceTime = 0.0f;
    private float lastTickedTime = 0.0f;

    private bool isCompleted = true;
    private float percentDone = 0;

    public float width = 50;
    public float height = 6;

    void Start()
    {
        //Initialisation
        enhanceTime = gameObject.GetComponent<Towers>().getEnhanceTime();
        currentEnhanceTime = gameObject.GetComponent<Towers>().getCurrentEnhanceTime();

        background = new Texture2D(1, 1, TextureFormat.RGB24, false);
        foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);

        background.SetPixel(0, 0, Color.black);
        foreground.SetPixel(0, 0, Color.cyan);

        background.Apply();
        foreground.Apply();
    }

    void Update()
    {
        if (!isCompleted)
        {
            percentDone = gameObject.GetComponent<Towers>().getEnhancePercent();
            isCompleted = percentDone >= 1;
        }

        if (GameObject.Find("Player"))
        {
            if (!isCompleted && percentDone > 0)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
            }
            else
            {
                GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
            }
        }

        if(gameObject.GetComponent<Towers>().getIsEnhancing())
        {
            isCompleted = false;
        }
    }

    void OnGUI()
    {
        if (!isCompleted && percentDone > 0)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);// gets screen position.
            screenPosition.y = Screen.height - (screenPosition.y + 1);// inverts y

            Rect box = new Rect(screenPosition.x - 20, screenPosition.y - 35, width, height);

            GUI.DrawTexture(new Rect(box.x, box.y, box.width, box.height), background, ScaleMode.StretchToFill);
            GUI.DrawTexture(new Rect(box.x, box.y, box.width * percentDone, box.height), foreground, ScaleMode.StretchToFill);
        }
    }
}
