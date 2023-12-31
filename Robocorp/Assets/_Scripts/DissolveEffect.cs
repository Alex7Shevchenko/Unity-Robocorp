using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] Material[] dissolveMaterials = new Material[0];
    [SerializeField] GameObject staticTexture, dissolveTexture;
    public float desiredDuration;

    public float lerpStatus;
    public float elapsedTime;

    [HideInInspector] public bool isDissolving;

    private void Start()
    {
        GetShaderParameters();
    }

    private void Update()
    {
        Dissolve();
    }

    private void GetShaderParameters()
    {
        for(int i = 0;i < dissolveMaterials.Length; i++)
        {
            if (dissolveMaterials[i] == null)
            {
                dissolveMaterials[i] = dissolveTexture.GetComponent<MeshRenderer>().materials[i];
            }           
        }
    }

    private void Dissolve()
    {
        if (isDissolving)
        {
            staticTexture.SetActive(false);
            dissolveTexture.SetActive(true);
            elapsedTime += Time.deltaTime;
            if (lerpStatus >= 1f) 
            { 
                isDissolving = false;
            }
        }
        else if(isDissolving == false && lerpStatus > -1f)
        {
            CancelInvoke(nameof(ReturnToNormal));
            elapsedTime -= Time.deltaTime;
            Invoke(nameof(ReturnToNormal), desiredDuration - 0.95f);
        }

        float precentageComplete = elapsedTime / desiredDuration;
        lerpStatus = Mathf.Lerp(-1f, 1f, precentageComplete);

        for (int i = 0; i < dissolveMaterials.Length; i++)
        {
            if (dissolveMaterials[i] != null)
            {
                dissolveMaterials[i].SetFloat("_Lerp", lerpStatus);
            }
        }
    }

    private void ReturnToNormal()
    {
        staticTexture.SetActive(true);
        dissolveTexture.SetActive(false);
    }
}
