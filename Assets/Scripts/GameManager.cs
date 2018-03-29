using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int playerChoseCharacter;

    // Use this for initialization
    void Start()
    {
        CharacterChoice();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CharacterChoice()
    {
        playerChoseCharacter = PlayerPrefs.GetInt("CharacterChoice");

        switch (playerChoseCharacter)
        {
            case 1:
                Debug.Log("Player is DIVER!");
                break;
            case 2:
                Debug.Log("Player is SHRIMP");
                break;
        }
    }
}
