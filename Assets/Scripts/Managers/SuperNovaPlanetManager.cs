using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperNovaPlanetManager : PlanetManager
{

    public Transform superNovaPlanets;


    public override void InitPlanet(Transform _planetTransform)
    {
        superNovaPlanets = _planetTransform;

        listPlanetForEveryManager = new Dictionary<Transform, Planet>();
        foreach (Transform superNovaPlanet in superNovaPlanets)
        {
            if (superNovaPlanet != null)
            {
                SuperNovaPlanet superNovaPlanetComponemt = superNovaPlanet.GetComponent<SuperNovaPlanet>();
                superNovaPlanetComponemt.InitPlanet();
                listPlanetForEveryManager.Add(superNovaPlanet, superNovaPlanetComponemt);
            }
        }
    }


    public override void UpdatePlanet(float dt)
    {
        foreach (KeyValuePair<Transform, Planet> kv in listPlanetForEveryManager)
        {
            if (kv.Key != null)
            {
                kv.Value.UpdatePlanet(dt);
            }
        }
    }



    //remove a planet from the list and in the game 
    public override void RemovePlanet(Transform planetToRemove)
    {
        if (listPlanetForEveryManager.ContainsKey(planetToRemove))
        {
            listPlanetForEveryManager.Remove(planetToRemove);
            GameObject.Destroy(planetToRemove.gameObject);
        }
    }


    //add superNova i will do it later
    public override void AddPlanet(Transform _positioToAddThePlanet, float _maxLevel)
    {

    }

}
