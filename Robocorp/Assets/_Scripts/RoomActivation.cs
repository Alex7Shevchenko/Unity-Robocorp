using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActivation : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float delayActivation;
    [SerializeField] GameObject[] roomsToActivate;
    [SerializeField] GameObject[] roomsToDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == player.layer)
        {
            Invoke(nameof(ActivateRooms), delayActivation);
        }

        if (other.gameObject.layer == player.layer)
        {
            Invoke(nameof(DeactivateRooms), delayActivation);
        }
    }



    private void ActivateRooms()
    {
        foreach (var room in roomsToActivate)
        {
            room.SetActive(true);
        }
    }

    private void DeactivateRooms()
    {
        foreach (var room in roomsToDeactivate)
        {
            room.SetActive(false);
        }
    }
}
