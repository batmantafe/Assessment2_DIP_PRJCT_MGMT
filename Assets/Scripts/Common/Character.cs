using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterChoice selectedCharacter;
    public enum CharacterChoice
    {
        Diver,
        Shrimp
    }

    [Header("DiverGun")]
    public int DDamage = 2;
    public int DRange = 10;
    
    [Header("ShrimpPunch")]
    public int SDamage = 4;
    public int SRange = 3;
    
    public void DiverGun()
    {
        Debug.Log("I'm Shooting The Guy!");
    }

    public void ShrimpPunch()
    {

    }
}
