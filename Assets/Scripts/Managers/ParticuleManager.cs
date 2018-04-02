using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleManager
{

    #region singleton
    private static ParticuleManager instance;

    private ParticuleManager() { }

    public static ParticuleManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ParticuleManager();

            return instance;
        }
    }
    #endregion singleton


    public Dictionary<Transform, Particule> listeParticuleEnemy;


    //transform who know the position af the player enemy for the IA
    public Transform playerPlanets;


    public void InitParticuleManager()
    {
        listeParticuleEnemy = new Dictionary<Transform, Particule>();

        //position of all playerPlanets

    }


    public void UpdateParticuleManager(float dt)
    {
        EnemyPlanetManager.Instance.UpdateEnemyPlanetsManager(dt);
    }



    public void ControlParticuleEnemy(Dictionary<Transform, Particule> listeParticuleEnemy, Transform PlayerPlanets)
    {




    }

}
