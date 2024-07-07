using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject menu;

    Movement movement;
    Weapon weapon;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Pause();
        }
    }

    private void updatePlayer()
    {
        movement = GameManager.Instance.Player.GetComponent<Movement>();
        weapon = GameManager.Instance.Player.GetComponent<Weapon>();
    }

    public void Pause()
    {
        updatePlayer();
        isPaused = !isPaused;
        menu.SetActive(isPaused);
        Time.timeScale = Convert.ToInt32(!isPaused);
        movement.enabled = !isPaused;
        weapon.enabled = !isPaused;
        Debug.Log("Game Paused");
    }

    //Restart the level
    public void Retry()
    {
        Pause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Level Restarted");
    }

    //Open Settings menu
    public void Settings() 
    {
        Debug.Log("Settings Opened");
    }

    //Go back to Main menu or exit cave
    public void Quit()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) Application.Quit();
        else
        {
            Pause();
            SceneManager.LoadScene(0);
        }
    }
}
