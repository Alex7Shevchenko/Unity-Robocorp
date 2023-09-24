using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomizeRobot : MonoBehaviour
{
    public TMP_Text IDNumber;
    [SerializeField] int IDLength;
    [Space]
    [SerializeField] GameObject[] armorPanels = new GameObject[0];
    [SerializeField] int[] randomNumbers = new int[0];
    [Space]
    [SerializeField] Material[] materials = new Material[7];

    [SerializeField] GameObject[] markers = new GameObject[0];
    [SerializeField] Material redMark;

    public int timesMarked = 0;

    private void Awake()
    {
        RandomizeArmorColor();
        RandomizeArmorPanels();
        IDNumber.text = RandomizeID();
    }

    private void Update()
    {
        MarkingSystem();
    }

    void MarkingSystem()
    {
        if (timesMarked == 1 && markers[0].GetComponent<Renderer>().material != redMark) 
        {
            markers[0].GetComponent<Renderer>().material = redMark;
        }
        if (timesMarked == 2 && markers[1].GetComponent<Renderer>().material != redMark)
        {
            markers[1].GetComponent<Renderer>().material = redMark;
        }
        if (timesMarked == 3 && markers[2].GetComponent<Renderer>().material != redMark)
        {
            markers[2].GetComponent<Renderer>().material = redMark;
        }
    }

    void RandomizeArmorPanels()
    {
        for (int i = 1; i < armorPanels.Length; i++)
        {
            randomNumbers[i] = Random.Range(0, 2);
            if (randomNumbers[i] == 1)
            {
                armorPanels[i].SetActive(false);
            }
            else
            {
                armorPanels[i].SetActive(true);
            }
        }
    }

    void RandomizeArmorColor() 
    {
        int armorColorType = Random.Range(0, 7);

        for(int i = 0;i < armorPanels.Length; i++)
        {
            armorPanels[i].GetComponent<Renderer>().material = materials[armorColorType];
        }
    }

    public string RandomizeID()
    {
        var myNums = "0123456789";
        var myNumsExluded = "123456789";
        var randomNums = new char[IDLength];

        for (int i = 1; i < IDLength; i++)
        {
            randomNums[i] = myNums[Random.Range(0, myNums.Length)];
        }
        randomNums[0] = myNumsExluded[Random.Range(1, myNumsExluded.Length)];

        return new string(randomNums);
    }
}
