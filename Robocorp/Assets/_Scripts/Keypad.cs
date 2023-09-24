using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    [SerializeField] TMP_Text screenCode;
    [SerializeField] TMP_Text screenCodeRemoved;
    [SerializeField] float messageDisplayTimeSeconds;
    [SerializeField] LayerMask numberLayers;
    [SerializeField] LayerMask enterLayer;
    [SerializeField] PuzzpleKeyAndInfo puzzleInfo;

    public string code;
    private float defaultFontSize;
    private Color defaultColor;
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
                print(hit.collider.gameObject.name);
                if (screenCode.text.Length < codeLength && hit.collider.gameObject.layer == 21)
                {
                    screenCode.text += hit.collider.gameObject.name;
                }
                else if (!screenCode.text.Contains(code) && hit.collider.gameObject.layer == 22)
                {
                    screenCode.text = string.Empty;
                    print("false");
                }
                else if (screenCode.text.Contains(code) && hit.collider.gameObject.layer == 22)
                {
                    print("Open");
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
        
    }
}
