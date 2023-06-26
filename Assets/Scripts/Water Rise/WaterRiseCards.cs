using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class WaterRiseCards : MonoBehaviour
{
    // Keeping track if the card has been played previously.
    public bool hasBeenPlayed;

    public int handIndex;

    public FloodManager gm;

    public WaterRise waterRise; // Reference to the WaterRise script

    private void Start()
    {
        gm = FindObjectOfType<FloodManager>();
        waterRise = FindObjectOfType<WaterRise>();
    }

    private void OnMouseDown()
    {
        // Checking first to see if the card hasn't been played yet.
        if (hasBeenPlayed == false)
        {
            transform.position += Vector3.up * 5;
            hasBeenPlayed = true;
            gm.availableCardSlots[handIndex] = true;

            // After the card has been played, the card needs to move to the discard pile.
            Invoke("MoveToDiscardPile", 2f);

            // Increase the water level when the card is played
            waterRise.IncreaseWaterLevel();
            Debug.Log("Water meter is incredes by 1 increment"); 
        }
    }

    void MoveToDiscardPile()
    {
      

        // Deactivating the card after it is transferred into the discard pile.
        gameObject.SetActive(false);

        gm.DrawCards();
    }
}
