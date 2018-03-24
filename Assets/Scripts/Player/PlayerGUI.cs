using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    public float playerHealthMax;
    public float playerHealth;
    public GUIStyle healthBarStyle; // Player > HUD > FuelBarRed > Normal > Background

    void Start()
    {
        playerHealthMax = 100;
        playerHealth = playerHealthMax;
    }

    void Update()
    {
        TestHealth();
    }

    void OnGUI()
    {
        float scrW = Screen.width / 16; // Dividing Screen Width into 16 parts, value of scrW = 1
        float scrH = Screen.height / 9; // Dividing Screen Height into 9 parts, value of scrH = 1

        // Health Bar
        GUI.Box(new Rect(6f * scrW, 8.4f * scrH, playerHealth * (4 * scrW) / playerHealthMax, 0.5f * scrH), "", healthBarStyle);
    }

    void TestHealth()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            playerHealth = playerHealth - 10;
            Debug.Log("Testing playerHealth in PlayerGUI script.");

            if(playerHealth <= 0)
            {
                playerHealth = 0;
            }
        }
    }
}
