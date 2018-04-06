using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralPlanet : Planet
{

    public float nbParticulePlayer;
    public float nbParticuleEnemy;


    public override void InitPlanet()
    {
        nbParticulePlayer = 0;
        nbParticuleEnemy = 0;
        type = GV.PLANET_TYPE.NEUTRAL;
        lvl = 1;
        capacity = lvl * GV.PLANET_MAX_PARTICULE_PER_LEVEL;
        currentHealth = 0;
        position = gameObject.transform;
        material = Resources.Load<Material>("Materials\\NeutralPlanet");
        size = lvl;

        //if the planet are static object they will not change when the game run
        gameObject.transform.localScale = new Vector3(size, size, size);
    }

    public override void UpdatePlanet(float dt)
    {
        //Debug.Log("Update neutral planet ");
    }


    //gerer la collision with strange particule
    public override void OnTriggerEnter(Collider other)
    {
        if (currentHealth < capacity)
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
                        //Debug.Log("remove the neutral plant and add a player planet in the some poition");
                        PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.PLAYER).AddPlanet(transform, maxLevel);
                        PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.NEUTRAL).RemovePlanet(transform);
                    }
                }
                if (nbParticulePlayer < nbParticuleEnemy)
                {
                    currentHealth = nbParticuleEnemy - nbParticulePlayer;
                    if (currentHealth == capacity)
                    {
                        //Debug.Log("remove the neutral planet and add a enemy planet in the some position");
                        PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.ENEMY).AddPlanet(transform, maxLevel);
                        PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.NEUTRAL).RemovePlanet(transform);
                    }
                }
                if (nbParticulePlayer == nbParticuleEnemy)
                {
                    currentHealth = 0;
                }
            }
        }
    }


    private float GetMax(float nb1, float nb2)
    {
        return (nb1 > nb2 ? nb1 : nb2);
    }


}
