using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallLauncher : Towers
{

    public Transform ball_prefab;
    public float ball_speed = 100;
    float max_nb_ball_load;
    float nb_ball_load;

    private float selectTargetRadius;
	private Color initialColor;

    // Use this for initialization
    protected override void Start()
    {
        player = GameObject.Find("Player");

        buildingMultiplicator = 1.0f;
        maxLevel = 10;
        max_radius = 25;
        min_reload_time = 1;
        base_radius = 7;
        base_reload_time = 5;
        base_enhance_time = 2;
        targets = new HashSet<Transform>();
        fear_damage = 0;

        level = 1;
        sc = transform.GetChild(0).GetComponent<SphereCollider>();
        selectTargetRadius = base_radius;
        sc.radius = 200;
        reload_time = base_reload_time;
        max_nb_ball_load = 1;
        nb_ball_load = max_nb_ball_load;
        next_attack_time = Time.time;

		initialColor = renderer.material.color;
    }

    protected override void Shoot()
    {
        Transform target_to_follow = getTargetToFollow();
        if (target_to_follow != null && nb_ball_load > 0)
        {
            Transform hit_target = getTargetToHit();
            if (hit_target != null)
            {
                Vector3 direction = (hit_target.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(direction);

                AudioManager.instance.playShootSound();
                Transform tmpBall = (Transform)Instantiate(ball_prefab, transform.position, Quaternion.identity);
                tmpBall.GetComponent<BallTrigger>().setObjective(hit_target);
                --nb_ball_load;
                next_attack_time = Time.time + reload_time;

				renderer.material.SetColor("_Color", new Color(0.2f, 0.2f, 0.3f));
            }
            else
            {
                Vector3 direction = (target_to_follow.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }

    }

    private Transform getTargetToHit()
    {
        Transform res = null;
        float pathSize = float.MaxValue;
        foreach (Transform t in targets)
        {
            if (t.GetComponent<NavMeshAgent>().remainingDistance < pathSize && Vector3.Distance(transform.position, t.position) < selectTargetRadius)
            {
                pathSize = t.GetComponent<NavMeshAgent>().remainingDistance;
                res = t;
            }
        }
        return res;
    }

    private Transform getTargetToFollow()
    {
        Transform res = null;
        float pathSize = float.MaxValue;
        foreach (Transform t in targets)
        {
            if (t.GetComponent<NavMeshAgent>().remainingDistance < pathSize)
            {
                pathSize = t.GetComponent<NavMeshAgent>().remainingDistance;
                res = t;
            }
        }
        return res;
    }

    public override void reload()
    {
        if(nb_ball_load == 0)
        {
			renderer.material.SetColor("_Color", initialColor);
			isReloading = true;
            nb_ball_load = max_nb_ball_load;
            //next_attack_time = Time.time + reload_time;
        }
    }

    public override void upgrade()
    {
        ++level;
        isEnhancing = true;
        float ratio = (level - 1) / (maxLevel - 1);
        selectTargetRadius = base_radius + (max_radius - base_radius) * ratio;
        reload_time = base_reload_time + (min_reload_time - base_reload_time) * ratio;
    }


}
