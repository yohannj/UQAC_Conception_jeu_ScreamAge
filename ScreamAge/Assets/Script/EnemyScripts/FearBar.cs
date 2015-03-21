using UnityEngine;
using System.Collections;

public class FearBar : MonoBehaviour
{
    private Texture2D background;
    private Texture2D foreground;

    private float fear = 0.0f;
    private float maxFear = 0.0f;

    public float width = 80;
    public float height = 8;

    void Start()
    {
        //Initialisation
        fear = GetComponent<EnemyBehaviour>().getFear();
        maxFear = GetComponent<EnemyBehaviour>().getMaxFear();

        background = new Texture2D(1, 1, TextureFormat.RGB24, false);
        foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);

        background.SetPixel(0, 0, Color.black);
        foreground.SetPixel(0, 0, Color.yellow);

        background.Apply();
        foreground.Apply();
    }

    void Update()
    {
        fear = GetComponent<EnemyBehaviour>().getFear();
        fear += Input.GetAxisRaw("Horizontal");
        if (fear < 0.0f) fear = 0.0f;
        if (fear > maxFear) fear = maxFear;
    }

    void OnGUI()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);// gets screen position.
        screenPosition.y = Screen.height - (screenPosition.y + 1);// inverts y

        Rect box = new Rect(screenPosition.x - width*0.5f, screenPosition.y - 35, width, height);

        GUI.DrawTexture(new Rect(box.x, box.y, box.width, box.height), background, ScaleMode.StretchToFill);
        GUI.DrawTexture(new Rect(box.x, box.y, box.width * fear / maxFear, box.height), foreground, ScaleMode.StretchToFill);
    }

}
