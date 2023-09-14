using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] string[] magnetableLayerNames = new string[0];
    [SerializeField] LayerMask[] magnetableLayers = new LayerMask[0];

    [SerializeField][HideInInspector] List<GameObject> magnetables;

    bool magnetActive;

    private void Awake()
    {
        for (int i = 0; i < magnetableLayerNames.Length; ++i)
        {
            magnetableLayers[i] = LayerMask.NameToLayer(magnetableLayerNames[i]);
        }
       
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
        for(int i = 0; i < magnetableLayers.Length; ++i)
        {
            if (other.gameObject.layer == magnetableLayers[i])
                magnetables.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < magnetableLayers.Length; ++i)
        {
            if (other.gameObject.layer == magnetableLayers[i])
                magnetables.Remove(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < magnetableLayers.Length; ++i)
        {
            if (collision.gameObject.layer == magnetableLayers[i])
                collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        for (int i = 0; i < magnetableLayers.Length; ++i)
        {
            if (collision.gameObject.layer == magnetableLayers[i])
                collision.transform.parent = null;
        }
    }
}
