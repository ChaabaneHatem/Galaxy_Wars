using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{

    //delta time 
    private float deltaTime;

    //all player Planets
    public Transform playerPlanets;

    //all enemy planets 
    public Transform EnemyPlanets;


    // Use this for initialization
    void Start()
    {
        CameraManager.Instance.init();
        PlayerPlanetManager.Instance.InitPlayerPlanetManager(playerPlanets);
        EnemyPlanetManager.Instance.InitEnemyPlanetManager(EnemyPlanets);
        ParticuleManager.Instance.InitParticuleManager();
        PlayerManager.Instance.InitPlayerManager();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;
        CameraManager.Instance.UpdateCamera(deltaTime);
        PlayerPlanetManager.Instance.UpdatePlayerPlanetManager(deltaTime);
        ParticuleManager.Instance.UpdateParticuleManager(deltaTime);
        PlayerManager.Instance.UpdatePlayerManager(deltaTime);
    }

    private void FixedUpdate()
    {

    }
}
