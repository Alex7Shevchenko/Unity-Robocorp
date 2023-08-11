using UnityEngine;

public class Levers : MonoBehaviour
{

    [SerializeField] GameObject[] levers;
    [SerializeField] GameObject[] buttonPrompts;
    [SerializeField] Transform[] buttonPromptPositions;
    [Header("Buttons Settings")]
    [Space]
    [SerializeField] Vector3 triggerCenterPosition = new Vector3(0, .5f, -.5f);
    [SerializeField] Vector3 triggerZone = new Vector3(.25f, .5f, .8f);
    [SerializeField] LayerMask detectionMask;

    [HideInInspector] public bool[] pressed = new bool[4];
    float buttonCooldown;
    float timer;

    Robomaker robomaker;

    private void OnDrawGizmos()
    {
        foreach(var lever in levers)        
            Gizmo(lever);       
    }

    private void Awake()
    {
        robomaker = GetComponent<Robomaker>();
    }

    private void Start()
    {
        for(int i = 0; i < buttonPromptPositions.Length; i++)
            ButtonPromptLocation(buttonPrompts[i], buttonPromptPositions[i]);

        timer = 0f;

        buttonCooldown = robomaker.animationDuration + robomaker.tubeMovementSpeed;
    }

    private void Update()
    {
        for(int i = 0; i < levers.Length; i++)        
            ButtonInteraction(levers[i], buttonPrompts[i], ref pressed[i]);
    }

    private void ButtonInteraction(GameObject lever, GameObject buttonPrompt, ref bool activated)
    {
        AnimatorStateInfo animatorState = lever.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        Vector3 centerPoint = lever.transform.TransformPoint(triggerCenterPosition);
        bool playerInRange = Physics.CheckBox(centerPoint, triggerZone, lever.transform.rotation, detectionMask);

        if (playerInRange && Time.time > timer)
        {
            buttonPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Invoke(nameof(ResetAnimation), buttonCooldown);              
                buttonPrompt.SetActive(false);
                timer = Time.time + buttonCooldown;
                lever.GetComponent<Animator>().Play("Pull_Lever");
                activated = true;
            }
        }
        else
        {
            activated = false;
            buttonPrompt.SetActive(false);
        }
    }

    private void ButtonPromptLocation(GameObject buttonPrompt, Transform promptPosition)
    {
        buttonPrompt.SetActive(false);
        buttonPrompt.transform.position = promptPosition.position;
        buttonPrompt.transform.rotation = promptPosition.rotation;
    }

    private void ResetAnimation()
    {
        foreach(var lever in levers)
            lever.GetComponent<Animator>().Play("Empty");
    }

    private void Gizmo(GameObject lever)
    {
        Gizmos.matrix = lever.transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(triggerCenterPosition, triggerZone * 2);
    }
}
