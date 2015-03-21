using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    private float fear = 0.0f;
    private NavMeshAgent agent;
	private bool isScared = false;
    private float stun_end_time;
	private GameObject dust;
	[SerializeField] private float timeUntilSelfDestruct = 15.0f;

    public float maxFear = 100.0f;
    public float movementSpeed = 2.5f;
    public Vector3 targetPoint = new Vector3(20, 20, 20); //Determine it through an empty GameObject corresponding to the exit of the map
 
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
		dust = transform.FindChild("Dust").gameObject;
		dust.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= stun_end_time)
        {
            agent.speed = movementSpeed;
            agent.SetDestination(targetPoint);
        }
        else
        {
            agent.speed = 0;
        }

        if(fear >= maxFear)
        {
            gameObject.tag = "DeadEnemy";
			timeUntilSelfDestruct -= timeUntilSelfDestruct * Time.deltaTime;

			if(timeUntilSelfDestruct <= 0)
				Destroy(gameObject);
        }
    }

    public void addFearDamage(float fear_to_add)
    {
        fear += fear_to_add;
        checkHorrified();
    }

    public float getFear()
    {
        return fear;
    }

    public float getMaxFear()
    {
        return maxFear;
    }

    private void checkHorrified()
    {
        if (!isScared && fear >= maxFear)
        {
			stun_end_time = 0;
			launchHorrifiedAnimation();
            scream();
			isScared = true;
        }
    }

    private void launchHorrifiedAnimation()
    {
        int nbOfSpawn = GameMaster.instance.spawnPoints.Length;
        int spawn = Random.Range(0, nbOfSpawn); //The enemy will run randomly to a spawn point
        GameObject spawnPoint = GameMaster.instance.spawnPoints[spawn];
        targetPoint = spawnPoint.transform.position;
		agent.speed = agent.speed*2;
		dust.SetActive(true);
    }

    private void scream()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void setStun(float duration)
    {
        stun_end_time = Time.time + duration;
    }
}
