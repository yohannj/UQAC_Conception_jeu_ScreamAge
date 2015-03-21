using UnityEngine;
using System.Collections;

public class StartMenuController : MonoBehaviour
{

    bool VDPadInUse = false;
    bool HDPadInUse = false;
    bool newCharGUI = false;
    bool controlGUI = false;
    StartMenu sm;

    // Use this for initialization
    void Start()
    {
        sm = transform.GetComponent<StartMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            if (!sm.getInSubMenu())
            {
				AudioManager.instance.playMenuSelectSound();
				switch (transform.GetComponent<StartMenu>().getCurrentPos())
                {
                    case 0:
                        //New game
                        sm.showNewGame();
                        newCharGUI = true;
                        break;
                    case 1:
                        //Show gui controls
                        sm.showControls();
                        controlGUI = true;
                        break;
                    case 2:
                        //Quit
                        Application.Quit();
                        break;
                }
            }
            else
            {
                switch (transform.GetComponent<StartMenu>().getCurrentPosSubMenu())
                {
                    case 0:
                        //Launch new game with first character
                        GameProperties.instance.setCharacter(0);
                        Application.LoadLevel(1);
                        break;
                    case 1:
                        //Launch new game with second character
                        GameProperties.instance.setCharacter(1);
                        Application.LoadLevel(1);
                        break;
                }
            }
            
        }
        if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.Escape))
        {
			AudioManager.instance.playMenuSelectSound();
			sm.hideNewGame();
            sm.hideControls();
            controlGUI = false;
            newCharGUI = false;
        }

        if (Input.GetAxis("VDPad") < 0)
        {
            if (!VDPadInUse && !controlGUI && !newCharGUI)
            {
				AudioManager.instance.playMenuShiftSound();
				sm.changePos(1);
                VDPadInUse = true;
            }
        }
        if (Input.GetAxis("VDPad") > 0)
        {
            if (!VDPadInUse && !controlGUI && !newCharGUI)
            {
				AudioManager.instance.playMenuShiftSound();
				sm.changePos(-1);
                VDPadInUse = true;
            }
        }
        if (Input.GetAxis("VDPad") == 0)
        {
            VDPadInUse = false;
        }

        if (Input.GetAxis("HDPad") != 0)
        {
            if (!HDPadInUse && newCharGUI)
            {
				AudioManager.instance.playMenuShiftSound();
				sm.changePosSubMenu();
                HDPadInUse = true;
            }
        }
        if (Input.GetAxis("HDPad") == 0)
        {
            HDPadInUse = false;
        }
    }
}
