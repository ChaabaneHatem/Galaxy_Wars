using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{

    public List<Transform> listOfSelectedParticulePlayer;
    public Vector3 firstPositionClick;
    public Vector3 currentPosition;
    public float rayonOfSelection;



    public void InitSelection(Vector3 _firstPositionClick)
    {
        Debug.Log("initSelection");
        firstPositionClick = _firstPositionClick;
        listOfSelectedParticulePlayer = new List<Transform>();
        rayonOfSelection = 0;
    }

    //public void CreateSelection(Vector3 _firstPositionClick)
    //{
    //    firstPositionClick = _firstPositionClick;
    //    selectionSprite = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\SpriteSelection\\Selection"));
    //    selectionSprite.transform.position = firstPositionClick;
    //    selectionSprite.gameObject.name = "cercleSelection";
    //    selectionSprite.SetActive(false);
    //}

    public void UpdateSelection(Vector3 _currentPositionOfTheMouse)
    {
        currentPosition = _currentPositionOfTheMouse;
        rayonOfSelection = Vector3.Distance(firstPositionClick, currentPosition) / 2;
        this.gameObject.transform.localScale = new Vector3(rayonOfSelection, rayonOfSelection, 1);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GV.PLAYER_PARTICULE_TAG))
        {
            //Debug.Log("enterTrigger");
            //Particule particuleComponent = other.gameObject.GetComponent<Particule>();

            if (other.gameObject != null)
            {
                if (!listOfSelectedParticulePlayer.Contains(other.gameObject.transform))
                {
                    listOfSelectedParticulePlayer.Add(other.gameObject.transform);
                }

            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(GV.PLAYER_PARTICULE_TAG))
        {
            //Debug.Log("enterTrigger");
            //Particule particuleComponent = other.gameObject.GetComponent<Particule>();

            if (other.gameObject != null)
            {
                listOfSelectedParticulePlayer.Remove(other.gameObject.transform);
            }
        }
    }


}
