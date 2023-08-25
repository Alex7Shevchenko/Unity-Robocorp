using UnityEngine;

public class Activator : MonoBehaviour
{
    [Header("How To Activate")]
    [Space]
    [SerializeField] bool activateByPressurePlate;
    [SerializeField] bool activateByLaser;
    [SerializeField] bool activateByPuzzleComplition;
    [Header("Activator Settings")]
    [Space]
    [SerializeField] GameObject activatorObject;
    [SerializeField] string activateAnimationName;
    [SerializeField] string closeAnimationName;
    [SerializeField] float animationStartDelay;

    [HideInInspector] public bool animationEnd = false;
    [HideInInspector] public bool animationStart = false;

    private Animator animator;
    private Animator objectAnimator;
    private float timer;
    private Laser laser;
    private PressurePlate pressurePlate;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        objectAnimator = activatorObject.GetComponent<Animator>();
    }

    private void Start()
    {
        animator.Play(closeAnimationName, 0, 1);

        if (activateByPressurePlate)
        {
            activateByLaser = false;
            pressurePlate = activatorObject.GetComponent<PressurePlate>();
        }

        if (activateByLaser)
            laser = activatorObject.GetComponent<Laser>();
    }

    private void Update()
    {
        if (activateByPressurePlate)
            ActivateByAnimation();

        if (activateByLaser)
            ActivateByLaser();
    }

    private void AnimationDelay()
    {
        if (timer < Time.time)
        {
            animator.Play(activateAnimationName, 0);
            timer = Time.time + animationStartDelay;
        }
    }

    private void ActivateByAnimation()
    {
        if (pressurePlate.isActivated && animationStart)
            Invoke(nameof(AnimationDelay), animationStartDelay);

        if (!pressurePlate.isActivated && animationEnd)
            animator.Play(closeAnimationName, 0);
    }

    private void ActivateByLaser()
    {
        if (laser.isActivated && animationStart)
            Invoke(nameof(AnimationDelay), animationStartDelay);

        if (!laser.isActivated && animationEnd)
            animator.Play(closeAnimationName, 0);
    }
}
