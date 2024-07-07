using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuRetry : MonoBehaviour
{
    public GameObject menu;

    //Restart the level
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
        Debug.Log("Level Restarted");
        GameManager.Instance.retryMenu.SetActive(false);
        GameManager.Instance.retry();
    }

    //Go back to Main menu
    public void Quit()
    {
        //SceneManager.LoadScene();
        Debug.Log("Game Quit");
    }
}
