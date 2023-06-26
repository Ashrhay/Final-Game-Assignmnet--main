using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> Cards = new List<Card>();
  
    void Awake()
    {
       

        Cards.Add(new Card(0, "None", Resources.Load <Sprite>("Sprites/0") ));
        Cards.Add(new Card(1, "Gold Gate", Resources.Load<Sprite>("Sprites/1") ));
        Cards.Add(new Card(2, "Bronze Gate", Resources.Load<Sprite>("Sprites/2") ));
        Cards.Add(new Card(3, "Coral Palace", Resources.Load<Sprite>("Sprites/3") ));
        Cards.Add(new Card(4, "Watchtower", Resources.Load<Sprite>("Sprites/4") ));

    }
}
