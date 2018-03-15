using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody playerRigi;
    public float moveSpeed;

    [Header("Jump")]
    public float jumpThrust;
    public float jumpTimer;
    public float jumpTimerMax;

    // Use this for initialization
    void Start()
    {
        playerRigi = GetComponent<Rigidbody>();
        moveSpeed = 5f;

        jumpThrust = 50f;
        jumpTimerMax = .2f;
        jumpTimer = jumpTimerMax;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        PlayerJump();
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
    #endregion

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
