using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager
{

    //dictionnaire of all planets
    public Dictionary<Transform, Planet> listPlanetForEveryManager;

    //function init of a planet 
    public virtual void InitPlanet(Transform planetTransform)
    {

    }


    //function update of a planet 
    public virtual void UpdatePlanet(float dt)
    {

    }

    //this function are specefic for the EnemyPlayerManger
    public virtual Dictionary<Transform, Particule> UpdateEnemyPlanetsManager(float dt)
    {
        return new Dictionary<Transform, Particule>();
    }


    //function of add and remove Planet
    //remove planet 
    public virtual void RemovePlanet(Transform planetToRemove)
    {

    }


    //add planet 
    public virtual void AddPlanet(Transform positioToAddThePlanet, float _maxLevel)
    {

    }




}
