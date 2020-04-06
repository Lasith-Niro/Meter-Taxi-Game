using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_target : MonoBehaviour
{
    public Camera camera;
    private Vector3 targetPosition;
    public Transform target;
    public LayerMask raycastMask;
    public float range = 10f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray screenRay = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(screenRay, out hit, range))
        {
            targetPosition = hit.point;
        }
        HandleTarget();

    }
    protected virtual void HandleTarget()
    {
        if (target)
        {
            target.position = targetPosition;
        }
    }
}
