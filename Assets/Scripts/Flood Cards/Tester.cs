using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; 

public class Tester : MonoBehaviour
{
    public GameObject tile;
    public FlippingCard flippingCard;
   
    public void Start()
    {
        flippingCard = tile.GetComponent<FlippingCard>(); // Get the FlippingCard component from the tile GameObject
    }

    public void Button()
    {
        flippingCard.StartFlip(); // Call the StartFlip() method from the FlippingCard component
    }
}
