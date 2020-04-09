using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class test_turret : MonoBehaviour
{
    public Transform target;

    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;


    [Header("Gun Attributes")]
    public Transform gunXform;
    public float turnSpeed = 10f;
    public GameObject bulletPrefab;
    // add custom fire point
    public Transform firePoint;

    private int milliseconds = 2000;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //UpdateTarget();
        if (target == null)
        {
            return;
        }
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = lookRotation.eulerAngles;
        Vector3 rotation = Quaternion.Lerp(gunXform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        gunXform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

        if(Input.GetMouseButtonDown(0))
        // &&  (fireCountdown <= 0f))
        {
            Shoot();
            //fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void UpdateTarget()
    {
        GameObject cross = GameObject.FindGameObjectWithTag("Target");
        //float distance = Vector3.Distance(transform.position, target.transform.position);
        target = cross.transform;
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    */
}
