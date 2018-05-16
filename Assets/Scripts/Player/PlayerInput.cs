using System.Collections;
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
    public float jumpWait;

    [Header("Block")]
    public static bool isBlocking;

    [Header("Lobster: Animations")]
    public GameObject lobsterMaya;
    public Animator lobsterAnim;
    public bool lobsterMoving;

    void Start()
    {
        playerRigi = GetComponent<Rigidbody>();
        moveSpeed = 5f;
        turnSpeed = 100f;

        jumpThrust = 50f;
        jumpTimerMax = .2f;
        jumpTimer = jumpTimerMax;
        jumpWait = 4f;

        isBlocking = false;

        lobsterMoving = false;

        lobsterAnim = lobsterMaya.GetComponent<Animator>();

        lobsterAnim.SetTrigger("LobsterIdle");
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

            lobsterMoving = true;
        }
        
        // Move Down
        if (Input.GetKey(KeyCode.S))
        {
            playerRigi.MovePosition(transform.position + -transform.forward * (Time.deltaTime * moveSpeed));

            lobsterMoving = true;
        }

        // Turn Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * (Time.deltaTime * turnSpeed));

            lobsterMoving = true;
        }
        
        // Turn Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * turnSpeed));

            lobsterMoving = true;
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            lobsterMoving = false;
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

        StartCoroutine(CanJump());
    }

    IEnumerator CanJump()
    {
        yield return new WaitForSeconds(jumpWait);

        jumpTimer = jumpTimerMax;
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
        /*if (other.gameObject.CompareTag("Ground"))
        {
            // Reset Clock
            jumpTimer = jumpTimerMax;

            Debug.Log("Ground Enter");
        }*/
    }

    void OnCollisionStay(Collision other)
    {
        /*if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Ground Exit");
        }*/
    }
    #endregion

    #region Animations
    void Animations()
    {
        if (lobsterMoving == true)
        {
            lobsterAnim.SetTrigger("LobsterWalking");
        }

        if (lobsterMoving == false)
        {
            lobsterAnim.SetTrigger("LobsterIdle");
        }
    }
    #endregion
}
