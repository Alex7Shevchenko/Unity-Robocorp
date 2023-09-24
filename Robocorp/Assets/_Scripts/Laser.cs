using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [Header("Laser Settings")]
    [Header("Choose the type of laser.\nThe type of the laser will dictate what box it will detect.")]
    [Space]
    public bool isRed;
    public bool isYellow;
    public bool isGreen;
    public bool isBlue;
    public bool isPurple;
    [Space]
    [Tooltip("What layers the raycast will ignore collision with.")]
    [SerializeField] LayerMask ignoreLayers;
    [Space]
    [Tooltip("the max amount of reflections the laser can have.")]
    [SerializeField] int maxReflections;
    [Tooltip("the max length of the laser.")]
    [SerializeField] float maxLength;
    [Space]
    [SerializeField] Material redMat;
    [SerializeField] Material yellowMat;
    [SerializeField] Material greenMat;
    [SerializeField] Material blueMat;
    [SerializeField] Material purpleMat;

     public bool isActivated;

    string boxTag;
    LineRenderer laser;
    Ray ray;
    RaycastHit hit;

    private void Start()
    {
        laser = GetComponent<LineRenderer>();
        ignoreLayers = ~ignoreLayers;

        Invoke(nameof(LaserColorSetter), 0.1f);

/*        if (isRed)
        {
            isYellow = false;
            isGreen = false;
            isBlue = false;
            isPurple = false;
            laser.material = redMat;
            boxTag = "Red Box";
        }

        if (isYellow)
        {
            isGreen = false;
            isBlue = false;
            isPurple = false;
            laser.material = yellowMat;
            boxTag = "Yellow Box";
        }

        if (isGreen)
        {
            isBlue = false;
            isPurple = false;
            laser.material = greenMat;
            boxTag = "Green Box";
        }

        if (isBlue)
        {
            isPurple = false;
            laser.material = blueMat;
            boxTag = "Blue Box";
        }

        if (isPurple)
        {
            laser.material = purpleMat;
            boxTag = "Purple Box";
        }*/
    }

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        laser.positionCount = 1;
        laser.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < maxReflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength, ignoreLayers))
            {
                laser.positionCount += 1;
                laser.SetPosition(laser.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                if (hit.collider.tag != "Mirror" || hit.collider == null)
                {
                    IsActiveBool(isRed);
                    IsActiveBool(isYellow);
                    IsActiveBool(isGreen);
                    IsActiveBool(isBlue);
                    IsActiveBool(isPurple);
                    break;
                }
            }
            else
            {
                laser.positionCount += 1;
                laser.SetPosition(laser.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }

    private void IsActiveBool(bool laserColor)
    {
        if (laserColor)
            if (hit.collider.tag == boxTag)
            {
                isActivated = true;
            }
            else
            {
                isActivated = false;
            }
    }

    private void LaserColorSetter()
    {
        if (isRed)
        {
            isYellow = false;
            isGreen = false;
            isBlue = false;
            isPurple = false;
            laser.material = redMat;
            boxTag = "Red Box";
        }

        if (isYellow)
        {
            isGreen = false;
            isBlue = false;
            isPurple = false;
            isRed = false;
            laser.material = yellowMat;
            boxTag = "Yellow Box";
        }

        if (isGreen)
        {
            isRed = false;
            isYellow = false;
            isBlue = false;
            isPurple = false;
            laser.material = greenMat;
            boxTag = "Green Box";
        }

        if (isBlue)
        {
            isRed = false;
            isYellow = false;
            isGreen = false;
            isPurple = false;
            laser.material = blueMat;
            boxTag = "Blue Box";
        }

        if (isPurple)
        {
            isRed = false;
            isYellow = false;
            isGreen = false;
            isBlue = false;
            laser.material = purpleMat;
            boxTag = "Purple Box";
        }
    }
}