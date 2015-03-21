using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    public float base_movement_speed = 100;
    public RatioCurve buildingSpeedRatio;
    public float base_building_speed = 1;
    public float base_enhancement_and_reload_speed = 1;
    float movement_speed;
    float building_speed;
    float enhancement_and_reload_speed;

    float movement_speed_stat = 0;
    public float building_speed_stat = 0;
    float enhancement_and_reload_speed_stat = 0;

    public void Start()
    {
        movement_speed = base_movement_speed;
        building_speed = base_building_speed;
        enhancement_and_reload_speed = base_enhancement_and_reload_speed;

        switch (GameProperties.instance.getCharacter())
        {
            case 0:
                addMovementSpeedStat();
                break;
            case 1:
                addBuildingSpeedStats();
                break;
        }
    }

    public void addMovementSpeedStat()
    {
        ++movement_speed_stat;
        movement_speed = base_movement_speed * (1 + (movement_speed_stat / 10));
    }

    public void addBuildingSpeedStats()
    {
        ++building_speed_stat;
        building_speed = base_building_speed * buildingSpeedRatio.ratioCurve.Evaluate(building_speed_stat / 100.0f);
    }

    public void addEnhancementAndReloadSpeedStat()
    {
        ++enhancement_and_reload_speed_stat;
        enhancement_and_reload_speed = base_enhancement_and_reload_speed * (1 + enhancement_and_reload_speed_stat / 10);
    }

    public float getMovementSpeed()
    {
        return movement_speed;
    }

    public float getBuildingSpeed()
    {
        return building_speed;
    }

    public float getEnhancementAndReloadSpeed()
    {
        return enhancement_and_reload_speed;
    }

    public float getStatMovementSpeed()
    {
        return movement_speed_stat;
    }

    public float getStatBuildingSpeed()
    {
        return building_speed_stat;
    }

    public float getStatEnhancementAndReloadSpeed()
    {
        return enhancement_and_reload_speed_stat;
    }



}
