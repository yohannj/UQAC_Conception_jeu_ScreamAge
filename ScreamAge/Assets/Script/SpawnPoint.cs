using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

    private int spawnRandomizer = 0;

    public GameObject[] enemyToSpawn;

    private int level = 1;
    private int difficulty;
    private int enemy;
    private int minEnemy;
    private int enemyLeft;
    private int totalEnemy;
    private float ratio;

	// Use this for initialization
	void Start () {
         
	}
	
	// Update is called once per frame
	void Update () {
  
	}

	public void spawn()
    {
        level = GameMaster.instance.currentLevel;
        enemyLeft = GameMaster.instance.getNbEnemyLeftToSpawn();
        totalEnemy = GameMaster.instance.getNbTotEnemy();

        difficulty = Random.Range(0, level);

        ratio = enemyLeft / totalEnemy;

        switch(difficulty)
        {
            case 0:
                enemy = 0;
                break;
            case 1:
                enemy = 1;
                break;
            case 2:
                enemy = 2;
                break;
            case 3:
                enemy = 2;
                break;
            case 4:
                enemy = 3;
                break;
            default:
                enemy = enemyToSpawn.Length-1;
                break;
        }

        if(ratio <= 0.1f)
        {
            minEnemy = enemy;
        }
        else if(ratio <= 0.3f && enemy >= 1)
        {
            minEnemy = enemy-1;
        }
        else if(ratio <= 0.55f && enemy >= 2)
        {
            minEnemy = enemy-2;
        }
        else if(ratio <= 0.75f && enemy >= 3)
        {
            minEnemy = enemy-3;
        }

        spawnRandomizer = Random.Range(minEnemy, enemy);
        Instantiate(enemyToSpawn[spawnRandomizer], transform.position, transform.rotation);
	}

    void OnTriggerEnter(Collider Other)
    {
        if(Other.tag == "DeadEnemy")
        {
            GameMaster.instance.killedEnemy();
            Destroy(Other.gameObject);
        }

    }
}
