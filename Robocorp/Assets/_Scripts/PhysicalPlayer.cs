using UnityEngine;

public class PhysicalPlayer : MonoBehaviour
{
    [SerializeField] float forceMagnitude = 1.5f;
    [SerializeField] float size;
    [SerializeField] LayerMask layer;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, size);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rb = hit.collider.attachedRigidbody;
        var legsArea = Physics.CheckSphere(transform.position, size, layer);

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

