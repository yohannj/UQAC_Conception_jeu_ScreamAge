using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*<summary>
 * Game's central entity. Is a singleton.
</summary>*/

public class GameMaster : MonoBehaviour
{
    private static GameMaster _instance;

    public static GameMaster instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameMaster>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }


    public int currentLevel = 0;
    [SerializeField]
    private float enemySpawnDelay = 5.0f;
    [SerializeField]
    private int enemyTotal = 10;
    [SerializeField]
    private float percentageToKill = 0.5f;

    internal int enemiesToKillToWin; //percentageToKill*enemyTotal

    public GameObject[] spawnPoints;

    public GameObject guiGO;
    public GameObject endGameMenuGO;
    private ConstructionGUI gui;
    private EndRoundScreenScript endGameMenu;

    [SerializeField]
    private int dayTimer = 30; //seconds to place towers during day
    internal float dayCountDown; //time left to day. to display
    private string currentGamePhase = "Day"; //or "Night"
    private float roundTotalTime = 0;

	internal List<Transform> towers = new List<Transform>();

    private bool canSpawn = true;

    private int enemiesSpawned = 0; //number of enemies already spawned
    private int enemiesLeft; //number of enemies that are in game or not yet spawned
    private int enemiesInGame = 0; //number of enemies in game
    private bool round_is_success = false;
    private int nb_enemy_scared = 0;
    private float xp_won = 0;
    private float life_left = 3;

    private int lastTriedLevel = 1;

    private GameObject playerRef;
    private GameObject light;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;

        playerRef = GameObject.Find("Player");
        light = GameObject.Find("Sun/Moon");

        Time.timeScale = 1; //unpause game
        playerRef.SetActive(true);

        dayCountDown = dayTimer;

        enemiesLeft = enemyTotal;

        gui = guiGO.GetComponent<ConstructionGUI>();
        endGameMenu = endGameMenuGO.GetComponent<EndRoundScreenScript>();
    }


    void Update()
    {
		roundTotalTime += Time.deltaTime;

        //Lighting change 5 seconds before nighttime
        if ((int)roundTotalTime == (int)(dayTimer - 5))
        {
            light.GetComponent<SunMoon>().StartNight();
        }

        //Set game phase
        if (roundTotalTime < dayTimer)
        {
            currentGamePhase = "Day";
            dayCountDown = dayTimer - roundTotalTime;
        }
        else
        {
            if (currentGamePhase.Equals("Day"))
            { //Call once per round
                StartLevel(currentLevel);
            }
            currentGamePhase = "Night";
        }

        //Endgame
        if (enemiesLeft <= 0)
        {
            if (nb_enemy_scared >= enemiesToKillToWin)
            {
                round_is_success = true;
            }
            else
            {
                round_is_success = false;
            }

            enemiesInGame++;
            enemiesSpawned++;

            EndLevel();
        }
    }

    public void StartNextLevel(int lv) // lv in range[0,1]
    {
        guiGO.transform.GetChild(0).gameObject.SetActive(true);
        guiGO.transform.GetChild(1).gameObject.SetActive(false);

		foreach (Transform trans in towers){
			Destroy(trans.gameObject);
		}
		towers = new List<Transform>();

		//Reset all the shits
		dayCountDown = dayTimer;
		light.GetComponent<SunMoon>().reset();
        roundTotalTime = 0;
        enemiesSpawned = 0;
        nb_enemy_scared = 0;
        enemiesInGame = 0;
        xp_won = 0;
		enemiesLeft = enemyTotal;

		currentLevel += lv;
		Time.timeScale = 1; //unpause game
		playerRef.SetActive(true);
    }

    //Initialize number of enemies to spawn
    private void StartLevel(int currentLevel)
    {
        Time.timeScale = 1; //unpause game
        playerRef.SetActive(true);

        InvokeRepeating("SpawnEnemy", 0, enemySpawnDelay);

        if (currentLevel != 0)
            enemyTotal = Mathf.RoundToInt(enemyTotal + enemyTotal * 0.4f); 
        enemiesLeft = enemyTotal;

        enemiesToKillToWin = (int)(percentageToKill * enemyTotal);

		if(enemySpawnDelay > 1 && currentLevel != lastTriedLevel)
        {
            enemySpawnDelay = enemySpawnDelay * 0.9f;
            lastTriedLevel = currentLevel;
        }	
    }

    //Show endgame, pause game
    private void EndLevel()
    {
        Time.timeScale = 0; //pause game
        playerRef.SetActive(false);

        endGameMenu.Display();
    }

    //Spawn enemy at random spawn point
    private void SpawnEnemy()
    {
        if (enemiesSpawned < enemyTotal)
        {
            int selected = Random.Range(0, spawnPoints.Length);
            spawnPoints[selected].GetComponent<SpawnPoint>().spawn();

            enemiesInGame++;
            enemiesSpawned++;
        }
        else
        {
            CancelInvoke();
        }
    }

    //Enemy got through
    public void safeEnemy()
    {
        enemiesInGame--;
        enemiesLeft--;
    }

    //Scared an enemy successfully
    public void killedEnemy()
    {
        enemiesInGame--;
        enemiesLeft--;
        nb_enemy_scared++;

        xp_won += 1;
        playerRef.GetComponent<CharacterXP>().addXP(1);
    }


    public bool isRoundSuccess()
    {
        return round_is_success;
    }

    public int getNbEnemyScared()
    {
        return nb_enemy_scared;
    }

    public int getNbTotEnemy()
    {
        return enemyTotal;
    }

    public int getNbEnemyLeftToSpawn() 
    {
        return enemiesLeft;    
    }

    public float getNbXPWon()
    {
        return xp_won;
    }

    public float getLifeLeft()
    {
        return life_left;
    }

    public void OnGUI()
    {
        if (enemiesLeft > 0 || currentGamePhase.Equals("Day"))
        {
            GUIStyle gs = new GUIStyle(GUI.skin.box);
            gs.alignment = TextAnchor.MiddleCenter;
            gs.richText = true;
            string timerText = "<size=22>Level " + (currentLevel + 1) + "</size>\n\n";
            if (currentGamePhase.Equals("Day"))
            {
                timerText += "Night in: " + dayCountDown.ToString("0");
            }
            else
            {
                timerText += "Scared: " + nb_enemy_scared + "/" + enemyTotal + "\nLeft to spawn: " + (enemyTotal - enemiesSpawned);
            }
            GUI.Box(new Rect(10, 10, 115, 90), timerText, gs);
        }
    }
}
