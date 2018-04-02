using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlanetManager
{
    #region singleton
    private static EnemyPlanetManager instance;

    private EnemyPlanetManager() { }

    public static EnemyPlanetManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EnemyPlanetManager();

            return instance;
        }
    }
    #endregion singleton

    public Transform EnemyPlanets;
    public Dictionary<Transform, EnemyPlanet> listeEnemyPlanets;


    public void InitEnemyPlanetManager(Transform _enemyPlanets)
    {
        EnemyPlanets = _enemyPlanets;

        listeEnemyPlanets = new Dictionary<Transform, EnemyPlanet>();
        foreach (Transform enemyPlanet in EnemyPlanets)
        {
            if (enemyPlanet != null)
            {
                EnemyPlanet enemyPlanetComponemt = enemyPlanet.GetComponent<EnemyPlanet>();
                enemyPlanetComponemt.initEnemyPlanet();
                listeEnemyPlanets.Add(enemyPlanet, enemyPlanetComponemt);
            }
        }


    }

    public Dictionary<Transform, Particule> UpdateEnemyPlanetsManager(float dt)
    {
        Dictionary<Transform, Particule> listeAllParticule = new Dictionary<Transform, Particule>();

        foreach (KeyValuePair<Transform, EnemyPlanet> kv in listeEnemyPlanets)
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



}
