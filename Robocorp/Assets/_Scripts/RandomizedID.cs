using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomizedID : MonoBehaviour
{
    [SerializeField] TextMeshPro IDToRandomize;
    [SerializeField] int IDLength;
    [SerializeField] char[] specialCharacters;

    [HideInInspector] public int firstIDNumber;
    [HideInInspector] public int lastIDNumber;
    [HideInInspector] public bool containsSpecialLetters;

    private void Awake()
    {
        IDToRandomize.text = RandomID();
    }

    private void Start()
    {
        firstIDNumber = IDToRandomize.text[0] - 48;
        lastIDNumber = IDToRandomize.text[IDLength - 1] - 48;

        foreach (char character in specialCharacters)
            if (IDToRandomize.text.Contains(character))
                containsSpecialLetters = true;        
    }

    private string RandomID()
    {
        var myChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var myNums = "0123456789";
        var randomChars = new char[IDLength];

        for (int i = 1; i < randomChars.Length - 1; i++)
            randomChars[i] = myChars[Random.Range(0, myChars.Length)];

        randomChars[IDLength - 1] = myNums[Random.Range(0, myNums.Length)];
        randomChars[0] = myNums[Random.Range(0, myNums.Length)];

        return new string(randomChars);
    }
}
