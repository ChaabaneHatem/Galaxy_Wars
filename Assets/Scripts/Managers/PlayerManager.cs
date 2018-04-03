using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Selection
{

    #region singleton
    private static PlayerManager instance;

    private PlayerManager() { }

    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
                instance = new PlayerManager();

            return instance;
        }
    }
    #endregion singleton


    //info of the selection part
    public bool isClicked;
    public bool isSelected;
    public Vector3 firstPositionClick;
    public Vector3 lastPositionClick;
    public float distanceOfSelection;

    //sprite : cercle of the selection
    GameObject selection;


    //the ParticulePlayer selected
    Transform destinationPoint;

    public void InitPlayerManager()
    {
        isClicked = false;
        isSelected = false;

        firstPositionClick = new Vector3();
        firstPositionClick = new Vector3();
        distanceOfSelection = 0;

        destinationPoint = new GameObject().transform;
        selection = new GameObject();
        InitSelection();
    }


    public void UpdatePlayerManager(float dt)
    {
        if (destinationPoint.position != null)
        {
            if (listOfSelectedParticulePlayer != null)
            {
                ParticuleManager.Instance.ControlParticulePlayer(listOfSelectedParticulePlayer, destinationPoint);
            }
        }
    }



    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (!isSelected)
        {
            Debug.Log("mousedown");
            isClicked = true;
            if (Physics.Raycast(ray, out raycastHit))
            {
                firstPositionClick = lastPositionClick = raycastHit.point;
            }
            selection = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\SpriteSelection\\Selection"));
            selection.name = "CercleSelection";
            selection.transform.localScale = new Vector3(0, 0, 0);
            selection.gameObject.transform.position = firstPositionClick;

            distanceOfSelection = Vector3.Distance(firstPositionClick, lastPositionClick);
            selection.transform.localScale = new Vector3(distanceOfSelection, distanceOfSelection, 0);
        }
        if (isSelected)
        {
            if (Physics.Raycast(ray, out raycastHit))
            {
                destinationPoint.position = raycastHit.point;
                isSelected = false;


                //test function 

                ParticuleManager.Instance.ControlParticulePlayer(listOfSelectedParticulePlayer, destinationPoint);


            }
        }
    }


    private void OnMouseOver()
    {
        Debug.Log("mousedover");
        if (isClicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                lastPositionClick = raycastHit.point;
            }
            //LastClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            distanceOfSelection = Vector3.Distance(firstPositionClick, lastPositionClick);
            selection.transform.localScale = new Vector3(distanceOfSelection, distanceOfSelection, 1);
        }
    }


    private void OnMouseUp()
    {
        Debug.Log("mouse Up");
        isClicked = false;
        isSelected = true;

        if (destinationPoint == null)
        {
            destinationPoint = new GameObject().transform;
        }
        //destinationPoint.position = new Vector3();
        GameObject.Destroy(selection);
    }




}
