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
            Quaternion targetRotation = Quaternion.LookRotation(desiredLookingDirection);

            float maxPitchAngle = 40f;
            Vector3 eulerRotation = targetRotation.eulerAngles;
            eulerRotation.x = Mathf.Clamp(eulerRotation.x, -maxPitchAngle, maxPitchAngle);

            targetRotation = Quaternion.Euler(eulerRotation);

            float fixedSpeed = rotationSpeed * Time.deltaTime;
            turretHead.transform.rotation = Quaternion.RotateTowards(turretHead.transform.rotation, targetRotation, fixedSpeed);
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
            Quaternion targetRotation = Quaternion.LookRotation(desiredLookingDirection);

            float yLocal = Mathf.Repeat(targetRotation.eulerAngles.y, 5);

            float maxPitchAngle = 40f;
            Vector3 eulerRotation = targetRotation.eulerAngles;
            eulerRotation.x = Mathf.Clamp(eulerRotation.x, -maxPitchAngle, maxPitchAngle);
            eulerRotation.y = Mathf.Clamp(eulerRotation.y, minRotationDegreeClamp, maxRotationDegreeClamp);

            targetRotation = Quaternion.Euler(eulerRotation);

            float step = rotationSpeed * Time.deltaTime;
            turretHead.transform.rotation = Quaternion.RotateTowards(turretHead.transform.rotation, targetRotation, step);
        }
    }
}