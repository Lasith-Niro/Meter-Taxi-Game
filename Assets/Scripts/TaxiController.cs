using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TaxiController : MonoBehaviour
{
    #region variables
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject[] spawnPoints;
    public GameObject[] tempPoints;
    public float nextDelay = 2.0f;

    public float radius = 2f;

    private int random2;
    private Transform randomTransform;

    [Header("Earnings")]
    public int earnings = 0;
    public Text showEarnings;
    private DateTime startTime;
    private DateTime endTime;
    private int predictedTime; // 4-15

    #endregion

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

    // method overloading
    int GetRandom(int low, int high)
    {
        return UnityEngine.Random.Range(low, high);
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

    private void AssignDestination(GameObject obj, GameObject go)
    {
        SpawnRandom(obj, go, false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        
            
        if (collision.gameObject.tag == "Start")
        {
            Debug.Log("Start timer!!!");
            predictedTime = GetRandom(4, 15);
            startTime = System.DateTime.UtcNow;
            GameObject oldGO = collision.gameObject;
            Destroy(collision.gameObject);
            AssignDestination(endPoint, oldGO);
        }

        if (collision.gameObject.tag == "End")
        {
            //Destroy
            Debug.Log("End timer!!!");
            endTime = System.DateTime.UtcNow;

            TimeSpan ts = endTime - startTime;
            int timeDiff = (int)ts.TotalSeconds;
            int earn = CalculateEarnings(predictedTime, timeDiff, 20);
            earnings += earn;
            showEarnings.text = earnings.ToString("000");
            /*
            Debug.Log("predictedTime:   " + predictedTime);
            Debug.Log("timeDiff:    "+  timeDiff);
            Debug.Log("earn:    "+ earn);
            */
            GameObject oldGO = collision.gameObject;
            Destroy(collision.gameObject);
            SpawnRandom(startPoint, oldGO, true);
        }
    }

    public int CalculateEarnings(int predictedTime, int timeDiff, int baseCost)
    {
        if (timeDiff < predictedTime)
        {
            return baseCost;
        }
        else
        {
            //Debug.Log("Calculating linear function");
            return LinearEquation(timeDiff, baseCost);
        }
    }

    // y − y1 = m(x − x1)
    private int LinearEquation(int timeDiff, int baseCost)
    {
        double y = (-0.85 * (timeDiff - 8)) + baseCost;
        Debug.Log(y);
        return (int)y;
    }
}
