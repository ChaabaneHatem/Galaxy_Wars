﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{

    //type of planet 
    public GV.PLANET_TYPE type;
    //max level of a planet 
    public float maxLevel;

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

    //// HUD progress bar 
    //public Input ProgressBar;

    //health bar Transform
    public Transform healthBarTransform;


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

    //function upgrade level
    public void UpgradeLevel(float currentLevel)
    {
        capacity = currentLevel * GV.PLANET_MAX_PARTICULE_PER_LEVEL;
        size = lvl;
        gameObject.transform.localScale = new Vector3(size, size, size);
    }


    public virtual void UpdateHealth(Transform _healthBar, float _currentHealth, float _capacity, Material _material)
    {
        Transform foreground = _healthBar.GetChild(1);
        Transform backround = _healthBar.GetChild(0);
        foreground.GetComponent<Renderer>().material = _material;
        float deltaXScale = (_currentHealth * backround.localScale.x) / _capacity;
        float deltaxPosition = deltaXScale / 2;
        foreground.localScale = new Vector3(deltaXScale, foreground.localScale.y, foreground.localScale.z);
        //foreGround.localPosition = foreGround.localPosition - new Vector3(deltaxPosition, 0, 0);
    }

}
