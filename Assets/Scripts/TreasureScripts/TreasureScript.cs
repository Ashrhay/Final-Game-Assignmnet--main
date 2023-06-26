using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    public TreasureObjectType treasureType; // Assign the corresponding treasure type for each island tile in the inspector
    
    public enum TreasureObjectType
    {
        Fire,
        Water,
        Rock,
        Air
    }
    public bool IsSunk()
    {
        // Implement your logic to determine if the treasure is sunk
        // Return true if the treasure is sunk, otherwise return false
        // You can use any criteria you want to determine if the treasure is sunk
        // For example, you might check if the treasure is underwater or if it's in a specific state
        return false;
    }

    private void OnMouseDown()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager.IsTreasureComplete() && gameManager.currentTreasureType == (GameManager.TreasureType)treasureType)
        {
            gameManager.CollectTreasure();
            Destroy(gameObject);
        }
    }
}