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

    // Delegates
    public delegate void BlockDelegate();
    public BlockDelegate blockCallback;

    void Start()
    {
        playerRigi = GetComponent<Rigidbody>();
        moveSpeed = 5f;

        jumpThrust = 50f;
        jumpTimerMax = .2f;
        jumpTimer = jumpTimerMax;

        // PlayerBlock Function subscribes to blockCallback Delegate
        blockCallback += PlayerBlock;
    }

    void FixedUpdate()
    {
        PlayerMovement();
        PlayerJump();
    }

    void Update()
    {
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
        }
    }

    void PlayerBlock()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Player BLOCKS!");
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
