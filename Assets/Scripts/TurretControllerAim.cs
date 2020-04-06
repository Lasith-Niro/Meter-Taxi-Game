using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControllerAim : MonoBehaviour
{
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
    
    // private AudioSource audioSource;

    public Camera camera1;

    private Vector3 reticleNormal;
    private Vector3 reticlePosition;

    public Transform target;

    public float damage = 10f;
    public float range = 10f;



    // Start is called before the first frame update
    void Start()
    {
        
        if (aimTarget == null)
        {
            aimTarget = GameObject.FindWithTag("Target").transform;
        }
        //audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
        gunTimer -= Time.deltaTime;

        if (camera1)
        {
            interpolator = Mathf.Lerp(interpolator, 1.0f, gunSpeedOn);

            /*
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
            
            if (interpolator > 0.9)
            {
                Instantiate(bulletPrefab, gunXform.position, Quaternion.identity);

            }
            */

            Ray screenRay = camera1.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit, range, raycastMask))
            {
                Debug.Log(hit.transform.name);
                reticlePosition = hit.point;
                reticleNormal = hit.normal;

                if ((Input.GetMouseButtonDown(0)) && (gunTimer < 0.0f))
                {
                    Instantiate(bulletPrefab, gunXform.position, Quaternion.identity);
                    gunTimer = gunInterval;
                }

                Target target = hit.transform.GetComponent<Target>();
                if(target != null)
                {
                    target.TakeDamage(damage);
                }

            }
        }
        else
        {
            interpolator = Mathf.Lerp(interpolator, 0.0f, gunSpeedOff);

        }
        HandleTarget();


        aimVector = reticlePosition - gunXform.position;

        Quaternion aimQuat = Quaternion.LookRotation(aimVector);
        gunXform.rotation = Quaternion.Slerp(Quaternion.identity, aimQuat, interpolator);

        aimVector.y = 0.0f;
        Quaternion domeQuat = Quaternion.LookRotation(aimVector);
        domeXform.rotation = Quaternion.Slerp(Quaternion.identity, domeQuat, interpolator);

       

    }

    protected virtual void HandleTarget()
    {
        if (target)
        {
            target.position = reticlePosition;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(reticlePosition, 0.5f);
    }

    void Shoot()
    {

    }
}
