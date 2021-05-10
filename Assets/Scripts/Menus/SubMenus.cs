using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenus : MonoBehaviour
{
    public GameObject subMenuOne;
    public GameObject subMenuTwo;
    public enum MenuStates
    {
        Main,
        SubOne,
        SubTwo
    }
    private MenuStates currentState;
    private void Start()
    {
        subMenuOne.SetActive(false);
        subMenuTwo.SetActive(false);
    }
    public void LeaveSubMenu()
    {
        switch (currentState)
        {
            case MenuStates.Main:
                break;
            case MenuStates.SubOne:
                subMenuOne.SetActive(false);
                currentState = MenuStates.Main;
                break;
            case MenuStates.SubTwo:
                subMenuTwo.SetActive(false);
                currentState = MenuStates.Main;
                break;
        }
    }
    public void EnterSubMenuOne()
    {
        subMenuOne.SetActive(true);
        currentState = MenuStates.SubOne;
    }
    public void EnterSubMenuTwo()
    {
        subMenuTwo.SetActive(true);
        currentState = MenuStates.SubTwo;
    }
}
