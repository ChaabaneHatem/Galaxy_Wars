using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanet : Planet
{
    public static float nbTotalParticulePlayer;
    private float currentTime;
    public static Transform parentParticulePlayer;

    public override void InitPlanet()
    {
        nbTotalParticulePlayer = 0;
        currentTime = 0;
        type = GV.PLANET_TYPE.PLAYER;
        lvl = 1;
        capacity = lvl * GV.PLANET_MAX_PARTICULE_PER_LEVEL;
        currentHealth = capacity;
        position = gameObject.transform;
        material = Resources.Load<Material>("Materials\\PlayerPlanet");
        size = lvl;

        //if the planet are static object they will not change when the game run
        gameObject.transform.localScale = new Vector3(size, size, size);

        //Update the health Bar
        UpdateHealth(healthBarTransform, currentHealth, maxLevel * GV.PLANET_MAX_PARTICULE_PER_LEVEL, material);
    }

    public override void UpdatePlanet(float dt)
    {
        GenerationParticule(dt);
    }



    private void GenerationParticule(float dt)
    {
        currentTime += dt;

        if (currentTime >= (GV.PLANET_TIME_GENERATION_PARTICULE / lvl))
        {
            if (parentParticulePlayer == null)
            {
                parentParticulePlayer = new GameObject().transform;
                parentParticulePlayer.name = "ParentParticulePlayer";
                parentParticulePlayer.tag = GV.PARENT_PARTICULE_PLAYER_TAG;
            }

            GameObject particule = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\Entities\\ParticulePlanet"));
            particule.transform.SetParent(parentParticulePlayer);
            particule.transform.position = gameObject.transform.position + Random.onUnitSphere.normalized * 1.5f * gameObject.transform.localScale.magnitude / 2;
            Particule particuleComponent = particule.GetComponent<Particule>();
            if (particuleComponent == null)
            {
                Debug.LogError("Particule scripts non attached to particule enemy " + particule.name);
            }
            particuleComponent.particuleType = type;
            //we must call this function when particule was created 
            particuleComponent.initParticule();

            //Debug.Log(" currentTime : " + currentTime);
            currentTime = 0;
            nbTotalParticulePlayer++;
            //Debug.Log(" ParentParticulePlayer : " + nbTotalParticulePlayer);

        }
    }


    //gerrer la collision with a particule 
    public override void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag(GV.PLAYER_PARTICULE_TAG))
            {
                Particule particulePlayer = other.gameObject.GetComponent<Particule>();
                if (lvl <= maxLevel)
                {
                    float MaxLevelCapacity = maxLevel * GV.PLANET_MAX_PARTICULE_PER_LEVEL;
                    if (currentHealth < MaxLevelCapacity)
                    {
                        currentHealth += particulePlayer.value;
                        GameObject.Destroy(other.gameObject);
                        nbTotalParticulePlayer--;
                    }
                }
            }
            if (other.gameObject.CompareTag(GV.ENEMY_PARTICULE_TAG))
            {
                Particule particuleEnemy = other.gameObject.GetComponent<Particule>();
                currentHealth -= particuleEnemy.value;
                GameObject.Destroy(other.gameObject);
                EnemyPlanet.nbTotalParticuleEnemy--;
            }
            if (currentHealth == 0)
            {
                //Debug.Log("remove this planet player and add a neutral planet ");
                PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.NEUTRAL).AddPlanet(transform, maxLevel);
                PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.PLAYER).RemovePlanet(transform);
            }
            if (lvl < maxLevel)
            {
                float nextLevel = lvl + 1;
                float nextLevelCapacity = nextLevel * GV.PLANET_MAX_PARTICULE_PER_LEVEL;
                if (currentHealth == nextLevelCapacity)
                {
                    lvl++;
                    UpgradeLevel(lvl);
                }
            }

            //Update the health Bar
            UpdateHealth(healthBarTransform, currentHealth, maxLevel * GV.PLANET_MAX_PARTICULE_PER_LEVEL, material);

        }
    }




}
