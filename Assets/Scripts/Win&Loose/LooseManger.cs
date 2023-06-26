using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LooseManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void GameOver(string message)
    {
        Debug.Log("Game Over: " + message);

        gameOverPanel.SetActive(true);

        // Pausing the game.
        Time.timeScale = 0f;

        // Restarting the game
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}