using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationTest : MonoBehaviour
{
    public AnimationState aniState;
    
    public void Move(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>().x < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            Debug.Log("Run Left");
            aniState.SetCharacterState(AnimationState.CharacterState.Run);
        }   
        else if (context.ReadValue<Vector2>().x > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
            Debug.Log("Run Right");
            aniState.SetCharacterState(AnimationState.CharacterState.Run);
        }
        else
        {
            Debug.Log("Idle");
            aniState.SetCharacterState(AnimationState.CharacterState.Idle);
        }
    }
    public void Look(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        }
        else if (context.canceled)
        {
            aniState.SetCharacterState(AnimationState.CharacterState.Idle);
        }
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Fire");
            aniState.SetCharacterState(AnimationState.CharacterState.Throw);
        }
        else if (context.canceled)
        {
            aniState.SetCharacterState(AnimationState.CharacterState.Idle);
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump");
            aniState.SetCharacterState(AnimationState.CharacterState.Jump);
        }
        else if(context.canceled)
        {
            aniState.SetCharacterState(AnimationState.CharacterState.Idle);
        }
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Dash");
            aniState.SetCharacterState(AnimationState.CharacterState.Dash);
        }
        else if (context.canceled)
        {
            aniState.SetCharacterState(AnimationState.CharacterState.Idle);
        }
    }
    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Crouching Down");
            aniState.SetCharacterState(AnimationState.CharacterState.Crouch);
        }
        else if (context.canceled)
        {
            Debug.Log("Standing Up");
            aniState.SetCharacterState(AnimationState.CharacterState.Idle);
        }
    }
}
