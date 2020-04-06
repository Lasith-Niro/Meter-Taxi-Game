using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_bulet : MonoBehaviour
{
    private Vector3 startDirection;
    private Transform target;

    [Header("Bullet Properties")]
    public float speed = 10f;
    private void Start()
    {
        Destroy(this.gameObject, 2.0f);

        if (target == null)
        {
            target = GameObject.FindWithTag("Target").transform;

        }
        startDirection = target.position - transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        */

        transform.position += startDirection.normalized * Time.deltaTime * speed;
    }
}
