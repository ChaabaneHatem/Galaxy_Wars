using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV
{

    //information of every team
    public enum TEAM { ENEMY, PLAYER, NEUTRAL, SUPERNOVA };

    //planet informations
    public enum PLANET_TYPE { ENEMY, PLAYER, NEUTRAL, SUPERNOVA };
    public static readonly float PLANET_MAX_PARTICULE_PER_LEVEL = 25;
    public static readonly float PLANET_TIME_GENERATION_PARTICULE = 1;
    //player planet
    public static readonly string PLAYER_PLANET_TAG = "PlayerPlanet";
    public static readonly string PARENT_PLAYER_PLANET = "ParentPlayerPlanet";
    public static readonly string PARENT_PARTICULE_PLAYER_TAG = "ParentParticulePlayer";
    //enemy planet
    public static readonly string ENEMY_PLANET_TAG = "EnemyPlanet";
    public static readonly string PARENT_ENEMY_PLANET = "ParentEnemyPlanet";
    public static readonly string PARENT_PARTICULE_ENNEMY_TAG = "ParentParticuleEnemy";
    public static readonly float TIME_ENEMY_TO_ATTACK = 30;

    //neutral planet
    public static readonly string NEUTRAL_PLANET_TAG = "NeutralPlanet";
    public static readonly string PARENT_NEUTRAL_PLANET = "ParentNeutralPlanet";
    //super nova planet 
    public static readonly string SUPERNOVA_PLANET_TAG = "SuperNovaPlanet";
    public static readonly string PARENT_SUPERNOVA_PLANET = "ParentSuperNovaPlanet";
    public static readonly float SUPERNOVA_MAX_PARTICULE_PER_LEVEL = PLANET_MAX_PARTICULE_PER_LEVEL * 2;
    public static readonly float SUPERNOVA_COEFFICIENT = 2;


    //particule informations
    //public enum PARTICULE_TYPE { ENEMY, PLAYER };
    public static readonly float PARTICULE_VALUE = 1;
    public static readonly string PLAYER_PARTICULE_TAG = "ParticulePlayer";
    public static readonly string ENEMY_PARTICULE_TAG = "ParticuleEnemy";


    //game information 
    public static readonly string ENVIRONMENT_TAG = "Environment";



    //information Camera Control
    public static readonly float CAMERA_SPEED = 10;

    //useful function
    public static float GetRandomFloat(Vector2 vector)
    {
        return Random.Range(vector.x, vector.y);
    }

    public static int GetRandomInt(Vector2 vector)
    {
        return (int)Random.Range(vector.x, vector.y);
    }

}
