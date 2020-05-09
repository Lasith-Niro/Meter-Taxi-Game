using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelfController : MonoBehaviour
{
    public Transform path;
    private float speed;
    private List<Transform> nodes;
    private int rand;

    // Start is called before the first frame update
    void Start()
    {
        speed = UnityEngine.Random.Range(2, 6);
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
        rand = UnityEngine.Random.Range(0, nodes.Count);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, nodes[rand].position, speed * Time.deltaTime);
        transform.LookAt(nodes[rand]);
        if(Vector3.Distance(transform.position, nodes[rand].position) < 0.2f)
        {
            rand = UnityEngine.Random.Range(0, nodes.Count);
            speed = UnityEngine.Random.Range(2, 6);
        }
    }
}
