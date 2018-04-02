﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanet : Planet
{
    private static float nbTotalParticulePlayer;
    private float currentTime;
    private Transform parentParticulePlayer;

    public void initPlayerPlanet()
    {
        nbTotalParticulePlayer = 0;
        currentTime = 0;
        type = GV.PLANET_TYPE.PLAYER;
        //lvl = 2;
        capacity = lvl * GV.PLANET_MAX_PARTICULE_PER_LEVEL;
        currentHealth = capacity;
        position = gameObject.transform;
        material = Resources.Load<Material>("Materials\\PlayerPlanet");
        size = lvl * 2f;

        //if the planet are static object they will not change when the game run
        //gameObject.transform.localScale += new Vector3(size, size, size);
    }

    public void UpdatePlayerPlanet(float dt)
    {
        GenerationParticule(dt);



        //test partie 
        if (currentHealth == 0)
        {
            Debug.Log("planet destroyed");
            //GameObject.Destroy(this.gameObject);
        }
    }



    private void GenerationParticule(float dt)
    {
        currentTime += dt;

        if (currentTime >= (GV.PLANET_TIME_GENERATION_PARTICULE / lvl))
        {
            if (parentParticulePlayer == null)
            {
                parentParticulePlayer = new GameObject().transform;
                parentParticulePlayer.name = "ParentParticulePlayer";
                parentParticulePlayer.tag = GV.PARENT_PARTICULE_PLAYER_TAG;
            }

            GameObject particule = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\Entities\\ParticulePlanet"));
            particule.transform.SetParent(parentParticulePlayer);
            particule.transform.position = gameObject.transform.position + Random.onUnitSphere.normalized * 1.5f;
            particule.GetComponent<Particule>().particuleType = type;
            Debug.Log(" currentTime : " + currentTime);
            currentTime = 0;
            nbTotalParticulePlayer++;
            Debug.Log(" ParentParticulePlayer : " + nbTotalParticulePlayer);

        }
    }


    //function plus detail with get damage and get energy a faiire ....


}
