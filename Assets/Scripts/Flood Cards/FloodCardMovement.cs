using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class FloodCardMovement : MonoBehaviour
{
    //Keeping track if the card has been played previously. 
    public bool hasBeenPlayed;

    public int handIndex;

    private  FloodManager fm;

    public bool isTreasureObject { get; internal set; }

    private void Start()
    {
        fm = FindObjectOfType<FloodManager>();
    }

    private void OnMouseDown()
    {
        //Checking first to see if the card hasn not been played yet.
        if (hasBeenPlayed == false)
        {
            transform.position += Vector3.up * 5;
            hasBeenPlayed = true;
            fm.availableCardSlots[handIndex] = true;

            //After the card has been played the card needs to move to the discard pile; 
            Invoke("MoveToDiscardPile", 2f);
        }
    }

    private void MoveToDiscardPile()
    {
        fm.MoveToDiscardPile(this);
    }
}

