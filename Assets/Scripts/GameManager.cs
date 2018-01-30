﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will handle all the gamestates required throughout the game
public class GameManager : MonoBehaviour
{
    #region Variables
    public DungeonGenerator dungeonPrefab; //Allows us to create the object
    private DungeonGenerator dungeonInstance; //Can create more instances of the object.

    #endregion


    #region Functions
    private void Start()
    {
        GenerateDungeon(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateDungeon(1);
        }

    }

    #region DungeonRelated Functions
    //Begins the generation of the dungeon, takes in a value to choose whether to remake the dungeon or not.
    private void GenerateDungeon(int restart)
    {
        if (restart == 1)
        {
            StopAllCoroutines();
            Destroy(dungeonInstance.gameObject);
            GenerateDungeon(0);
        }
        else
        {
            dungeonInstance = Instantiate(dungeonPrefab) as DungeonGenerator;
            StartCoroutine(dungeonInstance.Generate());
        }
    }
    #endregion
    #endregion
}
