using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class winManager : MonoBehaviour
{

    public GameObject WinPanel;

    public void YOUWIN(string message)
    {
        Debug.Log("You win" + message);

        WinPanel.SetActive(true);

        //Pausing the game. 
        Time.timeScale = 0f;

        //Restarting the game
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
