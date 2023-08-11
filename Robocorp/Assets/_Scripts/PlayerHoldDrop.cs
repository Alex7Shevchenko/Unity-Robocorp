using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldDrop : MonoBehaviour
{
    [SerializeField] float areaSize, grabPower, mouseRotationSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] LayerMask holdableObjects;
    [SerializeField] Transform holdingPosition;
    [SerializeField] GameObject cam;

    float yAxisSpeed, xAxisSpeed;
    bool holdingObject;
    public Vector3 constantHoldingPosition;
    GameObject currentHoldable;
    Rigidbody currentHoldableRB;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(offset), areaSize);
    }

    private void Start()
    {
        yAxisSpeed = cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed;
        xAxisSpeed = cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed;
        constantHoldingPosition = holdingPosition.position;
    }

    private void Update()
    {
        var grabbingArea = Physics.CheckSphere(transform.position + transform.TransformDirection(offset), areaSize, holdableObjects);
        var grabbingCollider = Physics.OverlapSphere(transform.position + transform.TransformDirection(offset), areaSize, holdableObjects);

        if (grabbingArea)
        {
            if (currentHoldable == null)
                currentHoldable = grabbingCollider[0].gameObject;

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
            if (holdingObject && grabbingArea)
            {
                var position = (holdingPosition.position - currentHoldable.transform.position) * grabPower;
                currentHoldableRB.velocity = position;

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    mouseRotation();
                }
                else if (Input.GetKey(KeyCode.Mouse1))
                {
                    mouseUpDown();
                }
                else
                    ResetToStartValue();
            }
            else
                ResetToStartValue();
        }
        else
            ResetToStartValue();
    }

    private void mouseRotation()
    {
        float XaxisRotation = Input.GetAxis("Mouse X") * mouseRotationSpeed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * mouseRotationSpeed;

        currentHoldable.transform.Rotate(Vector3.down, YaxisRotation);
        currentHoldable.transform.Rotate(Vector3.right, XaxisRotation);
        cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 0f;
        cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 0f;
    }

    private void mouseUpDown()
    {
        float XaxisRotation = Input.GetAxis("Mouse X") * mouseRotationSpeed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * mouseRotationSpeed;

        holdingPosition.transform.Translate(Vector3.up * XaxisRotation);
        cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = 0f;
        cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = 0f;
    }

    private void ResetToStartValue()
    {
        cam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = xAxisSpeed;
        cam.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = yAxisSpeed;
        holdingPosition.position = constantHoldingPosition;
    }
}
