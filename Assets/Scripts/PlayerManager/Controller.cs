using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Controller : MonoBehaviour
{
    public GameObject[] characters; // Array of character prefabs
    public List<GameObject> instantiatedCharacters;
    public Button[] actionButtons; // Array of action buttons
    public Text remainingActionsText; // Text component to display remaining actions
    private Text playerIndexText; // Text component to display current player index
    public Text playerNameText; // Text component to display current player name
    public Text playerAbilityText; // Text component to display current player ability
    private Text NumberOfPlayersText;

    private int numberOfPlayers;
    public int numberOfActivePlayers;
    public List<GameObject> players = new List<GameObject>();
    private List<int> assignedCharacters; // Stores the assigned character indices for each player
    private int currentPlayerIndex;
    public int remainingActions;

  //  public InputField PlayerInput;
  //  public Text ErrorMessage;
    //public Controller getplayerNo;


    private void Start()
    {
        // Get the number of players from user input
        numberOfPlayers = GetNumberOfPlayers();

        // Assign characters to players
        AssignCharacters();

        // Set the initial player's turn
        currentPlayerIndex = 0;
        remainingActions = 3;
        remainingActionsText.text = "Remaining actions: " + remainingActions.ToString();
        SetCurrentPlayerTurn(currentPlayerIndex);
    }

    public int GetNumberOfPlayers()
    {
        
        return numberOfActivePlayers - 1;
    }

   /* public void CheckPlayers()
    {
        GameObject check = GameObject.Find("Checker");
        GameObject main = GameObject.Find("MainMenu");
        if (int.TryParse(PlayerInput.text, out int playerCount))
        {
            if (playerCount >= 2 && playerCount <= 4)
            {
                // Player count is valid
                ErrorMessage.text = "";

                check.SetActive(false);

                main.SetActive(true);

                numberOfActivePlayers = playerCount;
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

    }*/



    private void AssignCharacters()
    {
        // Randomly assign characters to players
        assignedCharacters = new List<int>();


        for (int i = 0; i < numberOfActivePlayers; i++)
        {
            int randomIndex = Random.Range(0, characters.Length);

            // Check if the character has already been assigned
            while (assignedCharacters.Contains(randomIndex))
            {
                randomIndex = Random.Range(0, characters.Length);
            }

            assignedCharacters.Add(randomIndex);
        }

        // Instantiate player objects with assigned characters
        for (int i = 0; i < numberOfActivePlayers; i++)
        {

            // Instantiate the character prefab and add it to the instantiatedCharacters list
            GameObject characterInstance = Instantiate(characters[assignedCharacters[i]]);
            instantiatedCharacters.Add(characterInstance);
        }


    }

    private void SetCurrentPlayerTurn(int playerIndex)
    {
        // Deactivate all players
        foreach (GameObject player in players)
        {
            player.SetActive(false);

        }

        // Activate the current player
        GameObject currentPlayer = players[playerIndex];
        GameObject currentCharacter = instantiatedCharacters[playerIndex];
        currentPlayer.SetActive(true);


        // Get the Character component of the current player
        Character character = currentCharacter.GetComponent<Character>();
        PlayerID playerID = currentPlayer.GetComponent<PlayerID>();

        // Remove the "(Clone)" suffix from the character's name
        string characterName = character.name.Replace("(Clone)", "");

        // Update UI with current player's information
       // playerIndexText.text = playerIndex.ToString();
        //NumberOfPlayersText.text = "Number of players: " + numberOfActivePlayers.ToString();

        playerNameText.text = playerID.PlayerName;
        playerAbilityText.text = characterName;

        if (playerIndex == playerID.id)
        {
            playerID.isCurrentPlayer = true;
        }
        else
            playerID.isCurrentPlayer = false;

        // Show the action buttons for the current player
        SetActionButtonsActive(true);

        if (playerIndex == playerID.id)
        {
            playerID.isCurrentPlayer = true;

            // Check if the current player is an Engineer and activate the corresponding button
            if (character.specialAbilities.Contains(Character.SpecialAbility.Engineer))
            {
                character.shoreUpButton.gameObject.SetActive(true);
                character.shoreUpButton.interactable = true;
            }
            else
            {
                character.shoreUpButton.gameObject.SetActive(false);
            }

            if (character.specialAbilities.Contains(Character.SpecialAbility.Navigator))
            {
                character.moveOtherPlayersButton.gameObject.SetActive(true);
                character.moveOtherPlayersButton.interactable = true;
            }
            else
            {
                character.moveOtherPlayersButton.gameObject.SetActive(false);
            }
        }
        else
        {
            playerID.isCurrentPlayer = false;
            character.shoreUpButton.gameObject.SetActive(false);
            character.moveOtherPlayersButton.gameObject.SetActive(false);

        }

    }

    private void SetActionButtonsActive(bool isActive)
    {
        foreach (Button button in actionButtons)
        {
            button.gameObject.SetActive(isActive);
        }
    }

    public void PerformAction()
    {
        // Decrease the remaining actions count
        remainingActions--;

        // Update UI with remaining actions
        remainingActionsText.text = "Remaining Actions: " + remainingActions;

        // Check if remaining actions are exhausted
        if (remainingActions == 0)
        {
            // Hide the action buttons
            SetActionButtonsActive(false);



            // Move to the next player's turn
            StartCoroutine(NextTurnCoroutine());
        }
    }

    private IEnumerator NextTurnCoroutine()
    {
        yield return new WaitForSeconds(2f); // Add a delay before moving to the next player's turn

        currentPlayerIndex++;
        playerIndexText.text = currentPlayerIndex.ToString();

        // Check if all players have played and restart from the first player
        if (currentPlayerIndex > 3)
        {
            currentPlayerIndex = 0;
            playerIndexText.text = currentPlayerIndex.ToString();

        }

        remainingActions = 3;
        remainingActionsText.text = "Remaining actions: " + remainingActions.ToString();

        GameObject currentPlayer = players[currentPlayerIndex];


        PlayerID playerID = currentPlayer.GetComponent<PlayerID>();


        //playerNameText.text = playerID.PlayerName;




        if (currentPlayerIndex == playerID.id)
        {
            playerID.isCurrentPlayer = true;
        }
        else
            playerID.isCurrentPlayer = false;



        foreach (GameObject player in players)
        {
            player.SetActive(false);
        }

        // Activate the current player
        currentPlayer.SetActive(true);

        // Check if all players have played and restart from the first player
        if (currentPlayerIndex > numberOfPlayers)
        {
            currentPlayerIndex = 0;
        }

        SetCurrentPlayerTurn(currentPlayerIndex);
    }
}

