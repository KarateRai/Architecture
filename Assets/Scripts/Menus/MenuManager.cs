using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject MenuCanvas;
    public Animator MenuAnimator;
    public EventSystem eventSystem;
    public PlayerInput playerInput;
    public UnityEvent onExitSelection;
    private GameManager gameManager;
    public bool isMainMenu;
    public Button initialButton;
    private Button lastActiveButton;
    private int loadTimer = 60;
    private bool initialLoadDone = false;
    public bool initialSelection = false;
    private bool selectPrevious = false;
    private void Start()
    {
        gameManager = GameManager.Instance;
        
    }
    private void Update()
    {
        if (isMainMenu)
        {
            if (loadTimer < 0 && !initialLoadDone)
            {
                initialLoadDone = true;
                MenuEnter();
            }
            else if (initialLoadDone == false)
            {
                loadTimer--;
            }
        }
        
        //Select initial button once menu is loaded
        if (!initialSelection && initialButton.IsInteractable() && MenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("MenuActive"))
        {
            initialButton.Select();
            initialSelection = true;
        }

        //Reset initial button selection function
        if (initialSelection && MenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("MenuInactive"))
        {
            initialSelection = false;
            eventSystem.SetSelectedGameObject(null); //can't reselect without deselecting or we get selected objects without visual indication.
        }


        if (selectPrevious)
        {
            lastActiveButton.Select();
            selectPrevious = false;
        }
    }
    public void MenuEnter() //Todo: add restrictions on when it's possible to enter menu
    {
        if (GameManager.Instance.CanPause())
        {
        MenuAnimator.SetBool("InMenu", true);
        gameManager.Pause();
        }

    }
    public void EnterSelection()
    {
        lastActiveButton = eventSystem.currentSelectedGameObject.GetComponent<Button>();
        MenuAnimator.SetBool("InSelection", true);
        eventSystem.SetSelectedGameObject(null); //can't reselect without deselecting or we get selected objects without visual indication.
    }
    public void ExitSelection()
    {
        if (MenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("InSelection"))
        {
            MenuAnimator.SetBool("InSelection", false);
            selectPrevious = true;
            onExitSelection.Invoke();
        }
        else if (MenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("MenuActive") && !isMainMenu)
        {
            MenuExit();
        }
    }
    public void MenuExit()
    {
        MenuAnimator.SetBool("InMenu", false);
        gameManager.UnPauseDelay(1);
        
    }
}
