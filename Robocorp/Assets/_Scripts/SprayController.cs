using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SprayController : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] SprayController[] sprayControllers = new SprayController[0];

    public int status;
    private UIText script;

    private void Start()
    {
        status = 1;
    }

    private void Update()
    {
        Presser();

        if(script != null && script.pressed) 
        {
            status = script.Status();
        }
    }

    private void Presser()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, mask))
        {
            script = hit.collider.gameObject.GetComponent<UIText>();

            if (script.pressed) return;
            if (Input.GetMouseButtonDown(0)) script.pressed = true;
            script.highlighted = true;
        }
        else
        {
            if(script == null) return;
            script.highlighted = false;
        }
    }
}
