using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject plate;
    [SerializeField] string desiredObjectTag;
    [SerializeField] string[] triggerDetectionLayers;
    [SerializeField] List<GameObject> interactables;

    [HideInInspector] public bool animationEnd = false;
    [HideInInspector] public bool animationStart = false;

    LayerMask[] layers;
    Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        layers = new LayerMask[triggerDetectionLayers.Length];

        for (int i = 0; i < triggerDetectionLayers.Length; i++)
        {
            layers[i].value = LayerMask.NameToLayer(triggerDetectionLayers[i]);
        }
    }

    private void Start()
    {
        animator.Play("Off", 0, 1);
    }

    private void Update()
    {
        if (interactables.Count == 0 && animationEnd == true)
        {
            animator.Play("Off", 0);
        }

        foreach (GameObject interactable in interactables)
        {
            if (interactable.tag == desiredObjectTag && interactables.Count > 0 && animationStart == true)
            {
                animator.Play("Active", 0);
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