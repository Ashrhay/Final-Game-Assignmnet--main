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
    public GameObject[] treasureSlot;
    public GameObject treasureTile1;
    public GameObject treasureTile2;

    public enum TreasureType { None, Fire, Water, Rock, Air }
    public TreasureType currentTreasureType;
    public winManager winManager;

    private int fireCount;
    private int waterCount;
    private int rockCount;
    private int airCount;

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

                    if (randCard.treasureObject != TreasureType.None)
                    {
                        IncrementTreasureCount(currentTreasureType);

                        if (IsTreasureComplete())
                            collectTreasureButton.SetActive(true);

                        if (currentTreasureType == TreasureType.Fire)
                            CheckSunkTreasureTile(treasureTile1, treasureTile2, fireCount);
                        else if (currentTreasureType == TreasureType.Water)
                            CheckSunkTreasureTile(treasureTile1, treasureTile2, waterCount);
                        else if (currentTreasureType == TreasureType.Rock)
                            CheckSunkTreasureTile(treasureTile1, treasureTile2, rockCount);
                        else if (currentTreasureType == TreasureType.Air)
                            CheckSunkTreasureTile(treasureTile1, treasureTile2, airCount);
                    }

                    return;
                }
            }
        }
    }

    internal void GameOver(string v)
    {
        throw new System.NotImplementedException();
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

    private void CheckSunkTreasureTile(GameObject treasureTile1, GameObject treasureTile2, int treasureCount)
    {
        if (treasureTile1 != null && treasureTile2 != null)
        {
            if (treasureCount >= 2)
            {
                if (!treasureTile1.GetComponent<TreasureScript>().IsSunk() || !treasureTile2.GetComponent<TreasureScript>().IsSunk())
                {
                    collectTreasureButton.SetActive(false);
                    ResetCardSlots();
                }
            }
        }
    }

    public void CollectTreasure()
    {
        Debug.Log("Treasure Collected!");

        GameObject gameObject1 = Instantiate(GetTreasurePrefab(currentTreasureType), treasureSlot[(int)currentTreasureType].transform);
        GameObject treasure = gameObject1;
        treasure.transform.localPosition = Vector3.zero;

        collectTreasureButton.SetActive(false);
        ResetCardSlots();

        if (IsTreasureComplete())
        {
            winManager.YOUWIN(" You Collected All Treasures!");
        }
    }

    private GameObject GetTreasurePrefab(TreasureType treasureType)
    {
        switch (treasureType)
        {
            case TreasureType.Fire:
                return fireTreasurePrefab;
            case TreasureType.Water:
                return waterTreasurePrefab;
            case TreasureType.Rock:
                return rockTreasurePrefab;
            case TreasureType.Air:
                return airTreasurePrefab;
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

    public void MoveToDiscardPile(CardMovementScript card)
    {
        discardPile.Add(card);
        card.gameObject.SetActive(false);
    }
}