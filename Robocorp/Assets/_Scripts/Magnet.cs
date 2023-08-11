using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] string magnetableLayerName;
    [SerializeField] float force;

    [SerializeField][HideInInspector] List<GameObject> magnetables;
    LayerMask magnetableLayer;
    bool magnetActive;

    private void Awake()
    {
        magnetableLayer = LayerMask.NameToLayer(magnetableLayerName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            magnetActive = !magnetActive;
    }

    private void FixedUpdate()
    {
        if (magnetActive)
        {
            for (int i = 0; i < magnetables.Count; i++)
            {
                Vector3 direction = magnetables[i].transform.position - transform.position;
                magnetables[i].GetComponent<Rigidbody>().AddForceAtPosition(-direction.normalized * force, transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == magnetableLayer)
            magnetables.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == magnetableLayer)
            magnetables.Remove(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == magnetableLayer)
            collision.transform.parent = transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == magnetableLayer)
            collision.transform.parent = null;
    }
}
