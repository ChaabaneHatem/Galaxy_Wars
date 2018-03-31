using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    private float deltaTime;

    // Use this for initialization
    void Start()
    {
        CameraManager.Instance.init();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;
        CameraManager.Instance.UpdateCamera(deltaTime);
    }
}
