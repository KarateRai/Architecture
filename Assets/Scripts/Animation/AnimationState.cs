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
                SetAniBoolTrue("AllOff"); //no bool is named AllOff, thus all will turn off.
                break;
            case CharacterState.Run:
                SetAniBoolTrue("isRunning");
                break;
            case CharacterState.Jump:
                SetAniBoolTrue("isJumping");
                break;
            case CharacterState.Fall:
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
        Debug.Log("Setting state to: " + newState);
        currentState = newState;
    }
    public CharacterState GetCurrentCharacterState()
    {
        return currentState;
    }
}
