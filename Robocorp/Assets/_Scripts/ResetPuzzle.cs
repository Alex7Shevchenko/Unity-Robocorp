using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPuzzle : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] Vector3 offset;
    [SerializeField] bool manualSettings;
    [SerializeField] GameObject[] objects = new GameObject[0];
    [SerializeField] GameObject[] mirrorObjects = new GameObject[0];

    public List<Vector3> positions = new List<Vector3>();
    public List<Quaternion> rotations = new List<Quaternion>();

    public List<Vector3> mirrorPositions = new List<Vector3>();
    public List<Quaternion> mirrorRotations = new List<Quaternion>();

    private bool inTrigger;
    private float timer;
    void Start()
    {
        if (!manualSettings)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                positions.Add(objects[i].transform.position);
                rotations.Add(objects[i].transform.rotation);
            }

            for (int i = 0;i < mirrorObjects.Length; i++)
            {
                mirrorPositions.Add(mirrorObjects[i].transform.position);
                mirrorRotations.Add(mirrorObjects[i].transform.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") inTrigger = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 2 && inTrigger)
        {
            UI.SetActive(true);
            UI.transform.position = transform.position + offset;
            UI.transform.LookAt(Camera.main.transform.position);
        }
        else
        {
            UI.SetActive(false);
        }

        StartDissolve();
    }

    private void StartDissolve()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E) && timer > 2f)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].GetComponent<DissolveEffect>().isDissolving = true;
            }
            
            gameObject.GetComponent<Animator>().Play("Pull_Lever", 0);
            timer = 0f;
            Invoke(nameof(ResetPositions), 1);
        }
    }

    private void ResetPositions()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.position = positions[i];
            objects[i].transform.rotation = rotations[i];
        }

        for (int i = 0;i < mirrorObjects.Length; i++)
        {
            mirrorObjects[i].transform.position = mirrorPositions[i];
            mirrorObjects[i].transform.rotation = mirrorRotations[i];
        }
    }
}
