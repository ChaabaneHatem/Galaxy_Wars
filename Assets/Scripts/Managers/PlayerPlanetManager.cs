using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanetManager
{
    #region singleton
    private static PlayerPlanetManager instance;

    private PlayerPlanetManager() { }

    public static PlayerPlanetManager Instance
    {
        get
        {
            if (instance == null)
                instance = new PlayerPlanetManager();

            return instance;
        }
    }
    #endregion singleton

    //all player planets given by the game flow as transform parent 
    public Transform playerPlanets;
    private Dictionary<Transform, PlayerPlanet> listePlayerPlanets;


    public void InitPlayerPlanetManager(Transform _PlayerPlanets)
    {
        playerPlanets = _PlayerPlanets;
        listePlayerPlanets = new Dictionary<Transform, PlayerPlanet>();
        foreach (Transform playerPlanet in playerPlanets)
        {
            if (playerPlanet != null)
            {
                PlayerPlanet playerPlanetComponemt = playerPlanet.GetComponent<PlayerPlanet>();
                playerPlanetComponemt.initPlayerPlanet();
                listePlayerPlanets.Add(playerPlanet, playerPlanetComponemt);
            }
        }
    }


    public void UpdatePlayerPlanetManager(float dt)
    {
        foreach (KeyValuePair<Transform, PlayerPlanet> kv in listePlayerPlanets)
        {
            if (kv.Key != null)
            {
                kv.Value.UpdatePlayerPlanet(dt);
            }
        }
    }

}
