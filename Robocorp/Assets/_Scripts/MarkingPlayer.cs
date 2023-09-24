using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkingPlayer : MonoBehaviour
{
    [SerializeField] RandomizeRobot playerMarkingSystem;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerMarkingSystem.timesMarked++;
        }
    }
}
