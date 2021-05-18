using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject GUIobject;
    public GameObject TransitionLayer;
    public static GameManager Instance;
    private enum ActionMaps
    {
        Gameplay,
        Menu
    }
    private ActionMaps currentActionMap;
    public enum GameState
    {
        //TODO:
        //Add no control state for end of stage
        Playing,
        Paused
    }
    private GameState currentState;

    private void Awake()
    {
        Instance = this;
        GUIobject.SetActive(true);
        TransitionLayer.SetActive(true);
    }
    private void Start()
    {
        Player.ResetPoints();
        List<SpriteRenderer> transparentFXsprites = new List<SpriteRenderer>();
        foreach (SpriteRenderer o in FindObjectsOfType<SpriteRenderer>())
        {
            if (o.tag == "HideOnStart")
            {
                o.enabled = false;
            }
        }
    }
    private void Update()
    {
        switch (currentState)
        {
            case GameState.Playing:
                if (currentActionMap != ActionMaps.Gameplay)
                {
                    playerInput.SwitchCurrentActionMap("Player");
                    currentActionMap = ActionMaps.Gameplay;
                    Time.timeScale = 1f;
                }
                break;
            case GameState.Paused:
                if (currentActionMap != ActionMaps.Menu)
                {
                    playerInput.SwitchCurrentActionMap("UI");
                    currentActionMap = ActionMaps.Menu;
                    Time.timeScale = 0f;
                }
                break;
        }
    }
    public void Pause()
    {
        currentState = GameState.Paused;
    }
    public void UnPause()
    {
        currentState = GameState.Playing;
    }
    public void UnPauseDelay(float delay)
    {
        StartCoroutine(DelayedPause(delay));
    }
    private IEnumerator DelayedPause(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        UnPause();
    }
    public bool IsPaused()
    {
        return currentState == GameState.Paused;
    }
    public bool IsPlaying()
    {
        return currentState == GameState.Playing;
    }
    public GameState GetCurrentState()
    {
        return currentState;
    }
    
    private void OnDestroy()
    {
        Instance = null;
    }

    internal bool CanPause()
    {
        if (currentState == GameState.Playing)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
