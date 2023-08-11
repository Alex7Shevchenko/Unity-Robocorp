using UnityEngine;

public class RaisingPlatform : MonoBehaviour
{
    [Header("Platform Settings")]
    [Tooltip("The object that triggers the platform movement")]
    [SerializeField] GameObject triggeringObject;
    [Tooltip("The material the platform should be")]
    [SerializeField] Material desiredMat;
    [Tooltip("The material the platform is when the player is not near")]
    [SerializeField] Material startingMat;
    [Tooltip("The platform that s being moved")]
    [SerializeField] GameObject platform;
    [Tooltip("The lowest point of the platform")]
    [SerializeField] float startingHeight;

    [Header("Detection Radius Of Platform Activation")]
    [Tooltip("The mask that detects if the platform should move or not")]
    [SerializeField] LayerMask detectingLayer;
    [Tooltip("The radius that detects if the platform should move")]
    [SerializeField] float outerRadius;
    [Tooltip("The radius that when entered, the platform will reach its final destination")]
    [SerializeField] float innerRadius;

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
    }

    private void Start()
    {
        platform.GetComponent<Renderer>().material = startingMat;

        startPos = platform.transform.position;
        endPos = transform.position;
    }

    private void Update()
    {
        var playerInOuterTrigger = Physics.CheckSphere(transform.position, outerRadius, detectingLayer);

        if (playerInOuterTrigger)
        {
            float distanceToCenter = Vector3.Distance(triggeringObject.transform.position, transform.position);
            float normilizedDistance = Mathf.InverseLerp(outerRadius, innerRadius, distanceToCenter);

            platform.GetComponent<Collider>().enabled = true;
            platform.transform.position = Vector3.Lerp(startPos, endPos, normilizedDistance);
            platform.GetComponent<Renderer>().material.color = Color.Lerp(startingMat.color, desiredMat.color, normilizedDistance);
        }
        else
        {
            platform.GetComponent<Collider>().enabled = false;
        }
    }
}