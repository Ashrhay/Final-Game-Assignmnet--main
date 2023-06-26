using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerID : MonoBehaviour
{
    public Guid ID;//{ get; private set; }
    public string PlayerName;// { get; private set; }
    public int id;
    public bool isCurrentPlayer;


    private void Awake()
    {
        GeneratePlayerID();

    }

    public void GeneratePlayerID()
    {
        ID = Guid.NewGuid();
        Debug.Log("Player ID generated: " + ID.ToString());
    }



    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }




}
