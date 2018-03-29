using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject inGameOptionsMenu;

    private string currentScene;

    // Use this for initialization
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        Splash();

        EscapeKeyInGame();
    }

    #region Splash Screen
    void Splash()
    {
        if (currentScene == "Splash")
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }
    #endregion

    #region Main Menu
    public void PlayButton()
    {
        SceneManager.LoadScene("Character");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    #endregion

    #region Character
    public void DiverButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShrimpButton()
    {
        SceneManager.LoadScene("Game");
    }
    #endregion

    #region ESC / In-Game Options Menu
    void EscapeKeyInGame()
    {
        if (currentScene == "Game")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (inGameOptionsMenu.activeSelf == false)
                {
                    Time.timeScale = 0f;

                    inGameOptionsMenu.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1f;

                    inGameOptionsMenu.SetActive(false);
                }
            }
        }
    }

    public void EscapeButton()
    {
        Time.timeScale = 0f;

        inGameOptionsMenu.SetActive(true);
    }

    public void ContinueButton()
    {
        Time.timeScale = 1f;

        inGameOptionsMenu.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
    #endregion
}
