using System.Runtime.CompilerServices;
using UnityEngine;

public class PhysicalPlayer : MonoBehaviour
{
    [SerializeField] float forceMagnitude = 1.5f;
    public bool playerInUI;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rb = hit.collider.attachedRigidbody;

        if(rb != null)
        {
            if (hit.moveDirection.y < -0.3f) return;

            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rb.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Force);
        }
    }
}



