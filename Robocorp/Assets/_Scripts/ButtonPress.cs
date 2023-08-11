using Unity.Mathematics;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    [SerializeField] float buttonCooldown = 5f;
    [SerializeField] Vector3 triggerCenterPosition = new Vector3(0,.5f,-.5f);
    [SerializeField] Vector3 triggerZone = new Vector3(.5f,.5f,.8f);
    [SerializeField] LayerMask detectionMask;
    [SerializeField] GameObject buttonPrompt;

    [HideInInspector] public bool activated;
    float timer;
    Transform buttonPromptPosition;

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(triggerCenterPosition, triggerZone * 2);
    }
    
    private void Awake()
    {
        buttonPromptPosition = gameObject.transform.Find("Botton_Prompot_Position");
    }

    private void Start()
    {
        timer = 0f;
        buttonPrompt.SetActive(false);
        ButtonPromptLocation();
    }

    private void Update()
    {
        ButtonInteraction();
    }
    
    private void ButtonInteraction()
    {
        Vector3 centerPoint = transform.TransformPoint(triggerCenterPosition);
        bool playerInRange = Physics.CheckBox(centerPoint, triggerZone, transform.rotation, detectionMask);

        if (playerInRange && Time.time > timer)
        {
            buttonPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Invoke(nameof(ResetAnimation), buttonCooldown);
                buttonPrompt.SetActive(false);
                timer = Time.time + buttonCooldown;
                gameObject.GetComponent<Animator>().Play("Pull_Lever");
                activated = true;
            }
        }
        else
        {
            activated = false;
            buttonPrompt.SetActive(false);
        }
    }

    private void ButtonPromptLocation()
    {
        buttonPrompt.transform.position = buttonPromptPosition.position;
        buttonPrompt.transform.rotation = buttonPromptPosition.rotation;
    }

    private void ResetAnimation()
    {
        gameObject.GetComponent<Animator>().Play("Empty");
    }
}
