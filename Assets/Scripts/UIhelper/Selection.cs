using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{

    public Dictionary<Transform, Particule> listOfSelectedParticulePlayer;


    public void InitSelection()
    {
        Debug.Log("initSelection");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GV.PLAYER_PARTICULE_TAG))
        {
            Debug.Log("enterTrigger");
            Particule particuleComponent = other.gameObject.GetComponent<Particule>();

            if (listOfSelectedParticulePlayer == null)
            {
                listOfSelectedParticulePlayer = new Dictionary<Transform, Particule>();
            }
            if (!listOfSelectedParticulePlayer.ContainsKey(other.gameObject.transform))
            {
                listOfSelectedParticulePlayer.Add(other.gameObject.transform, particuleComponent);
                Debug.Log("eleement selected count yessssss : " + listOfSelectedParticulePlayer.Count);
            }
        }
    }

}
