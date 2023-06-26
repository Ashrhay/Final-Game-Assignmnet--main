using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloodManager : MonoBehaviour
{
    public List<FloodCardMovement> deck = new List<FloodCardMovement>();
    public Transform[] cardSlots;
    public List<FloodCardMovement> discardPile = new List<FloodCardMovement>();
    // Telling us what card slots are available and what are not.
    public bool[] availableCardSlots;
    public LooseManger looseManager;
    public GameObject foolsLandingTile; // Reference to the Fool's Landing island tile GameObject

    private void Awake()
    {
        // Initialize all card slots as locked (false)
        availableCardSlots = new bool[cardSlots.Length];
        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            availableCardSlots[i] = false;
            Debug.Log("Card slots are locked at the beginning of the game");
        }
    }

    // Unlock a specific card slot
    public void UnlockCardSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < availableCardSlots.Length)
        {
            availableCardSlots[slotIndex] = true;
        }
    }

    public void DrawCards()
    {
        // Checking to see if there are still cards in the deck that we can draw.
        if (deck.Count >= 1)
        {
            // Find all available card slots
            List<int> availableSlots = new List<int>();
            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    availableSlots.Add(i);
                }
            }

            // Check if there are available slots to place the card
            if (availableSlots.Count > 0)
            {
                FloodCardMovement randCard = deck[Random.Range(0, deck.Count)];
                int randomSlotIndex = Random.Range(0, availableSlots.Count);

                int slotIndex = availableSlots[randomSlotIndex];
                Transform cardSlot = cardSlots[slotIndex];

                randCard.gameObject.SetActive(true);
                randCard.handIndex = slotIndex;
                randCard.transform.position = cardSlot.position;
                randCard.hasBeenPlayed = false;

                availableCardSlots[slotIndex] = false;
                deck.Remove(randCard);
            }
        }
    }

    public void Shuffle()
    {
        // Want to shuffle the cards if there is at least one card in the discard pile
        if (discardPile.Count >= 1)
        {
            // Loop through our discard pile
            foreach (FloodCardMovement card in discardPile)
            {
                deck.Add(card);
            }
            // Removing each card from the discard pile and moving it back to the deck.
            discardPile.Clear();
        }
    }

    public void SinkIslandTile(GameObject islandTile)
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (cardSlots[i].gameObject == islandTile)
            {
                availableCardSlots[i] = false;
                // Perform any other actions to destroy or remove the island tile from the game
                Debug.Log("Island tile " + islandTile.name + " has sunk.");

                // Checking if the Fool's Landing tile has sunk.
                if (islandTile == foolsLandingTile)
                {
                    looseManager.GameOver("Fool's Landing has sunk!");
                }
                break;
            }
        }
    }
}