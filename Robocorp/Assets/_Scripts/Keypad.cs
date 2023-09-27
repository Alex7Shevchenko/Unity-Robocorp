using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class Keypad : MonoBehaviour
{
    [SerializeField] TMP_Text screenCode;
    [SerializeField] TMP_Text screenCodeRemoved;
    [SerializeField] TMP_Text errorMessage;
    [SerializeField] TMP_Text accessMesage;
    [SerializeField] float messageDisplayTimeSeconds;
    [SerializeField] LayerMask numberLayers;
    [SerializeField] LayerMask enterLayer;
    [SerializeField] PuzzpleKeyAndInfo puzzleInfo;

    public string code;
    public bool isActivated;
    private int codeLength;

    private void Start()
    {
        Invoke(nameof(SetCode), 0.1f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                hit.collider.gameObject.TryGetComponent<Renderer>(out Renderer renderer);
                if(renderer != null)
                {
                    renderer.materials[1].color = Color.green;
                    StartCoroutine(ResetMaterial(renderer.materials[1]));
                }

                if (screenCode.text.Length < codeLength && hit.collider.gameObject.layer == 21)
                {
                    screenCode.text += hit.collider.gameObject.name;
                }
                else if (!screenCode.text.Contains(code) && hit.collider.gameObject.layer == 22)
                {
                    screenCode.enabled = false;
                    screenCodeRemoved.enabled = false;
                    errorMessage.enabled = true;
                    Invoke(nameof(SetDefaults), messageDisplayTimeSeconds);
                }
                else if (screenCode.text.Contains(code) && hit.collider.gameObject.layer == 22)
                {
                    screenCode.enabled = false;
                    screenCodeRemoved.enabled = false;
                    accessMesage.enabled = true;
                    isActivated = true;
                }
            }
        }

        if (screenCode.text.Length == 0)
        {
            screenCodeRemoved.text = "****";
        }
        else if (screenCode.text.Length == 1)
        {
            screenCodeRemoved.text = "***";
        }
        else if (screenCode.text.Length == 2)
        {
            screenCodeRemoved.text = "**";
        }
        else if (screenCode.text.Length == 3)
        {
            screenCodeRemoved.text = "*";
        }
        else
        {
            screenCodeRemoved.text = string.Empty;
        }
    }

    private void SetCode()
    {
        code = puzzleInfo.keypadCode;
        codeLength = code.Length;
    }

    private void SetDefaults()
    {
        screenCode.enabled = true;
        screenCodeRemoved.enabled = true;
        errorMessage.enabled = false;
        screenCode.text = string.Empty;
    }

    IEnumerator ResetMaterial(Material material)
    {
        yield return new WaitForSeconds(.2f);

        material.color = new Color32(0,90,255,255);
    }
}
