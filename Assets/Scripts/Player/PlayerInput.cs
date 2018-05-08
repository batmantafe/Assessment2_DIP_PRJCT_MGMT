﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Character
{
    [Header("Movement")]
    public Rigidbody playerRigi;
    public float moveSpeed;
    public float turnSpeed;

    [Header("Jump")]
    public float jumpThrust;
    public float jumpTimer;
    public float jumpTimerMax;

    [Header("Block")]
    public static bool isBlocking;

    void Start()
    {
        playerRigi = GetComponent<Rigidbody>();
        moveSpeed = 5f;
        turnSpeed = 100f;

        jumpThrust = 50f;
        jumpTimerMax = .2f;
        jumpTimer = jumpTimerMax;

        isBlocking = false;
    }

    void FixedUpdate()
    {
        PlayerMovement();
        PlayerJump();
    }

    public override void Update()
    {
        base.Update();
        PlayerAttack();
        PlayerBlock();
    }

    #region Movement
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

        // Turn Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * (Time.deltaTime * turnSpeed));
        }

        // Turn Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * turnSpeed));
        }
    }
    #endregion

    #region Jump
    void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) && jumpTimer != 0f)
        {
            JumpTimerFunction();

            playerRigi.AddForce(transform.up * jumpThrust);
        }
    }

    void JumpTimerFunction()
    {
        jumpTimer = jumpTimer - (1 * Time.deltaTime);

        if (jumpTimer <= 0f)
        {
            jumpTimer = 0f;
        }
    }
    #endregion

    #region Attack/Block
    void PlayerAttack()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Player ATTACKS!");
            Attack();
        }
    }

    void PlayerBlock()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            isBlocking = true;

            Debug.Log("isBlocking = " + isBlocking);
        }

        else
        {
            isBlocking = false;
        }
    }
    #endregion

    #region Collisions
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // Reset Clock
            jumpTimer = jumpTimerMax;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {

        }
    }
    #endregion
}
