using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLaserTester : MonoBehaviour
{
    [SerializeField] Laser[] lasers = new Laser[0];

    public bool puzzleSolved;

    private void Update()
    {
        puzzleSolved = AllLasersSolved();
    }

    private bool AllLasersSolved()
    {
        foreach (var laser in lasers)
        {
            if(laser.isActivated == false)
            {
                return false;
            }
        }

        return true;
    }
}
