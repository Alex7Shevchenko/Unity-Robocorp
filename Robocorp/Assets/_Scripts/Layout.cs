using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Layout : MonoBehaviour
{
    [SerializeField] GameObject statusIndecator;
    [SerializeField] GameObject beeper;
    [SerializeField] GameObject ID;
    [SerializeField] GameObject[] pressurePlates;
    [SerializeField] PressurePlate[] pressurePlatesScripts;

    [HideInInspector] public string layoutName;

    private RandomizedBeeper randomizedBeeper;
    private RandomizedID randomizedID;
    private Renderer matColor;
    private bool finishedPuzzle;

    public bool haste, spear, arrow, basic , roger, drink;

    private void Awake()
    {
        for (int i = 0; i < pressurePlates.Length; i++)
            pressurePlatesScripts[i] = pressurePlates[i].GetComponent<PressurePlate>();

        matColor = statusIndecator.GetComponent<Renderer>();
        randomizedBeeper = beeper.GetComponent<RandomizedBeeper>();
        randomizedID = ID.GetComponent<RandomizedID>();
    }

    private void Start()
    {
        matColor.material.color = Color.red;
    }

    private void Update()
    {
        PuzzleStatus();

        if (randomizedBeeper.randomState == 1)
            if (randomizedID.lastIDNumber == 2 || randomizedID.lastIDNumber == 4 || randomizedID.lastIDNumber == 6 || randomizedID.lastIDNumber == 8)
                print("True");
            else
                print("False");

        if (randomizedBeeper.randomState == 2)
            SpearLayout();

        if (randomizedBeeper.randomState == 3)
            ArrowLayout();

        if (randomizedBeeper.randomState == 4)
            BasicLayout();

        if (randomizedBeeper.randomState == 5)
            RogerLayout();

        if (randomizedBeeper.randomState == 6)
            DrinkLayout();
    }

    private void PuzzleStatus()
    {
        for (int i = 0; i < pressurePlates.Length; i++)
        {
            if (pressurePlatesScripts.All(scripts => scripts.isActivated == true))
                finishedPuzzle = true;
            else
                finishedPuzzle = false;
        }

        if (finishedPuzzle)
            matColor.material.color = Color.green;
        else
            matColor.material.color = Color.red;
    }

    private void HasteLayout()
    {
        LayoutType("Red Box", "Yellow Box", "Green Box", "Purple Box", "Blue Box", "Haste");
    }
    private void SpearLayout()
    {
        LayoutType("Red Box", "Green Box", "Blue Box", "Purple Box", "Yellow Box", "Spear");
    }
    private void ArrowLayout()
    {
        LayoutType("Purple Box", "Blue Box", "Green Box", "Yellow Box", "Red Box", "Arrow");
    }
    private void BasicLayout()
    {
        LayoutType("Purple Box", "Green Box", "Yellow Box", "Blue Box", "Red Box", "Basic");
    }
    private void RogerLayout()
    {
        LayoutType("Blue Box", "Purple Box", "Red Box", "Green Box", "Yellow Box", "Roger");
    }
    private void DrinkLayout()
    {
        LayoutType("Blue Box", "Green Box", "Purple Box", "Yellow Box", "Red Box", "Drink");
    }
    private void LayoutType(string A, string B, string C, string D, string E, string layoutName)
    {
        pressurePlatesScripts[0].desiredObjectTag = A;
        pressurePlatesScripts[1].desiredObjectTag = B;
        pressurePlatesScripts[2].desiredObjectTag = C;
        pressurePlatesScripts[3].desiredObjectTag = D;
        pressurePlatesScripts[4].desiredObjectTag = E;
        this.layoutName = layoutName;
    }
}
