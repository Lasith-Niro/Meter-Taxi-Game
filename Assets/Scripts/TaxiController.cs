using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaxiController : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject[] spawnPoints;
    public GameObject[] tempPoints;
    public float nextDelay = 2.0f;

    public float radius = 2f;

    private int random2;
    private Transform randomTransform;

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
    }

    private void Update()
    {
        //startPoint = GameObject.FindGameObjectWithTag("Start");
    }

    int GetRandom(int count)
    {
        return UnityEngine.Random.Range(0, count);
    }

    Vector3 GetRandomVector(Vector3 vec)
    {
        return (UnityEngine.Random.insideUnitSphere * radius) + vec;
    }

    private void SpawnRandom(GameObject obj, GameObject go, bool sleep)
    {
        //random1 = GetRandom(endPassengers.Length);
        random2 = GetRandom(spawnPoints.Length);
        
        //int index = Array.FindIndex(spawnPoints, x => x == go); // https://stackoverflow.com/questions/17995706/how-to-get-the-index-of-an-item-in-a-list-in-a-single-step
        //Debug.Log(index);
        //tempPoints = spawnPoints.Skip(index).Take(spawnPoints.Length - 1).ToArray(); // https://stackoverflow.com/questions/943635/getting-a-sub-array-from-an-existing-array
        randomTransform = spawnPoints[random2].transform;
        if (sleep)
        {
            // start a new ride
            StartCoroutine(SpawnWithDelay(obj, randomTransform, nextDelay));
        }
        else
        {
            // the destination
            StartCoroutine(SpawnWithDelay(obj, randomTransform, 0));
        }
    }

    public IEnumerator SpawnWithDelay(GameObject obj, Transform newtransform, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(obj, newtransform.position, newtransform.rotation);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        
            
        if (collision.gameObject.tag == "Start")
        {
            //Destroy
            Debug.Log("Start timer!!!");
            GameObject oldGO = collision.gameObject;
            Destroy(collision.gameObject);
            
            SpawnRandom(endPoint, oldGO, false);
        }

        if (collision.gameObject.tag == "End")
        {
            //Destroy

            Debug.Log("End timer!!!");
            GameObject oldGO = collision.gameObject;
            Destroy(collision.gameObject);
            SpawnRandom(startPoint, oldGO, true);
        }
    }
}
