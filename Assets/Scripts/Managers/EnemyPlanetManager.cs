using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlanetManager : PlanetManager
{

    public Transform EnemyPlanets;


    public override void InitPlanet(Transform _enemyPlanets)
    {
        EnemyPlanets = _enemyPlanets;

        //listeEnemyPlanets = new Dictionary<Transform, EnemyPlanet>();
        listPlanetForEveryManager = new Dictionary<Transform, Planet>();
        foreach (Transform enemyPlanet in EnemyPlanets)
        {
            if (enemyPlanet != null)
            {
                EnemyPlanet enemyPlanetComponemt = enemyPlanet.GetComponent<EnemyPlanet>();
                enemyPlanetComponemt.InitPlanet();
                //listeEnemyPlanets.Add(enemyPlanet, enemyPlanetComponemt);
                listPlanetForEveryManager.Add(enemyPlanet, enemyPlanetComponemt);
            }
        }


    }

    public override Dictionary<Transform, Particule> UpdateEnemyPlanetsManager(float dt)
    {
        Dictionary<Transform, Particule> listeAllParticule = new Dictionary<Transform, Particule>();

        foreach (KeyValuePair<Transform, Planet> kv in listPlanetForEveryManager)
        {
            if (kv.Key != null)
            {
                Dictionary<Transform, Particule> listOfPartcileEnemyOfOneEnemyPlanet = kv.Value.UpdateEnemyPlanet(dt);
                if (listOfPartcileEnemyOfOneEnemyPlanet != null)
                {
                    foreach (KeyValuePair<Transform, Particule> kv1 in listOfPartcileEnemyOfOneEnemyPlanet)
                    {
                        listeAllParticule.Add(kv1.Key, kv1.Value);
                    }
                }
            }
        }

        if (listeAllParticule != null)
        {
            return listeAllParticule;
        }

        return null;
    }

    //remove enemy planet
    public override void RemovePlanet(Transform planetToRemove)
    {
        if (listPlanetForEveryManager.ContainsKey(planetToRemove))
        {
            listPlanetForEveryManager.Remove(planetToRemove);
            GameObject.Destroy(planetToRemove.gameObject);
        }
    }



    //add new enemy planet to the list 
    public override void AddPlanet(Transform positioToAddThePlanet, float maxLevel)
    {
        GameObject enemyPlanet = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\Entities\\EnemyPlanet"));
        enemyPlanet.name = "EnemyPlanet";
        enemyPlanet.tag = GV.ENEMY_PLANET_TAG;
        enemyPlanet.layer = LayerMask.NameToLayer(GV.ENEMY_PLANET_TAG);
        enemyPlanet.transform.position = positioToAddThePlanet.position;
        enemyPlanet.transform.SetParent(GameObject.FindGameObjectWithTag(GV.PARENT_ENEMY_PLANET).transform);
        EnemyPlanet enemyPlanetComponent = enemyPlanet.GetComponent<EnemyPlanet>();
        if (enemyPlanetComponent == null)
        {
            Debug.LogError("Enemy planet component not attached to the new planet " + enemyPlanet.name);
        }
        else
        {
            enemyPlanetComponent.InitPlanet();
            enemyPlanetComponent.maxLevel = maxLevel;
            listPlanetForEveryManager.Add(enemyPlanet.transform, enemyPlanetComponent);
        }
    }

}
