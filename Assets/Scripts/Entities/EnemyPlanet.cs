using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlanet : Planet
{

    public static float nbTotalParticuleEnemy;
    private float currentTime;
    private static Transform parentParticuleEnemy;


    public override void InitPlanet()
    {
        nbTotalParticuleEnemy = 0;
        currentTime = 0;
        type = GV.PLANET_TYPE.ENEMY;
        //lvl = 2;
        capacity = lvl * GV.PLANET_MAX_PARTICULE_PER_LEVEL;
        currentHealth = capacity;
        position = gameObject.transform;
        material = Resources.Load<Material>("Materials\\EnemyPlanet");
        size = lvl;

        //if the planet are static object they will not change when the game run
        gameObject.transform.localScale = new Vector3(size, size, size);
    }



    public override Dictionary<Transform, Particule> UpdateEnemyPlanet(float dt)
    {
        return GenerationParticule(dt);
    }


    public Dictionary<Transform, Particule> GenerationParticule(float dt)
    {
        currentTime += dt;
        if (currentTime >= (GV.PLANET_TIME_GENERATION_PARTICULE / lvl))
        {
            if (parentParticuleEnemy == null)
            {
                parentParticuleEnemy = new GameObject().transform;
                parentParticuleEnemy.name = "ParentParticuleEnemy";
                parentParticuleEnemy.tag = GV.PARENT_PARTICULE_ENNEMY_TAG;
            }
            GameObject particule = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\Entities\\ParticuleEnemy"));
            particule.transform.SetParent(parentParticuleEnemy);
            particule.transform.position = gameObject.transform.position + Random.onUnitSphere.normalized * 1.5f;
            Particule particuleComponent = particule.GetComponent<Particule>();
            if (particuleComponent == null)
            {
                Debug.LogError("Particule scripts non attached to particule enemy " + particule.name);
            }
            particuleComponent.particuleType = type;
            particuleComponent.initParticule();

            Debug.Log(" currentTime : " + currentTime);
            currentTime = 0;
            nbTotalParticuleEnemy++;
            Debug.Log(" ParentParticulePlayer : " + nbTotalParticuleEnemy);
            Dictionary<Transform, Particule> uneParticuleEnemy = new Dictionary<Transform, Particule>();
            uneParticuleEnemy.Add(particule.transform, particuleComponent);
            return uneParticuleEnemy;
        }
        return null;
    }


    //gerer la collision with a particule 
    public override void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag(GV.PLAYER_PARTICULE_TAG))
            {
                Particule particulePlayer = other.gameObject.GetComponent<Particule>();
                currentHealth -= particulePlayer.value;
                GameObject.Destroy(other.gameObject);

            }
            if (other.gameObject.CompareTag(GV.ENEMY_PARTICULE_TAG))
            {
                Particule particuleEnemy = other.gameObject.GetComponent<Particule>();
                if (currentHealth < capacity)
                {
                    currentHealth += particuleEnemy.value;
                    GameObject.Destroy(other.gameObject);
                }
            }

            if (currentHealth == 0)
            {
                Debug.Log("destroy this enemy planet and add a neural planet ");
                PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.NEUTRAL).AddPlanet(transform);
                PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.ENEMY).RemovePlanet(transform);
            }
        }
    }

}
