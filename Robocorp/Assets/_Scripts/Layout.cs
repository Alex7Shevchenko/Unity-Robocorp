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
                HasteLayout();
            else if (randomizedID.firstIDNumber == 1)
                BasicLayout();
            else
                SpearLayout();

        if (randomizedBeeper.randomState == 2)
            if (randomizedID.lastIDNumber > 7)
                SpearLayout();
            else if (randomizedID.firstIDNumber < 6)
                RogerLayout();
            else
                DrinkLayout();

        if (randomizedBeeper.randomState == 3)
            if (randomizedID.containsSpecialLetters == true && (randomizedID.firstIDNumber == 1 || randomizedID.firstIDNumber == 3 || randomizedID.firstIDNumber == 5 || randomizedID.firstIDNumber == 7 || randomizedID.firstIDNumber == 9))
                ArrowLayout();
            else if ((randomizedID.lastIDNumber + randomizedID.firstIDNumber) < 9)
                HasteLayout();
            else
                RogerLayout();

        if (randomizedBeeper.randomState == 4)
            if ((randomizedID.lastIDNumber * 2) > 11)
                BasicLayout();
            else if (randomizedID.containsSpecialLetters == false)
                SpearLayout();
            else
                ArrowLayout();

        if (randomizedBeeper.randomState == 5)
            if (randomizedID.containsSpecialLetters == true && (randomizedID.lastIDNumber == 2 || randomizedID.lastIDNumber == 4 || randomizedID.lastIDNumber == 6 || randomizedID.lastIDNumber == 8))
                RogerLayout();
            else
                HasteLayout();

        if (randomizedBeeper.randomState == 6)
            if (randomizedID.lastIDNumber == 1 || randomizedID.lastIDNumber == 3 || randomizedID.lastIDNumber == 5 || randomizedID.lastIDNumber == 7 || randomizedID.lastIDNumber == 9)
                DrinkLayout();
            else if (randomizedID.containsSpecialLetters == true && (randomizedID.firstIDNumber == 1 || randomizedID.firstIDNumber == 3 || randomizedID.firstIDNumber == 5 || randomizedID.firstIDNumber == 7 || randomizedID.firstIDNumber == 9))
                ArrowLayout();
            else if ((randomizedID.lastIDNumber + randomizedID.firstIDNumber) < 9)
                HasteLayout();
            else
                RogerLayout();

        LayoutInfo();
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

    private void LayoutInfo()
    {
        print("Beeper State: " + randomizedBeeper.randomState);
        print("First Number: " + randomizedID.firstIDNumber);
        print("Last Number: " + randomizedID.lastIDNumber);
        print("Contains Special Letters: " + randomizedID.containsSpecialLetters);
        print("Layout Name: " + layoutName);
    }
}
