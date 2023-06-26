using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureGameManager : MonoBehaviour
{
    public List<CardMovementScript> deck = new List<CardMovementScript>();
    public Transform[] cardSlots;
    public List<CardMovementScript> discardPile = new List<CardMovementScript>(); 
    // Telling us what card slots are availe and what are not. 
    public bool[] availableCardSlots; 

    public void DrawCards()
    {
        // checking to see if there are still cards in the deck that we can draw. 
        if(deck.Count>= 1)
        {
            CardMovementScript randCard = deck[Random.Range(0, deck.Count)]; 

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i]== true)
                {
                    // looping the cards through to the available slots. 
                    randCard.gameObject.SetActive(true); 

                    // to know where the card has been placed in the player's hand. 
                    randCard.handIndex = i; 

                    //activationg the postion for where we want the avaliable card to go. 
                    randCard.transform.position = cardSlots[i].position;

                    // to make the card playable after it was in the discard pile. 
                    randCard.hasBeenPlayed = false; 

                    //setting the remainding cards to not go ontop of the pther card occuping a slot.
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return; 
                }
            }
        }
    }

    public void Shuffle()
    {
        //Want to shuffle the cards if there is at least one card in the discard pile 
        if(discardPile.Count >= 1)
        {
            //Loop through our discard pile
            foreach( CardMovementScript card in discardPile)
            {
                deck.Add(card); 
            }
            //removing each card from the discard pile and moving it back to the deck. 
            discardPile.Clear(); 
        }
    }
}
