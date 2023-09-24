using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RandomizeCores : MonoBehaviour
{
    [SerializeField] GameObject[] cores = new GameObject[3];
    [SerializeField] Transform[] positions = new Transform[3];
    public GameObject[] activeCores = new GameObject[3];

    public int[] coresActiveRandomizer = new int[3];
    public int[] coresColorRandomizer = new int[3];
    public int numberOfActiveCores;

    private void Awake()
    {
        RandomStateSelector();
        numberOfActiveCores = 0;
        RandomizedCores();
    }

    void RandomStateSelector()
    {
        for (int i = 0; i < coresActiveRandomizer.Length; i++)
        {
            coresActiveRandomizer[i] = Random.Range(0, 3);
            coresColorRandomizer[i] = Random.Range(1, 4);
        }
    }

    void CoreStatus(int core, int coreColor, Transform position, int index)
    {
        if(core != 0 && coreColor == 1)
        {
            activeCores[index] = Instantiate(cores[0], position);
            numberOfActiveCores++;
        }
        else if(core != 0 && coreColor == 2)
        {
            activeCores[index] = Instantiate(cores[1], position);
            numberOfActiveCores++;
        }
        else if(core != 0 && coreColor == 3)
        {
            activeCores[index] = Instantiate(cores[2], position);
            numberOfActiveCores++;
        }
    }

    void RandomizedCores() 
    {
        CoreStatus(coresActiveRandomizer[0], coresColorRandomizer[0], positions[0], 0);
        CoreStatus(coresActiveRandomizer[1], coresColorRandomizer[1], positions[1], 1);
        CoreStatus(coresActiveRandomizer[2], coresColorRandomizer[2], positions[2], 2);
    }
}
