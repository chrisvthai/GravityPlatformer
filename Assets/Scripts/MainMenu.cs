﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject MenuUI;
    public GameObject LevelsUI;

    // Use this for initialization
    void Start () {
        MenuUI.SetActive(true);
        LevelsUI.SetActive(false);
	}

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {

    }

    public void levels()
    {
        MenuUI.SetActive(false);
        LevelsUI.SetActive(true);
    }

    /***Now an exhaustive list of links to every single level in the game ****/

    public void mainMenu()
    {
        MenuUI.SetActive(true);
        LevelsUI.SetActive(false);
    }

    public void level0()
    {
        SceneManager.LoadScene(1); //Main Menu has to be scene 0, so +1 for every level
    }
}