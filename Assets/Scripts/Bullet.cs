using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform aimTarget;
    private Vector3 startDirection;

    public float speed = 10f;
    //public float mass = 1.0f;
    //private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.mass = mass;

        Destroy(this.gameObject, 2.0f);

        if (aimTarget == null)
        {
            aimTarget = GameObject.FindWithTag("Target").transform;
        }

       startDirection = aimTarget.position - transform.position;
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += startDirection.normalized * Time.deltaTime * speed;   
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        Destroy(this.gameObject);
    }
    */
}
