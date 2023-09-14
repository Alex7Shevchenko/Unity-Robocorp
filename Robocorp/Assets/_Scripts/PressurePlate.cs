using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject plate;
    [SerializeField] public string desiredObjectTag;
    [SerializeField] string[] triggerDetectionLayers;
    [SerializeField] List<GameObject> interactables;

    [HideInInspector] public bool animationEnd = false;
    [HideInInspector] public bool animationStart = false;
    [HideInInspector] public bool isActivated = false;

    LayerMask[] layers;
    Animator animator;
    Material material;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        layers = new LayerMask[triggerDetectionLayers.Length];
        material = plate.GetComponent<Renderer>().material;

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
        if (desiredObjectTag == "Red Box")
            material.color = Color.red;
        if (desiredObjectTag == "Yellow Box")
            material.color = Color.yellow;
        if (desiredObjectTag == "Green Box")
            material.color = Color.green;
        if (desiredObjectTag == "Blue Box")
            material.color = Color.blue;
        if (desiredObjectTag == "Purple Box")
            material.color = new Color32(121, 0, 255, 255);

        if (interactables.Count == 0 && animationEnd == true)
        {
            animator.Play("Off", 0);
            isActivated = false;
        }

        foreach (GameObject interactable in interactables)
        {
            if (interactable.tag == desiredObjectTag && interactables.Count > 0 && animationStart == true)
            {
                animator.Play("Active", 0);
                isActivated = true;
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