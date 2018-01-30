using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Different dungeon room types
//Can be used to determine what things are placed within each room
public enum DungeonRoomType
{
    Start,
    Boss,
    Treasure,
    Trap,
    Rooms
}
public class DungeonRooms : MonoBehaviour {
    //public int numRooms = Enum.GetNames(typeof(DungeonRoomType)).Length; // Counts the number of rooms in the enum. So we can add extra anytime
    public static int Count = 5;
    //public IntVector2 startCoords;

  

    public static DungeonRoomType RandomRoom
    {
        get { return (DungeonRoomType)UnityEngine.Random.Range(0, Count); }
        
    }
}
