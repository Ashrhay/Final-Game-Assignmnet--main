using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card 
{
    public int id;
    public string cardName;

    public Sprite thisImage; 

    public Card(int Id, string CardName, Sprite ThisImage)
    {
        id = Id; 
        cardName = CardName;
        thisImage = ThisImage;
    }
}
