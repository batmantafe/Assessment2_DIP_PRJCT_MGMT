using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 0;
    public float force;
    public float lifeTime = 1;
    public Vector3 direction;

    private float currentLife = 0;
    private bool hasFired = false;
    private Rigidbody rigid;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFired)
        {
            rigid.AddForce(direction * force, ForceMode.Impulse);
            hasFired = true;
        }
        currentLife += Time.deltaTime;
        if (currentLife >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {

    }
}
