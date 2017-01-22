using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;
    public GameObject ControlsUI;
    public GameObject DeadUI;
    public PlayerController player;
    public GameObject hero;
    private bool paused = false;
    

    void Start()
    {
        PauseUI.SetActive(false);
        ControlsUI.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            PauseUI.SetActive(true);
        }

        if(paused)
        {
            Time.timeScale = 0;
            
            
        } else
        {
            Time.timeScale = 1;
            PauseUI.SetActive(false);
            ControlsUI.SetActive(false);
        }
    }

    public void Resume()
    {
        paused = false;
    }

    public void Controls()
    {
        PauseUI.SetActive(false);
        ControlsUI.SetActive(true);

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ControlsReturn()
    {
        PauseUI.SetActive(true);
        ControlsUI.SetActive(false);
    }

    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void spawnAtCheckpoint()
    {
        hero.transform.position = player.lastCheckpoint;
        player.gravityDir = player.checkpointGravity;
        player.DeadUI.SetActive(false);
        player.enabled = true;
    }
}
