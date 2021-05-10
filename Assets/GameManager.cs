using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerInput playerInput;
    
    public static GameManager Instance;
    private enum ActionMaps
    {
        Gameplay,
        Menu
    }
    private ActionMaps currentActionMap;
    public enum GameState
    {
        Playing,
        Paused
    }
    private GameState currentState;

    private void Awake()
    {
        Instance = this;
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
                }
                break;
            case GameState.Paused:
                if (currentActionMap != ActionMaps.Menu)
                {
                    playerInput.SwitchCurrentActionMap("UI");
                    currentActionMap = ActionMaps.Menu;
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
        yield return new WaitForSeconds(delay);
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
}
