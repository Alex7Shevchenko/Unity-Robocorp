using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CraneMovement : MonoBehaviour
{
    [SerializeField] Transform backForwardMove;
    [SerializeField] Transform leftRightMove;
    [SerializeField] Transform upDownMove;
    [SerializeField] GameObject magnetLocationMarker;
    [Space]
    [SerializeField] float speed;
    [Space]
    [SerializeField] Vector2 clampBackForwardPos;
    [SerializeField] Vector2 clampLeftRitghPos;
    [SerializeField] Vector2 clampUpDownPos;

    public bool isCraneActive;

    private void Update()
    {
        BackForwardMovement();
        LeftRightMovement();
        UpDownMovement();
        Extra();
    }

    private void BackForwardMovement()
    {
        if (isCraneActive)
        {
            if (Input.GetKey(KeyCode.A) && leftRightMove.localPosition.y > clampLeftRitghPos.x)
            {
                leftRightMove.Translate(-backForwardMove.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D) && leftRightMove.localPosition.y < clampLeftRitghPos.y)
            {
                leftRightMove.Translate(backForwardMove.forward * speed * Time.deltaTime);
            }
        }
    }

    private void LeftRightMovement()
    {
        if (isCraneActive)
        {
            if (Input.GetKey(KeyCode.W) && backForwardMove.localPosition.z > clampBackForwardPos.x)
            {
                backForwardMove.Translate(backForwardMove.right * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S) && backForwardMove.localPosition.z < clampBackForwardPos.y)
            {
                backForwardMove.Translate(-backForwardMove.right * speed * Time.deltaTime);
            }
        }
    }

    private void UpDownMovement()
    {
        if (isCraneActive)
        {
            if (Input.GetKey(KeyCode.R) && upDownMove.localPosition.z < clampUpDownPos.x)
            {
                upDownMove.Translate(-upDownMove.up * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.F) && upDownMove.localPosition.z > clampUpDownPos.y)
            {
                upDownMove.Translate(upDownMove.up * speed * Time.deltaTime);
            }
        }
    }

    private void Extra()
    {
        if (isCraneActive)
        {
            magnetLocationMarker.SetActive(true);
        }
        else
        {
            magnetLocationMarker.SetActive(false);
        }
    }
}
