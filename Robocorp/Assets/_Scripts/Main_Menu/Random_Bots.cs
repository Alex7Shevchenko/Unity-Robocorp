using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Bots : MonoBehaviour
{
    [SerializeField] SprayController sprayController;
    [SerializeField] int delaySeconds;
    [SerializeField] int animationLength;
    [SerializeField] GameObject[] plates = new GameObject[0];
    [SerializeField] Material[] platesMaterial = new Material[0];

    private float timer;

    private void Awake()
    {
        timer = animationLength - delaySeconds;

        PlatesRandomizerOnStart();
    }

    private void Update()
    {
        PlatesRandomizerLoop();
    }

    private void PlatesRandomizerLoop()
    {
        timer += Time.deltaTime;

        if (timer >= animationLength)
        {
            int randomColor = Random.Range(0, platesMaterial.Length);

            for (int i = 1; i < plates.Length; i++)
            {
                int randomOnOff = Random.Range(0, 2);

                if (randomOnOff == 0) plates[i].SetActive(false);
                else plates[i].SetActive(true);

                if(sprayController.status > 1 && sprayController.status < 9)
                {
                    plates[i].GetComponent<Renderer>().material = platesMaterial[sprayController.status - 2];
                }
                else
                {
                    plates[i].GetComponent<Renderer>().material = platesMaterial[randomColor];
                }
            }

            if (sprayController.status > 1 && sprayController.status < 9)
            {
                plates[0].GetComponent<Renderer>().material = platesMaterial[sprayController.status - 2];
            }
            else
            {
                plates[0].GetComponent<Renderer>().material = platesMaterial[randomColor];
            }

            timer = 0;
        }
    }

    private void PlatesRandomizerOnStart()
    {
        int randomColor = Random.Range(0, platesMaterial.Length);

        for (int i = 1; i < plates.Length; i++)
        {
            int randomOnOff = Random.Range(0, 2);

            if (randomOnOff == 0) plates[i].SetActive(false);
            else plates[i].SetActive(true);

            plates[i].GetComponent<Renderer>().material = platesMaterial[randomColor];
        }
        plates[0].GetComponent<Renderer>().material = platesMaterial[randomColor];
    }
}