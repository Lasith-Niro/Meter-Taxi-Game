using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleFollow : MonoBehaviour
{
    public Transform vehicle;
    public float speed = 10.0f;
    public Vector3 dist;
    public Transform lookTarget;

    private void LateUpdate()
    {
        Vector3 dPosition = vehicle.position + dist;
        Vector3 sPosition = Vector3.Lerp(transform.position, dPosition, speed * Time.deltaTime);
        transform.position = sPosition;
        transform.LookAt(lookTarget.position);
    }

}
