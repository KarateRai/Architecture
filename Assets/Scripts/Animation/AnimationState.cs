using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationState : MonoBehaviour
{
    private Animator animator;
    private List<string> animationBoolNames;    
    public enum CharacterState
    {
        Idle,
        Run,
        Jump,
        Fall,
        Land,
        Dash,
        Throw,
        Crouch
    }
    private CharacterState currentState;
    private bool playSound;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animationBoolNames = new List<string>();
    }
    private void Start()
    {
        SetAnimationNames();
    }

    private void SetAnimationNames()
    {
        //the names for all bools in animator related to their animation.
        animationBoolNames.Add("isRunning");
        animationBoolNames.Add("isJumping");
        animationBoolNames.Add("isFalling");
        animationBoolNames.Add("isLanding");
        animationBoolNames.Add("isDashing");
        animationBoolNames.Add("isThrowing");
        animationBoolNames.Add("isCrouching");
    }

    void Update()
    {
        UpdateAnimator();        
    }

    private void UpdateAnimator()
    {
        //Debug.LogWarning(currentState);
        switch (currentState)
        {
            case CharacterState.Idle:
                if (playSound)
                {
                    FindObjectOfType<AudioManager>().Stop("steps");
                    playSound = false;
                }
                SetAniBoolTrue("AllOff"); //no bool is named AllOff, thus all will turn off.
                break;
            case CharacterState.Run:
                if (!playSound)
                {
                    FindObjectOfType<AudioManager>().Play("steps");
                    playSound = true;
                }
                SetAniBoolTrue("isRunning");
                break;
            case CharacterState.Jump:
                SetAniBoolTrue("isJumping");
                if (playSound)
                {
                    FindObjectOfType<AudioManager>().Stop("steps");
                    playSound = false;
                }
                break;
            case CharacterState.Fall:
                if (playSound)
                {
                    FindObjectOfType<AudioManager>().Stop("steps");
                    playSound = false;
                }
                SetAniBoolTrue("isFalling");
                break;
            case CharacterState.Land: //TimeUpAnimation script should call for switching to idle OnStateExit.
                SetAniBoolTrue("isLanding");
                break;
            case CharacterState.Dash:
                SetAniBoolTrue("isDashing");
                break;
            case CharacterState.Throw:
                SetAniBoolTrue("isThrowing");
                break;
            case CharacterState.Crouch:
                SetAniBoolTrue("isCrouching");
                break;
        }
    }

    internal void SetAniBoolTrue(string aniName)
    {
        foreach (string aniBool in animationBoolNames)
        {
            if (aniBool == aniName)
            {
                animator.SetBool(aniName, true);
            }
            else
            {
                animator.SetBool(aniBool, false);
            }
        }
    }
    internal void AnimationEnded(AnimatorStateInfo stateInfo) //use this if a transition to a new state needs to know when animation ends. needs setup in animator.
    {
        if (stateInfo.IsName("Land"))
        {
            SetCharacterState(CharacterState.Idle);
        }
        else if (stateInfo.IsName("Throw"))
        {
            SetCharacterState(CharacterState.Idle);
        }
    }

    public void SetCharacterState(CharacterState newState)
    {
        currentState = newState;
    }
    public CharacterState GetCurrentCharacterState()
    {
        return currentState;
    }

    public bool CanMove()
    {
        switch (currentState)
        {
            case CharacterState.Idle:
                return true;
            case CharacterState.Run:
                return true;
            case CharacterState.Jump:
                return true;
            case CharacterState.Fall:
                return true;
            case CharacterState.Land:
                return false;
            case CharacterState.Dash:
                return true;
            case CharacterState.Throw:
                return false;
            case CharacterState.Crouch:
                return false;
            default:
                return true;
        }
    }
    public bool CanJump()
    {
        switch (currentState)
        {
            case CharacterState.Idle:
                return true;
            case CharacterState.Run:
                return true;
            case CharacterState.Jump:
                return true;
            case CharacterState.Fall:
                return true;
            case CharacterState.Land:
                return false;
            case CharacterState.Dash:
                return false;
            case CharacterState.Throw:
                return false;
            case CharacterState.Crouch:
                return false;
            default:
                return true;
        }
    }
    public bool CanDash()
    {
        switch (currentState)
        {
            case CharacterState.Idle:
                return true;
            case CharacterState.Run:
                return true;
            case CharacterState.Jump:
                return true;
            case CharacterState.Fall:
                return true;
            case CharacterState.Land:
                return false;
            case CharacterState.Dash:
                return false;
            case CharacterState.Throw:
                return false;
            case CharacterState.Crouch:
                return false;
            default:
                return true;
        }
    }
    /// <summary>
    /// Call on in cases where animations don't properly transition. (jumping going to land without falling etc)
    /// </summary>
    public void ReturnToIdle()
    {
        currentState = CharacterState.Idle;
        animator.SetTrigger("ReturnToIdle");
    }
}
