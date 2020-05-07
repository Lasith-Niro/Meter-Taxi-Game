using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;

    private void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1"))
        {
            cam1.enabled = true;
            cam2.enabled = false;
        }
        if (Input.GetKey("2"))
        {
            cam1.enabled = false;
            cam2.enabled = true;
        }
    }
}
