using System;
using System.Threading;
using UnityEditor.Animations;
using UnityEngine;

public class Robomaker : MonoBehaviour
{
    [Header("-Robomaker- Settings")]
    [Space]
    [Tooltip("Instert the time in seconds it takes the animation to finish")]
    [SerializeField] public float animationDuration;
    [Tooltip("Instert the time in seconds the arm will reach the destination")]
    [SerializeField] public int tubeMovementSpeed;

    GameObject machineArm;
    GameObject beltAnimation;
    Transform machineArmEndPoint;
    Vector3 startOfLine, endOfLine;
    AnimatorStateInfo animatorState;

    float currentPos, nextPos, timer;
    float[] lerpStarts = new float[4];

    Levers levers;

    private void Awake()
    {
        levers = GetComponent<Levers>();
        beltAnimation = GameObject.Find("Belt_Animation");
        machineArm = GameObject.Find("Machine_Arm_Animation");
        machineArmEndPoint = gameObject.transform.Find("End_Point");
    }

    private void Start()
    {
        startOfLine = machineArm.transform.position;
        endOfLine = machineArmEndPoint.position;

        timer = 0f;

        for(int i = 0; i < lerpStarts.Length; ++i)
            lerpStarts[i] = 0;
    }

    private void Update()
    {
        StageRaycast(0, 1, 2, 3, 0, .333f, "Machine_One", "Stage_One");
        StageRaycast(1, 0, 2, 3, .333f, .667f, "Machine_Two", "Stage_Two");
        StageRaycast(2, 0, 1, 3, .667f, 1, "Machine_Three", "Stage_Three");
        StageRaycast(3, 0, 1, 2, 1, 0, "Machine_Four", "Empty");

        RoboArmMovment(0, currentPos, nextPos, "Body_Spawner", "Body_Spawner_End");
        RoboArmMovment(1, currentPos, nextPos, "Arms_Spawner", "Arms_Spawner_End");
        RoboArmMovment(2, currentPos, nextPos, "Head_Spawner", "Head_Spawner_End");
        RoboArmMovment(3, currentPos, nextPos, "Legs_Spawner", "Legs_Spawner_End");
    }

    private void StageRaycast(int rightLever, int firstWrongLever, int secondWrongLever, int thirdWrongLever, float currentPos, float nextPos, string machineName, string beltAnimation)
    {
        Ray ray = new Ray(machineArm.transform.position, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.collider.CompareTag(machineName) && levers.pressed[rightLever])
        {
            this.currentPos = currentPos;
            this.nextPos = nextPos;
            this.beltAnimation.GetComponent<Animator>().Play(beltAnimation);
        }
        if (hit.collider.CompareTag(machineName) && (levers.pressed[firstWrongLever] || levers.pressed[secondWrongLever] || levers.pressed[thirdWrongLever]))
        {
            this.currentPos = currentPos;
            this.nextPos = 0;
        }
    }

    private void RoboArmMovment(int leverNumber, float startPos, float endPos, string animationName, string animationEndingName)
    {
        animatorState = machineArm.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        if (levers.pressed[leverNumber] == true)
        {
            lerpStarts[leverNumber] = 0f;
            machineArm.GetComponent<Animator>().Play(animationName);
            timer = Time.time + animationDuration + tubeMovementSpeed;
        }

        if (animatorState.IsName(animationEndingName) && timer > Time.time)
        {
            lerpStarts[leverNumber] = Mathf.MoveTowards(lerpStarts[leverNumber], 1, 1 / (float)tubeMovementSpeed * Time.deltaTime);
            machineArm.transform.position = Vector3.Lerp(startOfLine, endOfLine, Mathf.SmoothStep(startPos, endPos, lerpStarts[leverNumber]));
        }
        else
            lerpStarts[leverNumber] = 0;
    }
}
