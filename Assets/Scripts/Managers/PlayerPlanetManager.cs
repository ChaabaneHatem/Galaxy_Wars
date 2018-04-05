using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanetManager : PlanetManager
{


    //all player planets given by the game flow as transform parent 
    public Transform playerPlanets;
    //public Dictionary<Transform, PlayerPlanet> listePlayerPlanets;


    public override void InitPlanet(Transform _PlayerPlanets)
    {
        playerPlanets = _PlayerPlanets;
        //listePlayerPlanets = new Dictionary<Transform, PlayerPlanet>();
        listPlanetForEveryManager = new Dictionary<Transform, Planet>();
        foreach (Transform playerPlanet in playerPlanets)
        {
            if (playerPlanet != null)
            {
                PlayerPlanet playerPlanetComponemt = playerPlanet.GetComponent<PlayerPlanet>();
                playerPlanetComponemt.InitPlanet();
                //listePlayerPlanets.Add(playerPlanet, playerPlanetComponemt);
                listPlanetForEveryManager.Add(playerPlanet, playerPlanetComponemt);
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

    //remove a planet from the list anf in the game 
    public override void RemovePlanet(Transform planetToRemove)
    {
        if (listPlanetForEveryManager.ContainsKey(planetToRemove))
        {
            listPlanetForEveryManager.Remove(planetToRemove);
            GameObject.Destroy(planetToRemove.gameObject);
        }
    }


    public override void AddPlanet(Transform positioToAddThePlanet)
    {
        GameObject playerPlanet = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\Entities\\PlayerPlanet"));
        playerPlanet.name = "PlayerPlanet";
        playerPlanet.tag = GV.PLAYER_PLANET_TAG;
        playerPlanet.layer = LayerMask.NameToLayer(GV.PLAYER_PLANET_TAG);
        playerPlanet.transform.position = positioToAddThePlanet.position;
        playerPlanet.transform.SetParent(GameObject.FindGameObjectWithTag(GV.PARENT_PLAYER_PLANET).transform);
        PlayerPlanet playerPlanetComponent = playerPlanet.GetComponent<PlayerPlanet>();
        if (playerPlanetComponent == null)
        {
            Debug.LogError("player planet component not attached to the new planet " + playerPlanet.name);
        }
        else
        {
            playerPlanetComponent.InitPlanet();
            listPlanetForEveryManager.Add(playerPlanet.transform, playerPlanetComponent);
        }
    }


}
