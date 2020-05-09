using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathAI : MonoBehaviour
{
    public Color lineColor;

    public GameObject aiCar;
    public int numberOfCars;
    
    private List<Transform> nodes = new List<Transform>();

    private void Start()
    {
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int j = 0; j < pathTransforms.Length; j++)
        {
            if (pathTransforms[j] != transform)
            {
                nodes.Add(pathTransforms[j]);
            }
        }
        int i = 0;
        while (i < numberOfCars)
        {
            int rand = UnityEngine.Random.Range(0, nodes.Count);
            Instantiate(aiCar, nodes[rand].position, transform.rotation);
            i++;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = lineColor;

        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for(int i=0; i < pathTransforms.Length; i++)
        {
            if(pathTransforms[i] != transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }

        for (int i=0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 prevNode = Vector3.zero;
            if (i > 0)
            {
                prevNode = nodes[i - 1].position;
            } else if (i == 0 && nodes.Count > 1)
            {
                prevNode = nodes[nodes.Count - 1].position;
            }
            Gizmos.DrawLine(prevNode, currentNode);
            Gizmos.DrawSphere(currentNode, 0.3f);
        }
    }

}
