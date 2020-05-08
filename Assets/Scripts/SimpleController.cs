using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 5.0f;
    public float rotSpeed = 180.0f;

    // Update is called once per frame
    void Update()
    {

        transform.Translate(0, Input.GetAxis("Elevate") * Time.deltaTime * speed, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        
        /*
        float v = Input.GetAxis("Horizontal");
        if ((v == 1.0f) || v == -1.0f)
        {
            FindObjectOfType<AudioManager>().Play("NextGear");
        }
        */
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed, 0);

    }
}