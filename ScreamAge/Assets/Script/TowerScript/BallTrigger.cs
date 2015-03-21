using UnityEngine;
using System.Collections;

public class BallTrigger : MonoBehaviour {

    NavMeshAgent agent;
    Transform objective = null;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        Destroy(gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
        if (objective != null)
        {
            agent.SetDestination(objective.position);
        }
	}

    public void setObjective(Transform obj)
    {
        objective = obj;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
			AudioManager.instance.playBallHitSound();
			other.gameObject.GetComponent<EnemyBehaviour>().setStun(5);
            Destroy(gameObject);
        }
    }
}
