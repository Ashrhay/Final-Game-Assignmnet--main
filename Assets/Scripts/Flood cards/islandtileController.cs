using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandtileController : MonoBehaviour
{

    public int flag = 3; // Initial value of the flag (3 for starting state)

    // Update is called once per frame
    void Update()
    {
        if (flag == 1)
        {
            SinkIslandTile();
        }
    }

    public void SinkIslandTile()
    {
        // Perform any sinking effects or actions here
        Debug.Log("Island tile has sunk.");
        Destroy(gameObject);
    }
}
