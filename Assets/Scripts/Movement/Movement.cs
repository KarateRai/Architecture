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
    private AnimationState animState;

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
    public int dashes = 1;
    private int dashCounter;


    //Camera settings
    [Header("Player Camera Control")]
    public float aheadAmount;
    public float aheadSpeed;
    public Transform cameraTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        crouchDisableCollider = GetComponent<BoxCollider2D>();
        animState = GetComponent<AnimationState>();
       
        //Setup counters
        jumpsLeft = extraJumps;
        dashTimeCounter = dashTime;
        dashCounter = dashes;
    }
    private void Update()
    {
        if (inputX < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }    
        else if (inputX > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }
    
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);
        if (inputX == 0)
        {
            animState.SetCharacterState(AnimationState.CharacterState.Idle);
        }
        //Move on X plane
        if (!isCrouching && !Physics2D.OverlapCircle(crouchPoint.position, .4f, whatIsGround) && inputX != 0)
        {
            
            rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
            crouchDisableCollider.enabled = true;
        }
        else
        {
            animState.SetCharacterState(AnimationState.CharacterState.Run); //Crouch
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

        //Extra jumps resett
        if (Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround) == true)
        {
            jumpsLeft = extraJumps;
        }

        //Camera adjustment
        if (inputX != 0)
        {
            cameraTarget.localPosition = new Vector3(Mathf.Lerp(cameraTarget.localPosition.x, aheadAmount * rb.velocity.x, aheadSpeed * Time.deltaTime), cameraTarget.localPosition.y, cameraTarget.localPosition.z);
        }

        //Dash
        if (dashTimeCounter <= 0)
        {
            dashDir = 0;
            dashTimeCounter -= Time.deltaTime;
            if (Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround) == true && dashTimeCounter < -0.5f)
            {
                dashCounter = dashes;
            }
        }
        else
        {
            dashTimeCounter -= Time.deltaTime;
            if (dashDir > 0 && dashCounter >= 0)
            {
                rb.velocity = Vector2.right * dashSpeed;
            }
            else if (dashDir < 0 && dashCounter >= 0)
            {
                rb.velocity = Vector2.left * dashSpeed;
            }
        }
       
    }

    public void Move(InputAction.CallbackContext context)
    {
        animState.SetCharacterState(AnimationState.CharacterState.Run);
        inputX = context.ReadValue<Vector2>().x;
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
        if (context.performed && dashTimeCounter <= 0 && dashCounter > 0)
        {
            dashDir = (int)inputX;
            dashTimeCounter = dashTime;
            dashCounter--;
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
        }
    }
}

