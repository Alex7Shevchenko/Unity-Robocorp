using UnityEngine;

public class Activator : MonoBehaviour
{
    [Header("Activator Settings")]
    [Space]
    [SerializeField] GameObject activatorObject;
    [SerializeField] string boolName;
    [SerializeField] float animationStartDelay;
    [SerializeField] Laser[] lasers = new Laser[0];

    public bool activateByMultyLasers;

    private Animator animator;
    private float timer;
    private Laser laser;
    private PressurePlate pressurePlate;
    private PlayerTrigger playerTrigger;
    private Keypad keypad;
    bool activateByLaser, activateByPlate, activeByTrigger, activateByCode;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        if(activatorObject != null)
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

            if (activatorObject.GetComponent<Keypad>() != null)
            {
                keypad = activatorObject.GetComponent<Keypad>();
                activateByCode = true;
            }
        }
    }

    private void Update()
    {
        ActivateByPlate();
        ActivateByLaser();
        ActivateByTrigger();
        ActivateByCode();
        ActivateBeMultipleLasers();
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
                if (timer < Time.time)
                {
                    timer = Time.time + animationStartDelay;
                    Invoke(nameof(AnimationDelay), animationStartDelay);
                }
            }
            else
            {
                CancelInvoke();
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
                if (timer < Time.time)
                {
                    timer = Time.time + animationStartDelay;
                    Invoke(nameof(AnimationDelay), animationStartDelay);
                }              
            }
            else
            {
                CancelInvoke();
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
                if (timer < Time.time)
                {
                    timer = Time.time + animationStartDelay;
                    Invoke(nameof(AnimationDelay), animationStartDelay);
                }
            }
            else
            {
                CancelInvoke();
                animator.SetBool(boolName, false);
            }
        }
    }

    private void ActivateByCode()
    {
        if (activateByCode)
        {
            if (keypad.isActivated)
            {
                if (timer < Time.time)
                {
                    timer = Time.time + animationStartDelay;
                    Invoke(nameof(AnimationDelay), animationStartDelay);
                }
            }
            else
            {
                CancelInvoke();
                animator.SetBool(boolName, false);
            }
        }
    }

    private void ActivateBeMultipleLasers()
    {
        if (activateByMultyLasers)
        {
            if(LasersList() == true)
            {
                if (timer < Time.time)
                {
                    timer = Time.time + animationStartDelay;
                    Invoke(nameof(AnimationDelay), animationStartDelay);
                }
            }
            else
            {
                CancelInvoke();
                animator.SetBool(boolName, false);
            }
        }
    }

    bool LasersList()
    {
        foreach (var laser in lasers)
        {
            if(laser.isActivated == false)
            {
                return false;
            }
        }

        return true;
    }
}
