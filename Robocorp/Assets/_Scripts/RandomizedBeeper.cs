using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedBeeper : MonoBehaviour
{
    [HideInInspector] public int randomState;

    private Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        randomState = Random.Range(1, 6);
    }

    private void Update()
    {
        if (randomState == 1)
            animator.Play("Color_Beep_A", 0);
        else if (randomState == 2)
            animator.Play("Color_Beep_B", 0);
        else if (randomState == 3)
            animator.Play("Color_Beep_C", 0);
        else if (randomState == 4)
            animator.Play("Color_Beep_D", 0);
        else if (randomState == 5)
            animator.Play("Color_Beep_E", 0);
        else
            animator.Play("Color_Beep_F", 0);
    }
}
