using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public bool isActivated;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
            isActivated = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isActivated = false;
    }
}
