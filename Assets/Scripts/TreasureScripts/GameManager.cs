using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<CardMovementScript> deck = new List<CardMovementScript>();
    public Transform[] cardSlots;
    public List<CardMovementScript> discardPile = new List<CardMovementScript>();
    public bool[] availableCardSlots;

    public GameObject collectTreasureButton;
    public GameObject[] treasureSlot; // Reference to the treasure slot game object

    // Define the treasure types
    public enum TreasureType { Fire, Water, Rock, Air }
    public TreasureType currentTreasureType;
    public winManager WinManager; 

    // Counters for each treasure type
    private int fireCount;
    private int waterCount;
    private int rockCount;
    private int airCount;

    // Replace these with the actual prefab references
    public GameObject fireTreasurePrefab;
    public GameObject waterTreasurePrefab;
    public GameObject rockTreasurePrefab;
    public GameObject airTreasurePrefab;

    private void Start()
    {
        collectTreasureButton.SetActive(false);
    }

    public void DrawCards()
    {
        if (deck.Count >= 1)
        {
            CardMovementScript randCard = deck[Random.Range(0, deck.Count)];

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

                    // Increase the corresponding treasure count
                    IncrementTreasureCount(randCard.treasureObject);

                    // Check if the treasure count reaches 4
                    if (IsTreasureComplete())
                        collectTreasureButton.SetActive(true);

                    return;
                }
            }
        }
    }

    private void IncrementTreasureCount(TreasureType treasureType)
    {
        switch (treasureType)
        {
            case TreasureType.Fire:
                fireCount++;
                break;
            case TreasureType.Water:
                waterCount++;
                break;
            case TreasureType.Rock:
                rockCount++;
                break;
            case TreasureType.Air:
                airCount++;
                break;
        }
    }

    public bool IsTreasureComplete()
    {
        switch (currentTreasureType)
        {
            case TreasureType.Fire:
                return fireCount >= 4;
            case TreasureType.Water:
                return waterCount >= 4;
            case TreasureType.Rock:
                return rockCount >= 4;
            case TreasureType.Air:
                return airCount >= 4;
        }
        return false;
    }
    public void CollectTreasure()
    {
        Debug.Log("Treasure Collected!");

        // Move the treasure object to the treasure slot
        GameObject treasure = Instantiate(GetTreasurePrefab(currentTreasureType), treasureSlot[(int)currentTreasureType].transform);
        treasure.transform.localPosition = Vector3.zero;

        collectTreasureButton.SetActive(false);
        ResetCardSlots();

        if (IsTreasureComplete())
        {
            WinManager.YOUWIN(" You Collected All Treasures!");
        }
    }

    private GameObject GetTreasurePrefab(TreasureType treasureType)
    {
        switch (treasureType)
        {
            case TreasureType.Fire:
                return fireTreasurePrefab; // Replace with the actual fire treasure prefab
            case TreasureType.Water:
                return waterTreasurePrefab; // Replace with the actual water treasure prefab
            case TreasureType.Rock:
                return rockTreasurePrefab; // Replace with the actual rock treasure
        }
        return null; 
    }
    public void ResetCardSlots()
    {
        foreach (Transform cardSlot in cardSlots)
        {
            CardMovementScript cardScript = cardSlot.GetComponentInChildren<CardMovementScript>();
            if (cardScript != null)
            {
                cardScript.hasBeenPlayed = false;
                cardScript.gameObject.SetActive(false);
                deck.Add(cardScript);
            }
        }
    }

}

