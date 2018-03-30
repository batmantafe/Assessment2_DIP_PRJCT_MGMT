using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [Header("Health HUD")]
    public float playerHealthMax;
    public float playerHealth;
    public GUIStyle healthBarStyle; // Player > HUD > Health Bar Style > Normal > Background

    [Header("Block HUD")]
    public float playerBlockMax;
    public float playerBlockCurrent;
    public GUIStyle blockBarStyle;

    void Start()
    {
        playerHealthMax = 100;
        playerHealth = playerHealthMax;

        playerBlockMax = 1;
        playerBlockCurrent = playerBlockMax;
    }

    void Update()
    {
        TestBars();

        BlockCooldown();
    }

    void OnGUI()
    {
        float scrW = Screen.width / 16; // Dividing Screen Width into 16 parts, value of scrW = 1
        float scrH = Screen.height / 9; // Dividing Screen Height into 9 parts, value of scrH = 1

        // Health Bar
        GUI.Box(new Rect(6f * scrW, 8.4f * scrH, playerHealth * (4 * scrW) / playerHealthMax, 0.5f * scrH), "", healthBarStyle);

        // Block Bar
        GUI.Box(new Rect(6f * scrW, 8.15f * scrH, playerBlockCurrent * (4 * scrW) / playerBlockMax, 0.25f * scrH), "", blockBarStyle);
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
}
