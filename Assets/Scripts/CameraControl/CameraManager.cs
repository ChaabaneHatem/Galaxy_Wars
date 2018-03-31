using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{

    #region singleton
    private static CameraManager instance;

    private CameraManager() { }

    public static CameraManager Instance
    {
        get
        {
            if (instance == null)
                instance = new CameraManager();

            return instance;
        }
    }
    #endregion singleton


    //attributs 
    public CameraControl cameraControl;
    public Transform GameObjectCamera;

    public void init()
    {
        GameObjectCamera = Camera.main.gameObject.transform;
        cameraControl = GameObjectCamera.gameObject.AddComponent<CameraControl>();
        cameraControl.cameraSpeed = GV.CAMERA_SPEED;
        cameraControl.step = new Vector3(0, 0, 0);
    }

    public void UpdateCamera(float dt)
    {
        cameraControl.step = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.RightArrow))
        {
            cameraControl.step = new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftArrow))
        {
            cameraControl.step = new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.DownArrow))
        {
            cameraControl.step = new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.UpArrow))
        {
            cameraControl.step = new Vector3(1, 0, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            cameraControl.step = new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            cameraControl.step = new Vector3(0, -1, 0);
        }

        GameObjectCamera.position = GameObjectCamera.position + cameraControl.step * cameraControl.cameraSpeed * dt;
    }


}
