using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{

    //type of planet 
    public GV.PLANET_TYPE type;
    //level of planet
    [Range(1, 3)]
    public float lvl;
    //planet capacity
    public float capacity;
    //planet health
    public float currentHealth;
    //planet position 
    public Transform position;
    //material of planet 
    public Material material;

    //size of planet 
    public float size;

    // HUD progress bar 
    public Input ProgressBar;


    //virtual init planet function
    public virtual void InitPlanet()
    {

    }


    //virtual update planet 
    public virtual void UpdatePlanet(float dt)
    {

    }


    //virtual update the exception enemy planet 
    public virtual Dictionary<Transform, Particule> UpdateEnemyPlanet(float dt)
    {
        return new Dictionary<Transform, Particule>();
    }

    //gerer the collision for every planet type 
    public virtual void OnTriggerEnter(Collider other)
    {

    }





}
