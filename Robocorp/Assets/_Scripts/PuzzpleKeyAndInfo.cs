using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class PuzzpleKeyAndInfo : MonoBehaviour
{
    [SerializeField] RandomizedBeeper randomizedBeeper;
    [SerializeField] RandomizedID randomizedID;
    [SerializeField] RandomizeCores randomizeCores;
    [SerializeField] RandomizeRobot randomizeRobot;

    [Header("Puzzle Debug")]
    [SerializeField] int beeperCode;
    [SerializeField] int RBCode;
    [SerializeField] int firstIDNumber;
    [SerializeField] int lastIDNumber;
    [SerializeField] int numberOfCores;
    [SerializeField] bool containsSpecialLetters;
    [SerializeField] int redCores;
    [SerializeField] int blueCores;
    [SerializeField] int greenCores;
    public string keypadCode;
    [SerializeField] string[] coreColors = new string[3];

    [Header("Puzzle Settings")]
    [SerializeField] GameObject[] lasers = new GameObject[4];

    string RBCodeStringed;

    private void Start()
    {
        RandomizedPuzzleInfo();
        Rules();

        print(beeperCode);
    }

    private void RandomizedPuzzleInfo()
    {
        int.TryParse(randomizeRobot.IDNumber.text, out RBCode);
        RBCodeStringed = RBCode.ToString();
        beeperCode = randomizedBeeper.randomState;
        firstIDNumber = randomizedID.firstIDNumber;
        lastIDNumber = randomizedID.lastIDNumber;
        numberOfCores = randomizeCores.numberOfActiveCores;
        containsSpecialLetters = randomizedID.containsSpecialLetters;

        for (int i = 0; i < coreColors.Length; i++)
        {
            if (randomizeCores.activeCores[i] != null)
            {
                coreColors[i] = randomizeCores.activeCores[i].tag;
            }
            else
            {
                coreColors[i] = "------";
            }
        }

        foreach (var coreColor in coreColors)
        {
            if(coreColor == "Red Core")
            {
                redCores++;
            }
            else if (coreColor == "Blue Core")
            {
                blueCores++;
            }
            else if (coreColor == "Green Core")
            {
                greenCores++;
            }
        }
    }

    private void Rules()
    {
        CodeOne();
        CodeTwo();
        CodeThree();
        CodeFour();
        CodeFive();
        CodeSix();
    }

    private void PuzzleSetup()
    {
        LaserColor(0, 1, 2, 3, "Hades"); //Hades
        LaserColor(3, 0, 1, 2, "Roger"); // Roger
        LaserColor(2, 3, 0, 1, "Haste"); // Haste
        LaserColor(1, 2, 3, 0, "Spear"); // Spear
        LaserColor(0, 3, 1, 2, "Arrow"); // Arrow
        LaserColor(3, 1, 2, 0, "Admin"); // Admin
    }

    private void LaserColor(int index0, int index1, int index2, int index3, string layoutName)
    {
        lasers[index0].GetComponent<Laser>().isYellow = true;
        lasers[index1].GetComponent<Laser>().isGreen = true;
        lasers[index2].GetComponent<Laser>().isBlue = true;
        lasers[index3].GetComponent<Laser>().isPurple = true;
        print(layoutName);
    }

    void CodeOne()
    {
        if(beeperCode == 1)
        {
            if (numberOfCores > 2 && !containsSpecialLetters)
            {
                //LaserColor(1, 2, 3, 0, "Spear"); // Spear
                keypadCode = "2486";
            }
            else if ((lastIDNumber + firstIDNumber) > 7 && redCores > 0)
            {
                //LaserColor(2, 3, 0, 1, "Haste"); // Haste
                keypadCode = "9768";
            }
            else if (numberOfCores == 1)
            {
                //LaserColor(3, 0, 1, 2, "Roger"); // Roger
                keypadCode = "8256";
            }
            else
            {
                //LaserColor(0, 3, 1, 2, "Arrow"); // Arrow
                keypadCode = "1587";
            }
        }
    }

    void CodeTwo()
    {
        if(beeperCode == 2)
        {
            if (numberOfCores == 2)
            {
                //LaserColor(3, 1, 2, 0, "Admin"); // Admin
                keypadCode = "1208";
            }
            else if (containsSpecialLetters == true && blueCores > 0)
            {
                //LaserColor(0, 1, 2, 3, "Hades"); //Hades
                keypadCode = "6877";
            }
            else if (RBCode > 3085)
            {
                //LaserColor(2, 3, 0, 1, "Haste"); // Haste
                keypadCode = "4920";
            }
            else
            {
                //LaserColor(1, 2, 3, 0, "Spear"); // Spear
                keypadCode = "8264";
            }
        }
    }

    void CodeThree()
    {
        if (beeperCode == 3)
        {
            if((firstIDNumber == 1 || firstIDNumber == 3 || firstIDNumber == 5 || firstIDNumber == 7 || firstIDNumber == 9) && numberOfCores == 3)
            {
                //LaserColor(0, 3, 1, 2, "Arrow"); // Arrow
                keypadCode = "0918";
            }
            else if(numberOfCores == 0)
            {
                //LaserColor(0, 1, 2, 3, "Hades"); //Hades
                keypadCode = "2962";
            }
            else if(greenCores == 0)
            {
                //LaserColor(3, 1, 2, 0, "Admin"); // Admin
                keypadCode = "9797";
            }
            else // refered to code 5
            {
                if (RBCodeStringed.Contains("5") && (lastIDNumber == 1 || lastIDNumber == 3 || lastIDNumber == 5 || lastIDNumber == 7 || lastIDNumber == 9))
                {
                    //LaserColor(3, 0, 1, 2, "Roger"); // Roger
                    keypadCode = "1762";
                }
                else if (numberOfCores > 0)
                {
                    //LaserColor(0, 3, 1, 2, "Arrow"); // Arrow
                    keypadCode = "7702";
                }
                else if (firstIDNumber == 2 || firstIDNumber == 4 || firstIDNumber == 6 || firstIDNumber == 8)
                {
                    //LaserColor(0, 1, 2, 3, "Hades"); //Hades
                    keypadCode = "2506";
                }
                else
                {
                    //LaserColor(1, 2, 3, 0, "Spear"); // Spear
                    keypadCode = "2802";
                }
            }
        }
    }

    void CodeFour()
    {
        if(beeperCode == 4)
        {
            if(containsSpecialLetters == true && RBCode > 2938)
            {
                //LaserColor(3, 1, 2, 0, "Admin"); // Admin
                keypadCode = "1998";
            }
            else if(greenCores > 0 && lastIDNumber > 4)
            {
                //LaserColor(2, 3, 0, 1, "Haste"); // Haste
                keypadCode = "5880";
            }
            else if(numberOfCores == 1)
            {
                //LaserColor(3, 0, 1, 2, "Roger"); // Roger
                keypadCode = "2798";
            }
            else
            {
                //LaserColor(1, 2, 3, 0, "Spear"); // Spear
                keypadCode = "6800";
            }
        }
    }

    void CodeFive()
    {
        if (beeperCode == 5)
        {
            if (RBCodeStringed.Contains("5") && (lastIDNumber == 1 || lastIDNumber == 3 || lastIDNumber == 5 || lastIDNumber == 7 || lastIDNumber == 9))
            {
                //LaserColor(3, 0, 1, 2, "Roger"); // Roger
                keypadCode = "1762";
            }
            else if (numberOfCores > 0)
            {
                //LaserColor(0, 3, 1, 2, "Arrow"); // Arrow
                keypadCode = "7702";
            }
            else if (firstIDNumber == 2 || firstIDNumber == 4 || firstIDNumber == 6 || firstIDNumber == 8)
            {
                //LaserColor(0, 1, 2, 3, "Hades"); //Hades
                keypadCode = "2506";
            }
            else
            {
                //LaserColor(1, 2, 3, 0, "Spear"); // Spear
                keypadCode = "2802";
            }
        }
    }

    void CodeSix()
    {
        if (beeperCode == 6)
        {
            if((lastIDNumber + firstIDNumber) > 8 && numberOfCores > 1)
            {
                //LaserColor(0, 1, 2, 3, "Hades"); //Hades
                keypadCode = "4853";
            }
            else if((firstIDNumber == 1 || firstIDNumber == 3 || firstIDNumber == 5 || firstIDNumber == 7 || firstIDNumber == 9) && firstIDNumber > 3)
            {
                //LaserColor(3, 0, 1, 2, "Roger"); // Roger
                keypadCode = "8124";
            }
            else if(containsSpecialLetters == true)
            {
                //LaserColor(2, 3, 0, 1, "Haste"); // Haste
                keypadCode = "7988";
            }
            else // refered to code 1
            {
                if (numberOfCores > 2 && !containsSpecialLetters)
                {
                    //LaserColor(1, 2, 3, 0, "Spear"); // Spear
                    keypadCode = "2486";
                }
                else if ((lastIDNumber + firstIDNumber) > 7 && redCores > 0)
                {
                    //LaserColor(2, 3, 0, 1, "Haste"); // Haste
                    keypadCode = "9768";
                }
                else if (numberOfCores == 1)
                {
                    //LaserColor(3, 0, 1, 2, "Roger"); // Roger
                    keypadCode = "8256";
                }
                else
                {
                    //LaserColor(0, 3, 1, 2, "Arrow"); // Arrow
                    keypadCode = "1587";
                }
            }
        }
    }
}
