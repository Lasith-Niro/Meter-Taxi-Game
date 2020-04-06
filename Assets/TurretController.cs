using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public bool alerted;
    private Transform aimTarget;

    private Vector3 aimVector;
    public float interpolator = 1.0f;
    public Transform gunXform;
    public Transform domeXform;
    
    public GameObject bulletPrefab;

    public float gunSpeedOn = 0.2f;
    public float gunSpeedOff = 0.025f;

    public LayerMask raycastMask;


    private float gunTimer = 0.0f;
    public float gunInterval = 1.0f;
    private AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        
        if (aimTarget == null)
        {
            aimTarget = GameObject.FindWithTag("Player").transform;
        }
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        gunTimer -= Time.deltaTime;

        if (alerted)
        {
            interpolator = Mathf.Lerp(interpolator, 1.0f, gunSpeedOn);

            RaycastHit hit;

            if(Physics.Raycast(gunXform.position, gunXform.forward, out hit, Mathf.Infinity, raycastMask))
            {
                if((hit.collider.tag == "Player") && (gunTimer<0.0f))
                {
                    Instantiate(bulletPrefab, gunXform.position, Quaternion.identity);
                    gunTimer = gunInterval;
                    audioSource.Play();
                }
            }
            /*
            if (interpolator > 0.9)
            {
                Instantiate(bulletPrefab, gunXform.position, Quaternion.identity);

            }
            */
        }
        else
        {
            interpolator = Mathf.Lerp(interpolator, 0.0f, gunSpeedOff);

        }


        aimVector = aimTarget.position - gunXform.position;

        Quaternion aimQuat = Quaternion.LookRotation(aimVector);
        gunXform.rotation = Quaternion.Slerp(Quaternion.identity, aimQuat, interpolator);

        aimVector.y = 0.0f;
        Quaternion domeQuat = Quaternion.LookRotation(aimVector);


        domeXform.rotation = Quaternion.Slerp(Quaternion.identity, domeQuat, interpolator);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            alerted = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            alerted = false;
        }
    }
}
