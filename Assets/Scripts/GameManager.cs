using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseUI;
    public static bool isGameOver;
    public static bool isPaused;

    void Start()
    {
        isGameOver = false;
        Continue();
    }
    void Update()
    {
        if (isGameOver)
        {
            return;
        }
        
        if (UIManager.lives <= 0)
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Continue();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
        isGameOver = true;
        gameOverUI.SetActive(true);
    }

    private void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void Continue()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

}