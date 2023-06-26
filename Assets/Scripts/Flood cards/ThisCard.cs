using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThisCard : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisId;

    public int id;
    public string cardName;

    public Text nameText;

    public Sprite thisSprite;
    public Image thatImage;

    public bool cardBack;
    public static bool staticCardBack; 

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("i got the cards from the list");
        thisCard.Add(CardDatabase.Cards[thisId]);
    }

    // Update is called once per frame
    void Update()
    {
        if (thisCard.Count > 0)
        {
            id = thisCard[0].id;
            cardName = thisCard[0].cardName;

            thisSprite = thisCard[0].thisImage;

            nameText.text = cardName;

            thatImage.sprite = thisSprite; 
        }
        staticCardBack = cardBack;   
    }
}
