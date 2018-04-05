using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralPlanetManager : PlanetManager
{
    //parent of all the neutral planet 
    public Transform neutralPlanets;

    public override void InitPlanet(Transform planetTransform)
    {
        neutralPlanets = planetTransform;

        listPlanetForEveryManager = new Dictionary<Transform, Planet>();
        foreach (Transform neutralPlanet in neutralPlanets)
        {
            if (neutralPlanet != null)
            {
                NeutralPlanet neutralPlanetComponemt = neutralPlanet.GetComponent<NeutralPlanet>();
                neutralPlanetComponemt.InitPlanet();
                listPlanetForEveryManager.Add(neutralPlanet, neutralPlanetComponemt);
            }
        }
    }


    public override void UpdatePlanet(float dt)
    {

    }

    //remove neutral Planet
    public override void RemovePlanet(Transform planetToRemove)
    {
        if (listPlanetForEveryManager.ContainsKey(planetToRemove))
        {
            listPlanetForEveryManager.Remove(planetToRemove);
            GameObject.Destroy(planetToRemove.gameObject);
        }
    }



    //add new neutral planet 
    public override void AddPlanet(Transform positioToAddThePlanet)
    {
        GameObject neutralPlanet = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\Entities\\NeutralPlanet"));
        neutralPlanet.name = "NeutralPlanet";
        neutralPlanet.tag = GV.NEUTRAL_PLANET_TAG;
        neutralPlanet.layer = LayerMask.NameToLayer(GV.NEUTRAL_PLANET_TAG);
        neutralPlanet.transform.position = positioToAddThePlanet.position;
        neutralPlanet.transform.SetParent(GameObject.FindGameObjectWithTag(GV.PARENT_NEUTRAL_PLANET).transform);
        NeutralPlanet neutralPlanetComponent = neutralPlanet.GetComponent<NeutralPlanet>();
        if (neutralPlanetComponent == null)
        {
            Debug.LogError("Neutral planet component not attached to the new planet " + neutralPlanet.name);
        }
        else
        {
            neutralPlanetComponent.InitPlanet();
            listPlanetForEveryManager.Add(neutralPlanet.transform, neutralPlanetComponent);
        }
    }

}
