using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRise : MonoBehaviour
{
    public int WaterLevel;
    public int MaxWaterLevel = 10;
    public GameObject[] levelIndicator;
    public LooseManager looseManager;
    public FloodManager floodManager; 
    

    // Start is called before the first frame update
   
 

  void UpdateLevelIndicators()
    {
        for (int i = 0; i < levelIndicator.Length; i++)
        {
            if (i < WaterLevel)
            {
                // Set the color of the level indicator to blue
                Renderer renderer = levelIndicator[i].GetComponent<Renderer>();
                renderer.material.color = Color.blue;
               
            }
            else
            {
                // Set the color of the level indicator to white
                Renderer renderer = levelIndicator[i].GetComponent<Renderer>();
                renderer.material.color = Color.white;
                
            }
        }
    }
    void Update()
    {
        UpdateLevelIndicators();

        if (WaterLevel >= MaxWaterLevel)
        {
            looseManager.GameOver("You have lost the game");
        }
        // Unlock card slots based on water level
        if (WaterLevel >= 1)
        {
            floodManager.UnlockCardSlot(0);
            Debug.Log("slot is unlocked"); 
        }

       
        if (WaterLevel >= 3)
        {
            floodManager.UnlockCardSlot(1);
           
        }
        if (WaterLevel >= 5)
        {
            floodManager.UnlockCardSlot(2);
            
        }
       

        if (WaterLevel >= 7)
        {
            floodManager.UnlockCardSlot(3);
         
        }
       

        if (WaterLevel >= 9)
        {
            floodManager.UnlockCardSlot(4);
           
        }
       
    }

    public void IncreaseWaterLevel()
    {
        // Increase the water level by 1 (if it's not already at the maximum)
        if (WaterLevel < MaxWaterLevel)
        {
            WaterLevel++;
            UpdateLevelIndicators();
        }
    }
}
