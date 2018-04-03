using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV
{

    //planet informations
    public enum PLANET_TYPE { ENEMY, PLAYER };
    public static readonly float PLANET_MAX_PARTICULE_PER_LEVEL = 25;
    public static readonly float PLANET_TIME_GENERATION_PARTICULE = 10;

    public static readonly string PLAYER_PLANET_TAG = "PlayerPlanet";
    public static readonly string PARENT_PLAYER_PLANET = "ParentPlayerPlanet";
    public static readonly string PARENT_PARTICULE_PLAYER_TAG = "ParentParticulePlayer";

    public static readonly string ENEMY_PLANET_TAG = "EnemyPlanet";
    public static readonly string PARENT_ENEMY_PLANET = "ParentEnemyPlanet";
    public static readonly string PARENT_PARTICULE_ENNEMY_TAG = "ParentParticuleEnemy";
    public static readonly float TIME_ENEMY_TO_ATTACK = 30;


    //particule informations
    public enum PARTICULE_TYPE { ENEMY, PLAYER };
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
