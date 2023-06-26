using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] tileHolders;
    
    
    void Start()
    {
        ShuffleTiles();
        PlaceTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShuffleTiles()
    {
        for (int i = tiles.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = tiles[i];
            tiles[i] = tiles[j];
            tiles[j] = temp;
        }
    }

    void PlaceTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            GameObject tile = tiles[i];
            Transform tileHolder = tileHolders[i].transform;
            tile.transform.position = tileHolder.position;
            tile.transform.rotation = tileHolder.rotation;
            tile.transform.SetParent(tileHolder);
        }
    }

   


}
