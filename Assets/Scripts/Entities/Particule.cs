using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Particule : MonoBehaviour
{

    NavMeshAgent meshAgent;
    Transform destination;
    public float value = GV.PARTICULE_VALUE;
    public GV.PLANET_TYPE particuleType;

    //get the navmeshagent conponement 
    public void initParticule()
    {
        NavMeshAgent _meshAgent = gameObject.transform.GetComponent<NavMeshAgent>();
        if (_meshAgent != null)
        {
            Debug.LogError("Component NavMeshAgent not attached with the gameObject" + gameObject.name);
        }
        else
        {
            meshAgent = _meshAgent;
        }
    }


    //set la destination d'une particule 
    public void setParticuleDestination(Transform _destination)
    {
        destination = _destination;
        meshAgent.SetDestination(destination.position);
    }



}
