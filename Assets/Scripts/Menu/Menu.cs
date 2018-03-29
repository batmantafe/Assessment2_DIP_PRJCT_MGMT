using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
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
}
