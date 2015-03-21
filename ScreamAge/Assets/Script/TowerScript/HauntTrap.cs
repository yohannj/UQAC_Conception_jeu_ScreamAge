using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HauntTrap : Towers {

    float max_nb_ball_load;
    float nb_ball_load;

    private Vector3 initPosition;

    public float maxFearDamage = 300.0f;
    public float baseFearDamage = 100.0f;

    // Use this for initialization
    protected override void Start()
    {
        player = GameObject.Find("Player");

        buildingMultiplicator = 0.5f;
        maxLevel = 10;
        max_radius = 25;
        min_reload_time = 1;
        base_radius = 7;
        base_reload_time = 15;
        base_enhance_time = 2;
        targets = new HashSet<Transform>();
        fear_damage = 100.0f;

        level = 1;
        sc = transform.GetChild(0).GetComponent<SphereCollider>();
        sc.radius = base_radius;
        reload_time = base_reload_time;
        max_nb_ball_load = 1;
        nb_ball_load = max_nb_ball_load;
        next_attack_time = Time.time;

        initPosition = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y-1.1f, transform.position.z);
    }

    protected override void Shoot()
    {
        Transform target = getTarget();
        

        if(target != null)
        {
            if (nb_ball_load >= 1)
            {
				PlayScaryAnimation();
				AudioManager.instance.playHauntedTrapSound();
				target.GetComponent<EnemyBehaviour>().addFearDamage(fear_damage);
                nb_ball_load--;
				targets.Clear();
            }
        }
    }

    protected void PlayScaryAnimation()
    {
        transform.position = initPosition;
		transform.GetComponent<ParticleSystem> ().Play ();
    }

    private Transform getTarget()
    {
        Transform thisTarget = null;
        int cpt = 0;
        foreach (Transform t in targets)
        {
            if(cpt == 0)
            {
                thisTarget = t;
            }
            cpt++;
        }
        return thisTarget;
    }

    public override void reload()
    {
        if(nb_ball_load == 0)
        {
            isReloading = true;
            nb_ball_load = max_nb_ball_load;
            next_attack_time = Time.time;
            if (transform.position == initPosition)
                transform.position = new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z);
        }
    }

    public override void upgrade()
    {
        ++level;
        isEnhancing = true;
        float ratio = (level - 1) / (maxLevel - 1);
        fear_damage = baseFearDamage + (maxFearDamage - baseFearDamage )* ratio;
        currentEnhanceTime = 0;
    }
}
