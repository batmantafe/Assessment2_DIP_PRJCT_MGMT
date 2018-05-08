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
    public int DTime = 3;
    public int DAttackRange = 7;
    public float DSpeed = 5;
    public float DAttackDelay = 1;
    public GameObject DProjectile;
    
    [Header("ShrimpPunch")]
    public int SDamage = 4;
    public int SRange = 3;
    public float SAttackDelay = 0.5f;

    private float currentAttackDelay;

    public virtual void Update()
    {
        currentAttackDelay += Time.deltaTime;
    }

    public void Attack()
    {
        switch (selectedCharacter)
        {
            case CharacterChoice.Diver:
                if (currentAttackDelay >= DAttackDelay)
                {
                    DiverGun();
                }
                break;
            case CharacterChoice.Shrimp:
                if (currentAttackDelay >= SAttackDelay)
                {
                    ShrimpPunch();
                }
                break;
            default:
                break;
        }
    }

    void DiverGun()
    {
        Debug.Log(transform);
        Debug.Log("I'm Shooting The Guy!");
        GameObject clone = Instantiate(DProjectile, transform);
        Projectile cloneProjectile = clone.GetComponent<Projectile>();
        cloneProjectile.direction = transform.forward;
        cloneProjectile.force = DSpeed;
        cloneProjectile.damage = DDamage;
        cloneProjectile.lifeTime = DTime;
    }

    void ShrimpPunch()
    {
        Debug.Log("I'm Punching The Guy!");
    }
}
