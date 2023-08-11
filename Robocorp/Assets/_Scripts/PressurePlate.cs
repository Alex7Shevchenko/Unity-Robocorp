using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject plate;
    [SerializeField] string desiredObjectTag;
    [SerializeField] string[] triggerDetectionLayers;

    [SerializeField] public bool isActivated;
    [SerializeField] List<GameObject> interactables;

    LayerMask[] layers;

    private void Awake()
    {
        layers = new LayerMask[triggerDetectionLayers.Length];

        for (int i = 0; i < triggerDetectionLayers.Length; i++)
        {
            layers[i].value = LayerMask.NameToLayer(triggerDetectionLayers[i]);
        }
    }

    private void Update()
    {
        if (interactables.Count == 0)
        {
            isActivated = false;
        }

        foreach (GameObject interactable in interactables)
        {
            if(interactable.tag == desiredObjectTag && interactables.Count > 0)
            {
                isActivated = true;
            }
            else
            {
                isActivated = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (other.gameObject.layer == layers[i])
            {
                interactables.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (other.gameObject.layer == layers[i])
            {
                interactables.Remove(other.gameObject);
            }
        }
    }
}