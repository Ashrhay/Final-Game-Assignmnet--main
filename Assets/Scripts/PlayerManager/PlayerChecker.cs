using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChecker : MonoBehaviour
{
    public InputField PlayerInput;
    public Text ErrorMessage;
    //private Controller GetPlayerNo;

    GameObject mainMenu;

    public void CheckPlayers()
    {
        GameObject check = GameObject.Find("Checker");
        if (int.TryParse(PlayerInput.text, out int playerCount))
        {
            if (playerCount >= 2 && playerCount <= 4)
            {
                // Player count is valid
                ErrorMessage.text = "";

                check.SetActive(false);

                
                if (mainMenu != null)
                {
                    StartCoroutine(CloseGameObject());
                }

                //GetPlayerNo.numberOfActivePlayers = playerCount;
            }
            else
            {
                // Player count is invalid
                ErrorMessage.text = "Please enter a number between 2 and 4.";
            }
        }
        else
        {
            // Invalid input
            ErrorMessage.text = "Please enter a valid number.";
        }

    }

    private IEnumerator CloseGameObject()
    {
        Debug.Log("is waiting");
        yield return new WaitForSeconds(1f);

        mainMenu = GameObject.Find("MainMenu");
        mainMenu.SetActive(true);
    }
}
