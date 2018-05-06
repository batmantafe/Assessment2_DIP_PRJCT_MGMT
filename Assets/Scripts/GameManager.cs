using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

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

        DebugTestKeys(); // Remove this later
    }

    void CharacterChoice()
    {
        playerChoseCharacter = PlayerPrefs.GetInt("CharacterChoice");

        switch (playerChoseCharacter)
        {
            case 1:
                Debug.Log("Player is DIVER!");

                // Activate Player as Diver here!

                break;
            case 2:
                Debug.Log("Player is SHRIMP");

                // Activate Player as Shrimp here!

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

            SceneManager.LoadScene("Lose");
        }

        if (gameWon == true)
        {
            Debug.Log("gameWon == true");

            SceneManager.LoadScene("Win");
        }
    }

    // For Debugging and Testing
    void DebugTestKeys()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            gameWon = true;
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            SceneManager.LoadScene("Debug Key: Lose!");

            gameLost = true;
        }
    }
}
