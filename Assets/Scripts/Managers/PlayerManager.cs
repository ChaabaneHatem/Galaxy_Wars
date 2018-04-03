using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
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


    //sprite : cercle of the selection
    Selection selection;
    bool isClicked;
    int layerMaskEnvironment;


    //list if selected 

    //the ParticulePlayer destination
    Transform destinationPoint;

    public void InitPlayerManager()
    {
        isClicked = false;
        destinationPoint = new GameObject().transform;
        layerMaskEnvironment = 1 << LayerMask.NameToLayer(GV.ENVIRONMENT_TAG);
    }


    public void UpdatePlayerManager()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;


        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, layerMaskEnvironment))
            {
                isClicked = true;
                Debug.Log("mouseDown && mode selection particule");
                //create the sprite cercle and get the conponemt selection and init it 
                GameObject selectionSprite = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\SpriteSelection\\Selection"));
                selectionSprite.transform.position = raycastHit.point;
                selectionSprite.gameObject.name = "cercleSelection";
                selection = selectionSprite.GetComponent<Selection>();
                if (selection != null)
                    selection.InitSelection(raycastHit.point);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, layerMaskEnvironment))
            {
                Debug.Log("mouseDown && mode selection destinaion");
                destinationPoint.position = raycastHit.point;
                if (selection.listOfSelectedParticulePlayer != null)
                    ParticuleManager.Instance.ControlParticulePlayer(GetDictionaryParticuleFromList(selection.listOfSelectedParticulePlayer), destinationPoint);
            }
        }


        if (isClicked)
        {
            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, layerMaskEnvironment))
            {
                if (selection != null)
                {
                    Debug.Log("raypoint : " + raycastHit.point);
                    selection.UpdateSelection(raycastHit.point);

                }
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("mouse Up && mode selelection particule");
            isClicked = false;

            //destroy the sprite cerce

        }

        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("mouse Up && mode selelection destination");
            selection.listOfSelectedParticulePlayer.Clear();
        }

    }



    private Dictionary<Transform, Particule> GetDictionaryParticuleFromList(List<Transform> listParticuleTransform)
    {
        Dictionary<Transform, Particule> listParticule = new Dictionary<Transform, Particule>();
        foreach (Transform ParticuleTransform in listParticuleTransform)
        {
            if (ParticuleTransform != null)
            {
                Particule particuleComponent = ParticuleTransform.GetComponent<Particule>();
                if (particuleComponent != null)
                    listParticule.Add(ParticuleTransform, particuleComponent);
            }
        }
        return listParticule;
    }


    //private void OnMouseDown()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit raycastHit;
    //    Debug.Log("mousedown");
    //    isClicked = true;
    //    isSelected = false;
    //    if (Physics.Raycast(ray, out raycastHit))
    //    {
    //        firstPositionClick = lastPositionClick = raycastHit.point;
    //    }
    //    selection = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs\\SpriteSelection\\Selection"));
    //    selection.name = "CercleSelection";
    //    selection.transform.localScale = new Vector3(0, 0, 0);
    //    selection.gameObject.transform.position = firstPositionClick;

    //    distanceOfSelection = Vector3.Distance(firstPositionClick, lastPositionClick);
    //    selection.transform.localScale = new Vector3(distanceOfSelection / 2, distanceOfSelection / 2, 1);

    //}


    //private void OnMouseOver()
    //{
    //    Debug.Log("mousedover");
    //    if (isClicked)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit raycastHit;
    //        if (Physics.Raycast(ray, out raycastHit))
    //        {
    //            lastPositionClick = raycastHit.point;
    //        }
    //        //LastClickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        distanceOfSelection = Vector3.Distance(firstPositionClick, lastPositionClick);
    //        selection.transform.localScale = new Vector3(distanceOfSelection / 2, distanceOfSelection / 2, 1);
    //    }
    //}


    //private void OnMouseUp()
    //{
    //    Debug.Log("mouse Up");
    //    isClicked = false;
    //    isSelected = true;
    //    if (destinationPoint == null)
    //    {

    //        destinationPoint = new GameObject().transform;
    //        destinationPoint.position = new Vector3(1.5f, 0, 1.5f);
    //    }
    //    ParticuleManager.Instance.ControlParticulePlayer(listOfSelectedParticulePlayer, destinationPoint);
    //    //isSelected = true;
    //    GameObject.Destroy(selection);

    //}



}
