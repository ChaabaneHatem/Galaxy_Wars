using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManagerMaster
{

    #region singleton
    private static PlanetManagerMaster instance;

    private PlanetManagerMaster() { }

    public static PlanetManagerMaster Instance
    {
        get
        {
            if (instance == null)
                instance = new PlanetManagerMaster();

            return instance;
        }
    }
    #endregion singleton


    //dictionnaire of all the planet manger of all the planet 
    Dictionary<GV.TEAM, PlanetManager> listePlanetManager;

    public void InitPlanetManagerMaster()
    {
        listePlanetManager = new Dictionary<GV.TEAM, PlanetManager>();
    }


    public void InitPlayerPlanetManager(Transform _AllPlayerPlanetTransform)
    {
        PlayerPlanetManager playerPlanetManager = new PlayerPlanetManager();
        playerPlanetManager.InitPlanet(_AllPlayerPlanetTransform);
        listePlanetManager.Add(GV.TEAM.PLAYER, playerPlanetManager);
    }

    public void InitEnemyPlanetManager(Transform _AllEnemyPlanetTransform)
    {
        EnemyPlanetManager enemyPlanetManager = new EnemyPlanetManager();
        enemyPlanetManager.InitPlanet(_AllEnemyPlanetTransform);
        listePlanetManager.Add(GV.TEAM.ENEMY, enemyPlanetManager);
    }


    public void InitNeutralPlanetManager(Transform _AllNeutralPlanetTransform)
    {
        NeutralPlanetManager neutralPlanetManager = new NeutralPlanetManager();
        neutralPlanetManager.InitPlanet(_AllNeutralPlanetTransform);
        listePlanetManager.Add(GV.TEAM.NEUTRAL, neutralPlanetManager);
    }


    public void InitSuperNovaPlanetManager(Transform _AllSuperNovaPlanetTransform)
    {
        SuperNovaPlanetManager superNovaPlanetManager = new SuperNovaPlanetManager();
        superNovaPlanetManager.InitPlanet(_AllSuperNovaPlanetTransform);
        listePlanetManager.Add(GV.TEAM.SUPERNOVA, superNovaPlanetManager);
    }

    public void UpdatePlanetManager(float dt)
    {
        foreach (KeyValuePair<GV.TEAM, PlanetManager> kv in listePlanetManager)
        {
            if (kv.Value != null)
            {
                kv.Value.UpdatePlanet(dt);
            }
        }
        FinishGame();
    }

    public void FinishGame()
    {
        if (GetPlanetManager(GV.TEAM.PLAYER).listPlanetForEveryManager.Count == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
        if (GetPlanetManager(GV.TEAM.ENEMY).listPlanetForEveryManager.Count == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");
        }
    }

    public PlanetManager GetPlanetManager(GV.TEAM teamName)
    {
        return listePlanetManager[teamName];
    }


}
