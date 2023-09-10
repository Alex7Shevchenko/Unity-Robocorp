using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaisingPlatform : MonoBehaviour
{
    [Header("Platform Settings")]
    [Tooltip("The objects that triggers the platform movement")]
    [SerializeField] GameObject[] triggeringObjects;
    [Tooltip("The material the platform should be")]
    [SerializeField] Material desiredMat;
    [Tooltip("The material the platform is when the player is not near")]
    [SerializeField] Material startingMat;
    [Tooltip("The platform that is being moved")]
    [SerializeField] GameObject platform, platformMesh;
    [Tooltip("The lowest point of the platform")]
    [SerializeField] float startingHeight;

    [Header("Detection Radius Of Platform Activation")]
    [Tooltip("The mask that detects if the platform should move or not")]
    [SerializeField] LayerMask detectingLayer;
    [Tooltip("The radius that detects if the platform should move")]
    [SerializeField] float outerRadius;
    [Tooltip("The radius that when entered, the platform will reach its final destination")]
    [SerializeField] float innerRadius;

    float closestObject;
    float[] distances;
    Vector3 startPos;
    Vector3 endPos;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, outerRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
    }

    private void Awake()
    {
        platform.transform.position = new Vector3(transform.position.x, startingHeight, transform.position.z);
        distances = new float[triggeringObjects.Length];
    }

    private void Start()
    {
        platformMesh.GetComponent<Renderer>().material = startingMat;

        startPos = platform.transform.position;
        endPos = transform.position;
    }

    private void Update()
    {
        var objectInOuterTrigger = Physics.CheckSphere(transform.position, outerRadius, detectingLayer);

        if (objectInOuterTrigger)
        {
            for (int i = 0; i < triggeringObjects.Length; i++)
            {
                distances[i] = Vector3.Distance(triggeringObjects[i].transform.position, transform.position);
                closestObject = distances.Min();
            }
            float normilizedDistance = Mathf.InverseLerp(outerRadius, innerRadius, closestObject);

            platform.GetComponent<Collider>().enabled = true;
            platform.transform.position = Vector3.Lerp(startPos, endPos, normilizedDistance);
            platformMesh.GetComponent<Renderer>().material.color = Color.Lerp(startingMat.color, desiredMat.color, normilizedDistance);
            platformMesh.GetComponent<Animator>().enabled = true;
        }
        else
        {
            platform.GetComponent<Collider>().enabled = false;
            platformMesh.GetComponent<Animator>().enabled = false;
            platformMesh.GetComponent<Renderer>().material.color = startingMat.color;
        }
    }
}