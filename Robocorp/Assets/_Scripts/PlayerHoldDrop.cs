using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHoldDrop : MonoBehaviour
{
    [Header("Player Hold Settings")]
    [Tooltip("The size of the area that the play can hold things")]
    [SerializeField] float areaSize;
    [Tooltip("How fast the object will be grabbed")]
    [SerializeField] float grabPower;
    [Tooltip("Mouse sensetivity with rotation and levitation")]
    [SerializeField] float mouseRotationSpeed;
    [Tooltip("Holdable area position")]
    [SerializeField] Vector3 offset;
    [Tooltip("Thing the player can hold")]
    [SerializeField] LayerMask holdableObjects;
    [SerializeField] LayerMask ignorePlayer;
    [SerializeField] LayerMask ignoreStuff;
    [Tooltip("The position the holdable will be when held")]
    [SerializeField] Transform holdingPosition;
    [Tooltip("The camera attached to the player")]
    [SerializeField] GameObject cam;

    private GameObject currentHoldable;
    private float yAxisSpeed, xAxisSpeed, clampedY;
    private bool holdingObject;
    private Vector3 constantHoldingPosition, raycastStartPos;
    private Rigidbody currentHoldableRB;
    private RaycastHit hit;
    private float[] distances;
    private float closestHoldableDistance;
    private int closestHoldable;

    public float speed;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(offset), areaSize);

        Gizmos.color = Color.red;
        if(currentHoldable != null)
            Gizmos.DrawLine(raycastStartPos, hit.point);
    }

    private void Start()
    {
        yAxisSpeed = cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed;
        xAxisSpeed = cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed;
        constantHoldingPosition = holdingPosition.localPosition;
        ignorePlayer = ~ignorePlayer;
        ignoreStuff = ~ignoreStuff;
    }

    private void Update()
    {
        raycastStartPos = new Vector3(transform.position.x, transform.position.y + offset.y + 0.65f, transform.position.z);
        var grabbingArea = Physics.CheckSphere(transform.position + transform.TransformDirection(offset), areaSize, holdableObjects);
        var grabbingCollider = Physics.OverlapSphere(transform.position + transform.TransformDirection(offset), areaSize, holdableObjects);
        Array.Resize(ref distances, grabbingCollider.Length);
        clampedY = Mathf.Clamp(holdingPosition.localPosition.y, 0, 2.25f);
        holdingPosition.localPosition = new Vector3(holdingPosition.localPosition.x, clampedY, holdingPosition.localPosition.z);

        if (grabbingArea)
        {
            for (int i = 0; i < grabbingCollider.Length; i++)
            {
                distances[i] = Vector3.Distance(transform.position, grabbingCollider[i].transform.position);
                closestHoldableDistance = distances.Min();
            }

            closestHoldable = Array.IndexOf(distances, closestHoldableDistance);

            if (holdingObject == false)
                currentHoldable = grabbingCollider[closestHoldable].gameObject;

            currentHoldableRB = currentHoldable.GetComponent<Rigidbody>();
        }
        else
        {
            currentHoldable = null;
            currentHoldableRB = null;
        }

        if (Input.GetKeyDown(KeyCode.F) && grabbingArea)
            holdingObject = !holdingObject;

        if(currentHoldable == null)
            holdingObject = false;

        if (currentHoldable != null)
        {
            var direction = currentHoldable.transform.position - raycastStartPos;
            var ray = new Ray(raycastStartPos, direction);
            Physics.Raycast(ray, out hit, 100f, ignorePlayer);

            if (holdingObject && grabbingArea && hit.collider.name == currentHoldable.name)
            {
                currentHoldableRB.angularVelocity = new Vector3(0,0,0);
                var position = (holdingPosition.position - currentHoldable.transform.position) * grabPower;
                currentHoldableRB.velocity = position;

                if (Input.GetKey(KeyCode.Mouse0))
                    StartRotating();
                else if (Input.GetKey(KeyCode.Mouse1))
                    mouseUpDown();
                else
                    FreeRotation();
            }
            else
            {
                FreeRotation();
                FreeUpDown();
            }
        }
        else
        {
            FreeRotation();
            FreeUpDown();
        }
    }

    private void mouseRotation()
    {
/*        float XaxisRotation = Input.GetAxis("Mouse X") * mouseRotationSpeed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * mouseRotationSpeed;
        currentHoldable.transform.Rotate(transform.up, XaxisRotation, Space.World);
        currentHoldable.transform.Rotate(transform.right, YaxisRotation, Space.World);
        cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 0f;
        cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 0f;*/
    }

    private void mouseUpDown()
    {
        float YaxisRotation = Input.GetAxis("Mouse Y") * mouseRotationSpeed / 25f;
        holdingPosition.Translate(Vector3.up * YaxisRotation);
        cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 0f;
        cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 0f;
    }

    private void FreeRotation()
    {
        cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = xAxisSpeed;
        cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = yAxisSpeed;
    }

    private void FreeUpDown()
    {
        holdingPosition.localPosition = constantHoldingPosition;
    }

    void StartRotating()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, ignoreStuff))
        {
            Quaternion lookRotation = Quaternion.LookRotation(hit.point - currentHoldable.transform.position);

            float step = speed * Time.deltaTime;
            currentHoldable.transform.rotation = Quaternion.RotateTowards(currentHoldable.transform.rotation, lookRotation, step);
        }
    }
}
