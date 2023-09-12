using UnityEngine;

public class Activator : MonoBehaviour
{
    [Header("Activator Settings")]
    [Space]
    [SerializeField] GameObject activatorObject;
    [SerializeField] string boolName;
    [SerializeField] float animationStartDelay;

    private Animator animator;
    private float timer;
    private Laser laser;
    private PressurePlate pressurePlate;
    private PlayerTrigger playerTrigger;
    bool activateByLaser, activateByPlate, activeByTrigger;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        if (activatorObject.GetComponent<PressurePlate>() != null)
        {
            pressurePlate = activatorObject.GetComponent<PressurePlate>();
            activateByPlate = true;
        }

        if (activatorObject.GetComponent<Laser>() != null)
        {
            laser = activatorObject.GetComponent<Laser>();
            activateByLaser = true;
        }

        if (activatorObject.GetComponent<PlayerTrigger>() != null)
        {
            playerTrigger = activatorObject.GetComponent<PlayerTrigger>();
            activeByTrigger = true;
        }
    }

    private void Update()
    {
        ActivateByPlate();
        ActivateByLaser();
        ActivateByTrigger();
    }

    private void AnimationDelay()
    {
        animator.SetBool(boolName, true);
    }

    private void ActivateByPlate()
    {
        if (activateByPlate)
        {
            if (pressurePlate.isActivated)
            {
                Invoke(nameof(AnimationDelay), animationStartDelay);
            }
            else
            {
                animator.SetBool(boolName, false);
            }
        }
    }

    private void ActivateByLaser()
    {
        if (activateByLaser)
        {
            if (laser.isActivated)
            {
                Invoke(nameof(AnimationDelay), animationStartDelay);
            }
            else
            {
                animator.SetBool(boolName, false); ;
            }
        }
    }

    private void ActivateByTrigger()
    {
        if (activeByTrigger)
        {
            if (playerTrigger.isActivated)
            {
                Invoke(nameof(AnimationDelay), animationStartDelay);
            }
            else
            {
                animator.SetBool(boolName, false);
            }
        }
    }
}
