using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Particule : MonoBehaviour
{

    NavMeshAgent meshAgent;
    Transform destination;
    public float value = GV.PARTICULE_VALUE;
    public GV.PLANET_TYPE particuleType;

    //get the navmeshagent conponement 
    public void initParticule()
    {
        NavMeshAgent _meshAgent = gameObject.transform.GetComponent<NavMeshAgent>();
        if (_meshAgent != null)
        {
            Debug.LogError("Component NavMeshAgent not attached with the gameObject" + gameObject.name);
        }
        else
        {
            meshAgent = _meshAgent;
        }
    }


    //set la destination d'une particule 
    public void setParticuleDestination(Transform _destination)
    {
        destination = _destination;
        meshAgent.SetDestination(destination.position);
    }


    private void OnCollisionEnter(Collision collision)
    {
        GetDamageOrEnergy(collision.gameObject);
    }


    //function give damage if the particule and planet different type or give energy if the same type 
    public void GetDamageOrEnergy(GameObject objectInCollision)
    {
        if (objectInCollision.CompareTag(GV.PLAYER_PLANET_TAG))
        {
            PlayerPlanet playerPlanet = objectInCollision.GetComponent<PlayerPlanet>();
            if (particuleType == playerPlanet.type)
            {
                if (playerPlanet.currentHealth <= playerPlanet.capacity)
                {
                    playerPlanet.currentHealth += value;
                    GameObject.Destroy(this.gameObject);
                }
            }
            else
            {
                playerPlanet.currentHealth -= value;
                GameObject.Destroy(this.gameObject);
            }
            Debug.Log(playerPlanet.currentHealth);
        }
        else if (objectInCollision.CompareTag(GV.PLAYER_PLANET_TAG))
        {
            EnemyPlanet enemyPlanet = objectInCollision.GetComponent<EnemyPlanet>();
            if (particuleType == enemyPlanet.type)
            {
                if (enemyPlanet.currentHealth <= enemyPlanet.capacity)
                {
                    enemyPlanet.currentHealth += value;
                    GameObject.Destroy(this.gameObject);
                }
            }
            else
            {
                enemyPlanet.currentHealth -= value;
                GameObject.Destroy(this.gameObject);
            }

            Debug.Log(enemyPlanet.currentHealth);
        }
    }

}
