﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public float currentTimeToEnemyAttack;

    //transform who know the position af the player enemy for the IA
    //public Transform playerPlanets;
    public Dictionary<Transform, Planet> listePlayerPlanets;

    public void InitParticuleManager()
    {
        listeParticuleEnemy = new Dictionary<Transform, Particule>();


        // call initialation of the particule and the particule not reated yet !!!!! stupid !!! should do it when it created 
        //foreach (KeyValuePair<Transform, Particule> kv in listeParticuleEnemy)
        //{
        //    if (kv.Key != null)
        //    {
        //        kv.Value.initParticule();
        //    }
        //}

        currentTimeToEnemyAttack = 0;

        //position of all playerPlanets
        //listePlayerPlanets = PlayerPlanetManager.Instance.listePlayerPlanets;
        listePlayerPlanets = PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.PLAYER).listPlanetForEveryManager;
    }


    public void UpdateParticuleManager(float dt)
    {
        Dictionary<Transform, Particule> listeParticulePerFrame = PlanetManagerMaster.Instance.GetPlanetManager(GV.TEAM.ENEMY).UpdateEnemyPlanetsManager(dt);
        //Dictionary<Transform, Particule> listeParticulePerFrame = EnemyPlanetManager.Instance.UpdateEnemyPlanetsManager(dt);
        if (listeParticulePerFrame != null)
        {
            foreach (KeyValuePair<Transform, Particule> kv in listeParticulePerFrame)
            {
                listeParticuleEnemy.Add(kv.Key, kv.Value);
            }
        }
        if (listeParticuleEnemy != null)
            ControlParticuleEnemy(dt, listeParticuleEnemy, listePlayerPlanets);
    }



    public void ControlParticuleEnemy(float dt, Dictionary<Transform, Particule> _listeParticuleEnemy, Dictionary<Transform, Planet> _PlayerPlanets)
    {
        currentTimeToEnemyAttack += dt;
        int RandomNumber = GV.GetRandomInt(new Vector2(1, _listeParticuleEnemy.Count - 1));
        if (currentTimeToEnemyAttack >= GV.TIME_ENEMY_TO_ATTACK)
        {
            if (_listeParticuleEnemy.ElementAt(RandomNumber).Key == null)
            {
                currentTimeToEnemyAttack -= dt;
            }
            else if (_listeParticuleEnemy.ElementAt(RandomNumber).Key != null)
            {
                Vector3 RandomExistParticule = _listeParticuleEnemy.ElementAt(GV.GetRandomInt(new Vector2(1, _listeParticuleEnemy.Count - 1))).Key.position;
                Transform nearPlayerPlanetPosition = GetMostNearPositionOfPlanet(RandomExistParticule, _PlayerPlanets);
                foreach (KeyValuePair<Transform, Particule> kv in _listeParticuleEnemy)
                {
                    if (kv.Key != null)
                    {
                        kv.Value.setParticuleDestination(nearPlayerPlanetPosition);
                    }
                }
                currentTimeToEnemyAttack = 0;
            }
        }
    }



    public Transform GetMostNearPositionOfPlanet(Vector3 particulePosition, Dictionary<Transform, Planet> _listeOfPlayerPlanet)
    {
        Transform nearPosition = new GameObject().transform;
        float theSmallDistance = 10e5f;

        foreach (KeyValuePair<Transform, Planet> kv in _listeOfPlayerPlanet)
        {
            if (theSmallDistance > Vector3.Distance(particulePosition, kv.Value.position.position))
            {
                theSmallDistance = Vector3.Distance(particulePosition, kv.Value.position.position);
                nearPosition = kv.Value.position;
            }
        }

        return nearPosition;
    }



    //public Transform GetRandomKeyFromDictionnary(Dictionary<Transform, Particule> liste)
    //{
    //    Transform transformKey;
    //    int randomInt = GV.GetRandomInt(new Vector2(1, liste.Count - 1));
    //    for (int i = randomInt; i < randomInt + 1; i++)
    //    {
    //        transformKey = liste.ElementAt(i).Key;
    //    }

    //    return new GameObject().transform;
    //}


    public void ControlParticulePlayer(Dictionary<Transform, Particule> listSelectedParticulePlayer, Transform destination)
    {
        //Debug.Log("appel control particule player ");
        foreach (KeyValuePair<Transform, Particule> kv in listSelectedParticulePlayer)
        {
            if (kv.Key != null)
            {
                kv.Value.setParticuleDestination(destination);
            }
        }
    }


}
