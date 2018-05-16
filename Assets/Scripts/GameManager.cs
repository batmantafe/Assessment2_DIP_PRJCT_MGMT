using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private int playerChoseCharacter;

    public static bool gameLost, gameWon;

    public GameObject lobsterGO, diverGO;
    public Camera lobsterCam, diverCam;

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

                diverGO.GetComponent<PlayerHUD>().enabled = true;
                diverGO.GetComponent<PlayerInput>().enabled = true;
                diverGO.GetComponent<SphereCollider>().enabled = false;
                diverGO.GetComponent<Enemy>().enabled = false;
                diverGO.GetComponent<NavMeshAgent>().enabled = false;

                lobsterGO.GetComponent<PlayerHUD>().enabled = false;
                lobsterGO.GetComponent<PlayerInput>().enabled = false;
                lobsterGO.GetComponent<SphereCollider>().enabled = true;
                lobsterGO.GetComponent<Enemy>().enabled = true;
                lobsterGO.GetComponent<NavMeshAgent>().enabled = true;

                diverCam.enabled = true;
                lobsterCam.enabled = false;

                break;
            case 2:
                Debug.Log("Player is SHRIMP");

                // Activate Player as Shrimp here!

                lobsterGO.GetComponent<PlayerHUD>().enabled = true;
                lobsterGO.GetComponent<PlayerInput>().enabled = true;
                lobsterGO.GetComponent<SphereCollider>().enabled = false;
                lobsterGO.GetComponent<Enemy>().enabled = false;
                lobsterGO.GetComponent<NavMeshAgent>().enabled = false;

                diverGO.GetComponent<PlayerHUD>().enabled = false;
                diverGO.GetComponent<PlayerInput>().enabled = false;
                diverGO.GetComponent<SphereCollider>().enabled = true;
                diverGO.GetComponent<Enemy>().enabled = true;
                diverGO.GetComponent<NavMeshAgent>().enabled = true;

                lobsterCam.enabled = true;
                diverCam.enabled = false;

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
