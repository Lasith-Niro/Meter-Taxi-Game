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

    [Header("Timer")]
    public Slider timeBar;
    private bool timeStart = false;
    private float timeLeft;

    #endregion

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("CarDrive");
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
    }

    private void Update()
    {
        //startPoint = GameObject.FindGameObjectWithTag("Start");
        if (timeStart)
        {
            timeBar.gameObject.SetActive(true);
            timeBar.maxValue = predictedTime;

            timeLeft -= Time.deltaTime;
            timeBar.value = timeLeft;
        }
        else
        {
            timeBar.gameObject.SetActive(false);
        }
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
        random2 = GetRandom(spawnPoints.Length);
        randomTransform = spawnPoints[random2].transform;
        if (sleep)
        {
            // start a new ride
            StartCoroutine(SpawnWithDelay(obj, randomTransform, nextDelay));
            FindObjectOfType<AudioManager>().Play("Spawn");
            StartCoroutine(NextPassenger());
        }
        else
        {
            // the destination

            
            FindObjectOfType<AudioManager>().Play("FollowTheMap");
            StartCoroutine(SpawnWithDelay(obj, randomTransform, 0));
        }
    }

    public IEnumerator NextPassenger()
    {
        yield return new WaitForSeconds(3.0f);
        FindObjectOfType<AudioManager>().Play("NextPassengerReady");
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
        if (collision.gameObject.tag == "Start")
        {
            Debug.Log("Start timer!!!");
            predictedTime = GetRandom(4, 12);
            startTime = System.DateTime.UtcNow;
            GameObject oldGO = collision.gameObject;
            Destroy(collision.gameObject);
            AssignDestination(endPoint, oldGO);

            // start timer
            
            timeStart = true;
            timeLeft = predictedTime;


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
            



            timeStart = false;
        }
    }

    public int CalculateEarnings(int predictedTime, int timeDiff, int baseCost)
    {
        if (timeDiff < predictedTime)
        {
            FindObjectOfType<AudioManager>().Play("NiceRide");
            return baseCost;
        }
        else
        {
            //Debug.Log("Calculating linear function");
            return LinearEquation(timeDiff, baseCost);
        }
    }

    // y − y1 = -m(x − x1)
    private int LinearEquation(int timeDiff, int baseCost)
    {
        double y = (-0.85 * timeDiff) + baseCost;
        Debug.Log(y);
        return (int)y;
    }
}
