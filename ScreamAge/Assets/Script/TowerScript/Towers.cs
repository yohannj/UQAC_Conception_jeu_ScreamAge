using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Towers : MonoBehaviour
{

    [SerializeField]
    protected float base_building_time;

    [SerializeField]
    protected float base_reload_time;

    [SerializeField]
    protected float base_enhance_time;

    [SerializeField]
    protected float buildingMultiplicator;

    protected float currentBuildingTime = 0;
    protected float currentReloadTime = 0;
    protected float currentEnhanceTime = 0;

    protected bool isBuilt = false;
    protected bool isReloading = false;
    protected bool isEnhancing = false;
    protected bool canReload = true;

    protected float maxLevel;
    protected float max_radius;
    protected float min_reload_time;
    protected float base_radius;

    protected float level;
    protected float reload_time;
    protected float next_attack_time;
    protected HashSet<Transform> targets;

    [SerializeField]
    protected float fear_damage;
    protected SphereCollider sc;

    protected GameObject player;

    protected abstract void Start();

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!isBuilt)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            currentBuildingTime += Time.deltaTime;
            if (getBuildPercent() >= 1)
            {
                isBuilt = true;
            }
        }

        if (isReloading && canReload)
        {
            currentReloadTime += Time.deltaTime;
            if (getReloadPercent() >= 1)
            {
                currentReloadTime = 0;
                isReloading = false;
            }
        }

        if (isEnhancing)
        {
            currentEnhanceTime += Time.deltaTime;
            if (getEnhancePercent() >= 1)
            {
                currentEnhanceTime = 0;
                isEnhancing = false;
            }
        }

        if (isBuilt && !isReloading && !isEnhancing)
        {
            removeTargetThatEnded();
            if (targets.Count > 0 && Time.time >= next_attack_time)
            {
                Shoot();
            }
        }
    }

    protected abstract void Shoot();

    public virtual void addTarget(Transform target)
    {
        targets.Add(target);
    }

    public virtual void removeTarget(Transform target)
    {
        targets.Remove(target);
    }

    protected virtual void removeTargetThatEnded()
    {
        targets.RemoveWhere(t => t == null);
    }

    public abstract void reload();

    public abstract void upgrade();

    public virtual float getLevel()
    {
        return level;
    }

    public virtual float getReloadTime()
    {
        return reload_time;
    }

    public virtual float getCurrentReloadTime()
    {
        return currentReloadTime;
    }

    public virtual float getFearDamage()
    {
        return fear_damage;
    }

    public virtual bool canUpgrade()
    {
        return (level < maxLevel);
    }

    public virtual float getBuildingTime()
    {
        return base_building_time;
    }

    public virtual float getEnhanceTime()
    {
        return base_enhance_time;
    }

    public virtual bool getIsEnhancing()
    {
        return isEnhancing;
    }

    public virtual float getCurrentBuildingTime()
    {
        return currentBuildingTime;
    }

    public virtual float getCurrentEnhanceTime()
    {
        return currentEnhanceTime;
    }

    public virtual float getRadius()
    {
        return sc.radius;
    }

    public virtual float getBuildPercent()
    {
        return currentBuildingTime * player.GetComponent<CharacterStats>().getBuildingSpeed() / base_building_time * buildingMultiplicator;
    }

    public virtual float getReloadPercent()
    {
        return currentReloadTime * player.GetComponent<CharacterStats>().getEnhancementAndReloadSpeed() * 9 / base_reload_time;
    }

    public virtual float getEnhancePercent()
    {
        return currentEnhanceTime * player.GetComponent<CharacterStats>().getEnhancementAndReloadSpeed() / (Mathf.Pow(1.1f, level - 1) * base_enhance_time);
    }
}
