using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodDeck : MonoBehaviour
{
    
    public List<Card> floodDeck = new List<Card>();

    public int x;
    public int deckSize;

    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        deckSize = 24;

        for(int i = 0; i < deckSize; i++)
        {
            x = Random.Range(0, CardDatabase.Cards.Count);
            floodDeck.Add(CardDatabase.Cards[x]); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
