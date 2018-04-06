using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperNovaPlanet : Planet
{


    public float nbParticulePlayer;
    public float nbParticuleEnemy;

    //boolean for the explosion of this planet when it taken
    bool explosion;

    //type of the particule inside
    public GV.PLANET_TYPE particuleTypeInside;

    public override void InitPlanet()
    {
        explosion = false;
        type = GV.PLANET_TYPE.SUPERNOVA;
        nbParticulePlayer = 0;
        nbParticuleEnemy = 0;
        //lvl = 1;
        capacity = lvl * GV.SUPERNOVA_MAX_PARTICULE_PER_LEVEL;
        currentHealth = 0;
        position = gameObject.transform;
        material = Resources.Load<Material>("Materials\\SuperNovaPlanet");
        size = lvl;

        //if the planet are static object they will not change when the game run
        gameObject.transform.localScale = new Vector3(size, size, size);

    }


    public override void UpdatePlanet(float dt)
    {
        if (explosion)
        {
            MakeExplosion();
        }
    }


    private void MakeExplosion()
    {
        if (particuleTypeInside == GV.PLANET_TYPE.PLAYER)
        {
            for (int i = 0; i < capacity * GV.SUPERNOVA_COEFFICIENT; i++)
            {
                GameObject particule = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\Entities\\ParticulePlanet"));
                particule.transform.SetParent(PlayerPlanet.parentParticulePlayer);
                particule.transform.position = gameObject.transform.position + Random.onUnitSphere;
                Particule particuleComponent = particule.GetComponent<Particule>();
                if (particuleComponent == null)
                {
                    Debug.LogError("Particule scripts non attached to particule enemy " + particule.name);
                }
                particuleComponent.particuleType = particuleTypeInside;
                //we must call this function when particule was created 
                particuleComponent.initParticule();
                PlayerPlanet.nbTotalParticulePlayer++;
            }
        }
        //trun off the explosion
        explosion = false;
    }


    public override void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag(GV.PLAYER_PARTICULE_TAG))
            {
                Particule particulePlayer = other.gameObject.GetComponent<Particule>();
                nbParticulePlayer += particulePlayer.value;
                GameObject.Destroy(other.gameObject);
            }
            if (other.gameObject.CompareTag(GV.ENEMY_PARTICULE_TAG))
            {
                Particule particuleEnemy = other.gameObject.GetComponent<Particule>();
                nbParticuleEnemy += particuleEnemy.value;
                GameObject.Destroy(other.gameObject);
            }
            if (nbParticulePlayer > nbParticuleEnemy)
            {
                currentHealth = nbParticulePlayer - nbParticuleEnemy;
                if (currentHealth == capacity)
                {
                    //Debug.Log("remove the SuperNova planet and explosion of playerParticule");
                    explosion = true;
                    particuleTypeInside = GV.PLANET_TYPE.PLAYER;
                    //remove the supernova after i update this supernova
                    PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.SUPERNOVA).UpdatePlanet(1);
                    PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.SUPERNOVA).RemovePlanet(transform);
                }
            }
            if (nbParticulePlayer < nbParticuleEnemy)
            {
                currentHealth = nbParticuleEnemy - nbParticulePlayer;
                if (currentHealth == capacity)
                {
                    //Debug.Log("remove the supernova planet and explosion of enemyParticule");
                    explosion = true;
                    particuleTypeInside = GV.PLANET_TYPE.ENEMY;
                    //remove the supernova after i update this supernova
                    PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.SUPERNOVA).UpdatePlanet(1);
                    PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.SUPERNOVA).RemovePlanet(transform);
                }
            }
            if (nbParticulePlayer == nbParticuleEnemy)
            {
                currentHealth = 0;
            }
        }
    }


}
