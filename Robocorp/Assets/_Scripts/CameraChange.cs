using UnityEngine;
using Cinemachine;
using StarterAssets;
using Unity.Burst.CompilerServices;
using System.Security.Permissions;
using System;

public class CameraChange : MonoBehaviour
{
    [SerializeField] GameObject mainCamVirtualCamera;
    [SerializeField] GameObject player;
    [SerializeField] CinemachineVirtualCamera cinemachineBrain;
    [SerializeField] GameObject interactButtonPrompt;
    [SerializeField] Vector3 offset;
    [Space]
    [SerializeField] bool craneControl;
    [SerializeField] GameObject crane;
    [Space]
    [SerializeField] bool mouseControl;
    [SerializeField] GameObject[] texts = new GameObject[0];
    [SerializeField] bool ChangeCinimachinePos;
    [SerializeField] Transform newPos;
    [SerializeField] Transform defaultPos;

    PhysicalPlayer physicalPlayer;
    public float timer;
    public bool isPressed;
    bool inRange;
    int index, indexMax;

    private void Start()
    {
        physicalPlayer = player.GetComponent<PhysicalPlayer>();
        indexMax = texts.Length - 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == player.tag)
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == player.tag)
        {
            inRange = false;
            isPressed = false;
        }
    }

    private void Update()
    {
        CinemachinePrioriryChange();
    }

    void CinemachinePrioriryChange()
    {
        if(inRange)
        {
            if(!isPressed)
            {
                interactButtonPrompt.SetActive(true);
            }
            else
            {
                interactButtonPrompt.SetActive(false);
            }

            interactButtonPrompt.transform.position = transform.position + offset;
            interactButtonPrompt.transform.LookAt(mainCamVirtualCamera.transform.position);

            if(Input.GetKeyDown(KeyCode.E))
            {
                isPressed = !isPressed;
                physicalPlayer.playerInUI = !physicalPlayer.playerInUI;
            }
        }
        else
        {
            isPressed = false;
            interactButtonPrompt.SetActive(false);
        }

        if (isPressed && physicalPlayer.playerInUI)
        {
            cinemachineBrain.Priority = 11;
            player.GetComponent<PlayerHoldDrop>().enabled = false;
            player.GetComponent<StarterAssetsInputs>().move = new Vector2(0,0);
            player.GetComponent<StarterAssetsInputs>().enabled = false;

            if (ChangeCinimachinePos)
            {
                Invoke(nameof(ChangeCinemachinePosition), timer);
            }

            if (craneControl)
            {
                crane.GetComponent<CraneMovement>().isCraneActive = true;
            }

            if (mouseControl)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.gameObject.layer == 19 && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        var animator = hit.collider.GetComponent<Animator>();
                        animator.Play("ButtonPressed", 0, 0);
                        if(index > 0)
                        {
                            texts[index].SetActive(false);
                            index--;
                            texts[index].SetActive(true);
                        }
                    }
                    else if (hit.collider.gameObject.layer == 20 && Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        var animator = hit.collider.GetComponent<Animator>();
                        animator.Play("ButtonPressed", 0, 0);
                        if (index < indexMax)
                        {
                            texts[index].SetActive(false);
                            index++;
                            texts[index].SetActive(true);
                        }
                    }
                }
            }
        }
        else
        {
            if(timer < 1)
            {
                timer += Time.deltaTime;
            }

            if (ChangeCinimachinePos)
            {
                CancelInvoke(nameof(ChangeCinemachinePosition));
                cinemachineBrain.transform.position = defaultPos.position;
                cinemachineBrain.transform.rotation = defaultPos.rotation;
            }
            cinemachineBrain.Priority = 5;
            player.GetComponent<PlayerHoldDrop>().enabled = true;
            player.GetComponent<StarterAssetsInputs>().enabled = true;

            if (craneControl)
            {
                crane.GetComponent<CraneMovement>().isCraneActive = false;
            }
            else if (mouseControl && physicalPlayer.playerInUI == false)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void ChangeCinemachinePosition()
    {
        cinemachineBrain.transform.position = newPos.position;
        cinemachineBrain.transform.rotation = newPos.rotation;
        timer = 0;
    }
}
