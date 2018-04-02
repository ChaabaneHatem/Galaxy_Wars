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


    private void OnTriggerEnter(Collider other)
    {
        GetDamageOrEnergy(other.gameObject);
        CollisionOtherParticule(other.gameObject);
    }


    //function give damage if the particule and planet different type or give energy if the same type 
    public void GetDamageOrEnergy(GameObject objectInCollision)
    {
        if (objectInCollision.CompareTag(GV.PLAYER_PLANET_TAG))
        {
            PlayerPlanet playerPlanet = objectInCollision.GetComponent<PlayerPlanet>();
            if (particuleType == playerPlanet.type)
            {
                if (playerPlanet.currentHealth < playerPlanet.capacity)
                {
                    playerPlanet.currentHealth += value;
                    GameObject.Destroy(this.gameObject);
                }
            }
            else
            {
                if (playerPlanet.currentHealth > 0)
                    playerPlanet.currentHealth -= value;
                GameObject.Destroy(this.gameObject);
            }
            Debug.Log("player planet heath" + playerPlanet.currentHealth);
        }
        else if (objectInCollision.CompareTag(GV.ENEMY_PLANET_TAG))
        {
            EnemyPlanet enemyPlanet = objectInCollision.GetComponent<EnemyPlanet>();
            if (particuleType == enemyPlanet.type)
            {
                if (enemyPlanet.currentHealth < enemyPlanet.capacity)
                {
                    enemyPlanet.currentHealth += value;
                    GameObject.Destroy(this.gameObject);
                }
            }
            else
            {
                if (enemyPlanet.currentHealth > 0)
                    enemyPlanet.currentHealth -= value;
                GameObject.Destroy(this.gameObject);
            }

            Debug.Log("enemy planet heath" + enemyPlanet.currentHealth);
        }
    }


    private void CollisionOtherParticule(GameObject otherParticule)
    {
        if (otherParticule.CompareTag(GV.PLAYER_PARTICULE_TAG) || otherParticule.CompareTag(GV.ENEMY_PARTICULE_TAG))
        {
            Debug.Log("particule destroyed Named" + gameObject.name);
            PlayerPlanet.nbTotalParticulePlayer--;
            EnemyPlanet.nbTotalParticuleEnemy--;
            GameObject.Destroy(this.gameObject);
        }




    }


}
