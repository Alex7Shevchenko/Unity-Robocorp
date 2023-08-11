using UnityEngine;

public class Targeting : MonoBehaviour
{
    [SerializeField] GameObject turretHead;
    [SerializeField] float rotationSpeed = 50f;
    [Space]
    [SerializeField] bool test;
    [SerializeField] float minRotationDegreeClamp = 0f;
    [SerializeField] float maxRotationDegreeClamp = 180f;

    GameObject player;
    Vector3 desiredLookingDirection;
    Ray ray;
    RaycastHit hit;

    //Vector3 currentLookDirection;

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(turretHead.transform.position, hit.point);
        }
    }

    private void Awake()
    {
        player = GameObject.Find("PlayerArmature/PlayerCameraRoot");
    }

    private void Update()
    {
        if (test == false)
            Targetting();
        if (test)
            TargettingTest();
    }

    private void Targetting()
    {
        if (player == null) return;

        desiredLookingDirection = player.transform.position - turretHead.transform.position;

        ray = new Ray(turretHead.transform.position, desiredLookingDirection);
        Physics.Raycast(ray, out hit);

        if (hit.collider == null) return;

        if (hit.collider.CompareTag("Player"))
        {
            //This method uses GLOBAL quaternion rotation
            //This method makes the targeting move to the target at constant speed.

            //Converts the Vector3 to Quaternion
            Quaternion targetRotation = Quaternion.LookRotation(desiredLookingDirection);

            //Clamps the "x" rotation to the value of maxPitchRotation
            float maxPitchAngle = 40f;
            Vector3 eulerRotation = targetRotation.eulerAngles;
            eulerRotation.x = Mathf.Clamp(eulerRotation.x, -maxPitchAngle, maxPitchAngle);

            //Adds the clamp to the targetRotation
            targetRotation = Quaternion.Euler(eulerRotation);

            //Moves the turret head to the desired position in a constant speed
            float step = rotationSpeed * Time.deltaTime;
            turretHead.transform.rotation = Quaternion.RotateTowards(turretHead.transform.rotation, targetRotation, step);


            //This method makes the targeting move to the target at "X" speed between two points (Not constant speed).

            //currentLookDirection = Vector3.Lerp(currentLookDirection, desiredLookingDirection, turretRotationSpeed * Time.deltaTime);
            //turretHead.transform.rotation = Quaternion.LookRotation(currentLookDirection);
        }
    }

    private void TargettingTest()
    {
        if (player == null) return;

        desiredLookingDirection = player.transform.position - turretHead.transform.position;

        ray = new Ray(turretHead.transform.position, desiredLookingDirection);
        Physics.Raycast(ray, out hit);

        if (hit.collider.CompareTag("Player"))
        {
            //This method uses LOCAL quaternion rotation 
            //This method makes the targeting move to the target at constant speed.

            //Converts the Vector3 to Quaternion
            Quaternion targetRotation = Quaternion.LookRotation(desiredLookingDirection);

            //Clamps the "x, y, z"
            float maxPitchAngle = 40f;
            Vector3 eulerRotation = targetRotation.eulerAngles;
            eulerRotation.x = Mathf.Clamp(eulerRotation.x, -maxPitchAngle, maxPitchAngle);
            eulerRotation.y = Mathf.Clamp(eulerRotation.y, minRotationDegreeClamp + transform.rotation.eulerAngles.y, maxRotationDegreeClamp + transform.rotation.eulerAngles.y);

            //Adds the clamp to the targetRotation
            targetRotation = Quaternion.Euler(eulerRotation);

            //Moves the turret head to the desired position in a constant speed
            float step = rotationSpeed * Time.deltaTime;
            turretHead.transform.rotation = Quaternion.RotateTowards(turretHead.transform.rotation, targetRotation, step);
        }
    }
}