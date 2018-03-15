using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Rigidbody playerRigi;
    public float moveSpeed;

    // Use this for initialization
    void Start()
    {
        playerRigi = GetComponent<Rigidbody>();
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        // Move Up
        if (Input.GetKey(KeyCode.W))
        {
            playerRigi.MovePosition(transform.position + transform.forward * (Time.deltaTime * moveSpeed));
        }

        // Move Down
        if (Input.GetKey(KeyCode.S))
        {
            playerRigi.MovePosition(transform.position + -transform.forward * (Time.deltaTime * moveSpeed));
        }

        // Move Left
        if (Input.GetKey(KeyCode.A))
        {
            playerRigi.MovePosition(transform.position + -transform.right * (Time.deltaTime * moveSpeed));
        }

        // Move Right
        if (Input.GetKey(KeyCode.D))
        {
            playerRigi.MovePosition(transform.position + transform.right * (Time.deltaTime * moveSpeed));
        }
    }
}
