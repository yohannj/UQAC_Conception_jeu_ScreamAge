using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    private int currentPos;
    private int currentPosSubMenu;
    private bool inSubMenu = false;
    Transform controlsGUI;
    Transform newGameGUI;

    // Use this for initialization
    void Start()
    {
        controlsGUI = transform.GetChild(3);
        controlsGUI.gameObject.SetActive(false);
        newGameGUI = transform.GetChild(4);
        newGameGUI.gameObject.SetActive(false);
        currentPos = 0;
        changeSprite();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changePos(int side)
    {
        currentPos = (currentPos + side) % 3;
        currentPos = currentPos == -1 ? 2 : currentPos;
        changeSprite();
    }

    public int getCurrentPos()
    {
        return currentPos;
    }

    public void changePosSubMenu()
    {
        currentPosSubMenu = (currentPosSubMenu + 1) % 2;
        changeSpriteSubMenu();
    }

    public int getCurrentPosSubMenu()
    {
        return currentPosSubMenu;
    }

    public void changeSprite()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Equals("NewGame"))
            {
                if (currentPos == 0)
                {
					child.GetComponent<Image>().color = new Color(0, 0.7f, 0.1f);
                }
                else
                {
                    child.GetComponent<Image>().color = Color.white;
                }

            }
            else if (child.name.Equals("Controls"))
            {
                if (currentPos == 1)
                {
					child.GetComponent<Image>().color = new Color(0, 0.7f, 0.1f);
                }
                else
                {
                    child.GetComponent<Image>().color = Color.white;
                }
            }
            else if(child.name.Equals("Quit"))
            {
                if (currentPos == 2)
                {
					child.GetComponent<Image>().color = new Color(0, 0.7f, 0.1f);
                }
                else
                {
                    child.GetComponent<Image>().color = Color.white;
                }
            }
        }
    }

    public void changeSpriteSubMenu()
    {
        foreach (Transform child in transform.GetChild(4))
        {
            if (child.name.Equals("Runner"))
            {
                if (currentPosSubMenu == 0)
                {
					child.GetComponent<Image>().color = new Color(0.3f, 0.7f, 0.3f);
                }
                else
                {
                    child.GetComponent<Image>().color = Color.white;
                }

            }
            else if(child.name.Equals("Handyman"))
            {
                if (currentPosSubMenu == 1)
                {
					child.GetComponent<Image>().color = new Color(0.3f, 0.7f, 0.3f);
                }
                else
                {
                    child.GetComponent<Image>().color = Color.white;
                }
            }
        }
    }

    public void showControls()
    {
        controlsGUI.gameObject.SetActive(true);
    }

    public void hideControls()
    {
        controlsGUI.gameObject.SetActive(false);
    }

    public void showNewGame()
    {
        newGameGUI.gameObject.SetActive(true);
        inSubMenu = true;
        currentPosSubMenu = 0;
        changeSpriteSubMenu();
    }

    public void hideNewGame()
    {
        newGameGUI.gameObject.SetActive(false);
        inSubMenu = false;
    }

    public bool getInSubMenu()
    {
        return inSubMenu;
    }
}
