using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Activator : MonoBehaviour
{
    [SerializeField] GameObject activatorObject;
    [SerializeField] string activatorAnimationName;
    [SerializeField] string activeToPlay;
    [SerializeField] string offToPlay;
    [SerializeField] float delay;

    [HideInInspector] public bool animationEnd = false;
    [HideInInspector] public bool animationStart = false;

    private Animator animator;
    private Animator objectAnimator;
    private float timer;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        objectAnimator = activatorObject.GetComponent<Animator>();
    }

    private void Start()
    {
        animator.Play(offToPlay, 0, 1);
    }

    private void Update()
    {
        if (objectAnimator.GetCurrentAnimatorStateInfo(0).IsName(activatorAnimationName) && animationStart == true)
        {
            Invoke(nameof(ActiveAnimation), delay);
            print("In");
        }

        if (!objectAnimator.GetCurrentAnimatorStateInfo(0).IsName(activatorAnimationName) && animationEnd == true)
        {
            animator.Play(offToPlay, 0);
            print("Off");
        }
    }

    private void ActiveAnimation()
    {
        if (timer < Time.time)
        {
            animator.Play(activeToPlay, 0);
            timer = Time.time + delay;
        }
    }
}
