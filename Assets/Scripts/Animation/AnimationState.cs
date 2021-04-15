using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationState : MonoBehaviour
{
    private Animator animator;
    public enum CharacterState
    {
        Idle,
        Run,
        Jump,
        Fall,
        Land,
        Dash,
        Throw
    }
    private CharacterState currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
                animator.SetBool("isLanding", false);
                animator.SetBool("isDashing", false);
                animator.SetBool("isThrowing", false);
                break;
            case CharacterState.Run:
                animator.SetBool("isRunning", true);
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
                animator.SetBool("isLanding", false);
                animator.SetBool("isDashing", false);
                animator.SetBool("isThrowing", false);
                break;
            case CharacterState.Jump:
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
                animator.SetBool("isLanding", false);
                animator.SetBool("isDashing", false);
                animator.SetBool("isThrowing", false);
                break;
            case CharacterState.Fall:
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
                animator.SetBool("isLanding", false);
                animator.SetBool("isDashing", false);
                animator.SetBool("isThrowing", false);
                break;
            case CharacterState.Land: //TimeUpAnimation script should call for switching to idle OnStateExit.
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
                animator.SetBool("isLanding", true);
                animator.SetBool("isDashing", false);
                animator.SetBool("isThrowing", false);
                break;
            case CharacterState.Dash:
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
                animator.SetBool("isLanding", false);
                animator.SetBool("isDashing", true);
                animator.SetBool("isThrowing", false);
                break;
            case CharacterState.Throw:
                animator.SetBool("isRunning", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
                animator.SetBool("isLanding", false);
                animator.SetBool("isDashing", false);
                animator.SetBool("isThrowing", false);
                break;
        }
    }

    internal void AnimationEnded(AnimatorStateInfo stateInfo)
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
}
