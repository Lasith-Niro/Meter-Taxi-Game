using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_bulet : MonoBehaviour
{
    private Vector3 startDirection;
    private Transform target;

    [Header("Score")]
    public RaycastHit hit;
    public bool hitOnce = false;
    public LayerMask buildings;
    private GameController gameController;

    

    [Header("Bullet Properties")]
    public float speed = 10f;
    private void Start()
    {
        Destroy(this.gameObject, 3.0f);

        if (target == null)
        {
            target = GameObject.FindWithTag("Target").transform;

        }
        startDirection = target.position - transform.position;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
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
        Debug.DrawRay(transform.position, Vector3.down * 0.6f, Color.red);

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, buildings))
        {
            if (hit.collider.tag == "HostileBlock" && !hitOnce)
            {
                print("HIT");
                hitOnce = true;
                gameController.score++;
            }
        }
        else
        {
            hitOnce = false;
        }
        transform.position += startDirection.normalized * Time.deltaTime * speed;
    }
}
