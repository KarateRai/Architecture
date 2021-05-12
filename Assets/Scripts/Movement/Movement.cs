using System;
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
    public bool isGrounded;
    private int jumpsLeft;
    private AnimationState animState;
    private SpriteRenderer spriteRenderer;
    public GameObject spriteObject;
    private bool isFalling = false;

    //Public variables for inspector
    [Header("Movement Settings")]
    public float moveSpeed;
    public float crouchSpeed;
    public float jumpForce;
    public float crawlSpeed;

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
    public int extraJumps = 1;

    //Dash
    [Header("Special Movement settings")]
    public float dashSpeed;
    public float dashTime;
    private float dashTimeCounter;
    public float dashCooldown;
    private float dashCooldownCounter;
    private float dashDir;
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
        
       
        //Setup counters
        jumpsLeft = extraJumps;
        dashTimeCounter = dashTime;
        dashCooldownCounter = dashCooldown;
        dashCounter = dashes;
    }

    private void Start()
    {
        animState = spriteObject.GetComponent<AnimationState>();
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (isFalling)
        {
            if (cayoteCounter < cayoteTime && rb.velocity.y < 0)
            {
                animState.SetCharacterState(AnimationState.CharacterState.Fall);
            }
            else if (animState.GetCurrentCharacterState() != AnimationState.CharacterState.Jump)
            {
                animState.SetCharacterState(AnimationState.CharacterState.Land);
                isFalling = false;
            }
        }
        else if(animState.GetCurrentCharacterState() != AnimationState.CharacterState.Land &&
            animState.GetCurrentCharacterState() != AnimationState.CharacterState.Jump)
        {

            if (inputX == 0 && animState.GetCurrentCharacterState() != AnimationState.CharacterState.Idle)
            {
                animState.SetCharacterState(AnimationState.CharacterState.Idle);
            }
            else if (inputX != 0)
            {
                animState.SetCharacterState(AnimationState.CharacterState.Run);
            }
        }
        

        if (inputX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (inputX > 0)
        {
            spriteRenderer.flipX = false;
        }


        
    }

    
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        //Move on X plane
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);


        //Cayote timer
        if (isGrounded)
        {
            cayoteCounter = cayoteTime;
        }
        else
        {
            cayoteCounter -= Time.deltaTime;
            isFalling = true;
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
            dashCooldownCounter -= Time.deltaTime;
            if (dashCooldownCounter < 0)
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
            animState.SetCharacterState(AnimationState.CharacterState.Jump);
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
            dashDir = inputX;
            dashTimeCounter = dashTime;
            dashCooldownCounter = dashCooldown;
            dashCounter--;
            animState.SetCharacterState(AnimationState.CharacterState.Dash);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Teleporter t = collision.GetComponent<Teleporter>();
        if (t != null)
        {
            this.transform.position = t.teleportTo.position;
        }
    }
}

