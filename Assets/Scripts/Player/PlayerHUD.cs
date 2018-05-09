using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [Header("Health HUD")]
    public float playerHealthMax;
    public float playerHealth;
    public GUIStyle healthBarStyle; // Player > HUD > Health Bar Style > Normal > Background
    public float hazardDamage;

    [Header("Block HUD")]
    public float playerBlockMax;
    public float playerBlockCurrent;
    public GUIStyle blockBarStyle;

    [Header("Timer HUD")]
    public float playerTimerMax;
    public float playerTime;
    public GUIStyle timeBarStyle; // Player > HUD > Time Bar Style > Normal > Background

    void Start()
    {
        //playerHealthMax = 100;
        playerHealth = playerHealthMax;

        //hazardDamage = 25;

        //playerBlockMax = 1;
        playerBlockCurrent = playerBlockMax;

        //playerTimerMax = 10f;
        playerTime = playerTimerMax;
    }

    void Update()
    {
        TestBars();

        BlockCooldown();

        Countdown();
    }

    void OnGUI()
    {
        float scrW = Screen.width / 16; // Dividing Screen Width into 16 parts, value of scrW = 1
        float scrH = Screen.height / 9; // Dividing Screen Height into 9 parts, value of scrH = 1

        // Health Bar
        GUI.Box(new Rect(6f * scrW, 8.4f * scrH, playerHealth * (4 * scrW) / playerHealthMax, 0.5f * scrH), "", healthBarStyle);

        // Block Bar
        GUI.Box(new Rect(6f * scrW, 8.15f * scrH, playerBlockCurrent * (4 * scrW) / playerBlockMax, 0.25f * scrH), "", blockBarStyle);

        // Timer Bar
        GUI.Box(new Rect(6f * scrW, 7.9f * scrH, playerTime * (4 * scrW) / playerTimerMax, 0.25f * scrH), "", timeBarStyle);
    }

    #region Debugging Shortcuts
    void TestBars()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            playerHealth = playerHealth - 10;
            Debug.Log("Testing playerHealth in PlayerGUI script.");

            if (playerHealth <= 0)
            {
                playerHealth = 0;
            }
        }
    }
    #endregion

    void BlockCooldown()
    {
        if (playerBlockCurrent <= 0)
        {
            playerBlockCurrent = 0;
        }

        if (playerBlockCurrent >= playerBlockMax)
        {
            playerBlockCurrent = playerBlockMax;
        }

        if (PlayerInput.isBlocking == true &&
            playerBlockCurrent > 0f)
        {
            playerBlockCurrent = playerBlockCurrent - (1f * Time.deltaTime);
        }

        if (PlayerInput.isBlocking == false &&
            playerBlockCurrent != playerBlockMax)
        {
            playerBlockCurrent = playerBlockCurrent + (0.25f * Time.deltaTime);
        }
    }

    void Countdown()
    {
        playerTime = playerTime - (1 * Time.deltaTime);

        if (playerTime <= 0)
        {
            playerTime = 0;

            GameManager.gameLost = true;
        }

        //Debug.Log("timeCount = " + playerTime);
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            playerHealth = playerHealth - (hazardDamage * Time.deltaTime);
        }

        if (other.gameObject.CompareTag("Win"))
        {
            GameManager.gameWon = true;
        }
    }
}
