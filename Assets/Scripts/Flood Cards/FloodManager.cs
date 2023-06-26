using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodManager : MonoBehaviour
{
    public List<FloodCardMovement> deck = new List<FloodCardMovement>();
    public Transform[] cardSlots;
    public List<FloodCardMovement> discardPile = new List<FloodCardMovement>();
    public bool[] availableCardSlots;
    public GameObject foolsLandingTile;
    public Transform discardPileTransform;
    public GameManager gameManager;
    private int fireTileCount;
    private int waterTileCount;
    private int rockTileCount;
    private int airTileCount;

    private bool isTreasureTile1Sunk = false;
    private bool isTreasureTile2Sunk = false;

    private void Awake()
    {
        availableCardSlots = new bool[cardSlots.Length];
        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            availableCardSlots[i] = false;
            Debug.Log("Card slots are locked at the beginning of the game");
        }
    }

    public void UnlockCardSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < availableCardSlots.Length)
        {
            availableCardSlots[slotIndex] = true;
        }
    }

    public void DrawCards()
    {
        if (deck.Count >= 1)
        {
            FloodCardMovement randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;
                    randCard.transform.position = cardSlots[i].position;
                    randCard.hasBeenPlayed = false;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);

                    // Check if the drawn card is a treasure card
                    if (randCard.isTreasureObject)
                    {
                        // Increase the corresponding treasure tile count
                        IncrementTileCount(randCard.GetComponent<TreasureScript>().treasureType);

                        // Check for game over conditions
                        if (fireTileCount == 0 || waterTileCount == 0 || rockTileCount == 0 || airTileCount == 0)
                        {
                            gameManager.GameOver("One or more treasure tiles have sunk!");
                        }
                        else if (fireTileCount == -1 && waterTileCount == -1 && rockTileCount == -1 && airTileCount == -1)
                        {
                            gameManager.GameOver("All treasure tiles have sunk!");
                        }
                    }

                    return;
                }
            }
        }
    }

    private void IncrementTileCount(TreasureScript.TreasureObjectType treasureType)
    {
        switch (treasureType)
        {
            case TreasureScript.TreasureObjectType.Fire:
                fireTileCount++;
                break;
            case TreasureScript.TreasureObjectType.Water:
                waterTileCount++;
                break;
            case TreasureScript.TreasureObjectType.Rock:
                rockTileCount++;
                break;
            case TreasureScript.TreasureObjectType.Air:
                airTileCount++;
                break;
        }
    }

    internal void MoveToDiscardPile(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }

    public void Shuffle()
    {
        if (discardPile.Count >= 1)
        {
            foreach (FloodCardMovement card in discardPile)
            {
                deck.Add(card);
            }
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
                Debug.Log("Island tile " + islandTile.name + " has sunk.");

                // Decrease the corresponding treasure tile count
                TreasureScript.TreasureObjectType treasureType = cardSlots[i].GetComponent<CardMovementScript>().GetComponent<TreasureScript>().treasureType;
                switch (treasureType)
                {
                    case TreasureScript.TreasureObjectType.Fire:
                        fireTileCount--;
                        break;
                    case TreasureScript.TreasureObjectType.Water:
                        waterTileCount--;
                        break;
                    case TreasureScript.TreasureObjectType.Rock:
                        rockTileCount--;
                        break;
                    case TreasureScript.TreasureObjectType.Air:
                        airTileCount--;
                        break;
                }

                // Check for game over conditions
                if (fireTileCount == 0 || waterTileCount == 0 || rockTileCount == 0 || airTileCount == 0)
                {
                    gameManager.GameOver("One or more treasure tiles have sunk!");
                }
                else if (fireTileCount == -1 && waterTileCount == -1 && rockTileCount == -1 && airTileCount == -1)
                {
                    gameManager.GameOver("All treasure tiles have sunk!");
                }
                break;
            }
        }
    }
    public void MoveToDiscardPile(FloodCardMovement card)
    {
        discardPile.Add(card);
        card.gameObject.SetActive(false);
        DrawCards();
    }
}
