using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    //Private variables
    Rigidbody2D rb;
    BoxCollider2D crouchDisableCollider;
    private float inputX;
    private bool isGrounded;
    private int jumpsLeft;

    //Public variables for inspector
    [Header("Movement Settings")]
    public float moveSpeed;
    public float crouchSpeed;
    public float jumpForce;
    public float crawlSpeed;
    private bool isCrouching;

    [Header("Movement Control Points")]
    public Transform groundPoint;
    public Transform crouchPoint;
    public LayerMask whatIsGround;

    //Cayote time
    private float cayoteCounter;
    [Header("Movement Buffers")]
    public float cayoteTime;

    //Jump buffer
    public float jumpBuffer;
    private float jumpBufferCount;

    //Dash
    [Header("Special Movement settings")]
    public float dashSpeed;
    private float dashTimeCounter;
    public float dashTime;
    private int dashDir;
    public int extraJumps = 1;


    //Camera settings
    [Header("Player Camera Control")]
    public float aheadAmount;
    public float aheadSpeed;
    public Transform cameraTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        crouchDisableCollider = GetComponent<BoxCollider2D>();
        jumpsLeft = extraJumps;
        dashTimeCounter = dashTime;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);
        //Move on X plane
        if (!isCrouching)
        {
            rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(inputX * crouchSpeed, rb.velocity.y);
        }
        

        //Cayote timer
        if (isGrounded)
        {
            cayoteCounter = cayoteTime;
        }
        else
        {
            cayoteCounter -= Time.deltaTime;
        }
        jumpBufferCount -= Time.deltaTime;

        if (Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround) == true)
        {
            jumpsLeft = extraJumps;
        }

        if (inputX != 0)
        {
            cameraTarget.localPosition = new Vector3(Mathf.Lerp(cameraTarget.localPosition.x, aheadAmount * rb.velocity.x, aheadSpeed * Time.deltaTime), cameraTarget.localPosition.y, cameraTarget.localPosition.z);
        }

        if (dashTime <= 0)
        {
            dashDir = 0;
        }
        else
        {
            dashTime -= Time.deltaTime;
            if (dashDir > 0)
            {
                rb.velocity = Vector2.right * dashSpeed;
            }
            else if (dashDir < 0)
            {
                rb.velocity = Vector2.left * dashSpeed;
            }
        }
       
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().x > 0)
        {
            inputX = 1;
        }
        else if (context.ReadValue<Vector2>().x < 0)
        {
            inputX = -1;
        }
        else
        {
            inputX = 0;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpBufferCount = jumpBuffer;
        }
        
        if (jumpBufferCount >= 0 && cayoteCounter > 0 && context.performed || jumpBufferCount >= 0 && jumpsLeft > 0 && context.performed)
        {
            jumpsLeft--;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (context.canceled && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dashDir = (int)inputX;
            dashTime = dashTimeCounter;
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouching = true;
            crouchDisableCollider.enabled = false;
        }
        else if (context.canceled)
        {
            isCrouching = false;
            crouchDisableCollider.enabled = true;
        }
    }
}

