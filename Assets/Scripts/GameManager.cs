using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int playerChoseCharacter;

    public static bool gameLost, gameWon;

    // Use this for initialization
    void Start()
    {
        StartGameConditions();

        CharacterChoice();
    }

    // Update is called once per frame
    void Update()
    {
        WinLoseCheck();
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

    void StartGameConditions()
    {
        gameLost = false;
        gameWon = false;
    }

    

    void WinLoseCheck()
    {
        if (gameLost == true)
        {
            Debug.Log("gameLost == true");
        }

        if (gameWon == true)
        {
            Debug.Log("gameWon == true");
        }
    }
}
