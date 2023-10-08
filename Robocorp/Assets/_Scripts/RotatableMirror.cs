using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableMirror : MonoBehaviour
{
    [SerializeField] Transform mirrorTransform;
    [SerializeField] GameObject buttonPrompt;
    [SerializeField] Transform cameraPos;
    [SerializeField] float timeInSeconds = 2f;

    private Vector3 currentPos;
    private Vector3 nextPos;

    private float elapsedTime;
    private bool slerping, InRange;

    private void Start()
    {
        StarterSettings();
    }

    private void Update()
    {
        Rotate();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInTrigger(other);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInTrigger(other);
    }

    private void Rotate()
    {
        elapsedTime += Time.deltaTime;

        if(InRange)
        {
            buttonPrompt.SetActive(true);
            buttonPrompt.transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
            buttonPrompt.transform.LookAt(cameraPos);
        }
        else
        {
            buttonPrompt.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && slerping == false && InRange)
        {
            elapsedTime = 0;
            slerping = !slerping;
        }

        if (slerping)
        {
            var speed = elapsedTime / timeInSeconds;
            var smoothSpeed = Mathf.SmoothStep(0, 1, speed);
            mirrorTransform.eulerAngles = Vector3.Slerp(currentPos, nextPos, smoothSpeed);

            if (speed > 1)
            {
                currentPos = nextPos;
                nextPos += new Vector3(0, 0, 45);
                slerping = false;
            }
        }
    }

    private void PlayerInTrigger(Collider other)
    {
        if(other.gameObject.tag == "Player") InRange = !InRange;
    }

    private void StarterSettings()
    {
        currentPos = transform.eulerAngles;
        nextPos = transform.eulerAngles + new Vector3(0, 0, 45);
    }
}
