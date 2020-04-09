using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_target : MonoBehaviour
{
    public Camera camera;
    public Transform target;
    public LayerMask raycastMask;
    public float range = 10f;


    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.tag == "GameCamera")
        {
            Ray screenRay = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit, range))
            { 
                targetPosition = hit.point;
            }
            HandleTarget();
        }

    }
    protected virtual void HandleTarget()
    {
        if (target)
        {
            target.position = targetPosition;
        }
    }
}
