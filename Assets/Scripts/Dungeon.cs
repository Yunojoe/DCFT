using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// The random dungeon generator.
///
/// Starting with a stage of solid walls, it works like so:
///
/// 1. Place a number of randomly sized and positioned rooms. If a room
///    overlaps an existing room, it is discarded. Any remaining rooms are
///    carved out.
/// 2. Any remaining solid areas are filled in with mazes. The maze generator
///    will grow and fill in even odd-shaped areas, but will not touch any
///    rooms.
/// 3. The result of the previous two steps is a series of unconnected rooms
///    and mazes. We walk the stage and find every tile that can be a
///    "connector". This is a solid tile that is adjacent to two unconnected
///    regions.
/// 4. We randomly choose connectors and open them or place a door there until
///    all of the unconnected regions have been joined. There is also a slight
///    chance to carve a connector between two already-joined regions, so that
///    the dungeon isn't single connected.
/// 5. The mazes will have a lot of dead ends. Finally, we remove those by
///    repeatedly filling in any open tile that's closed on three sides. When
///    this is done, every corridor in a maze actually leads somewhere.
///
/// The end result of this is a multiply-connected dungeon with rooms and lots
/// of winding corridors.

public class Dungeon : MonoBehaviour {

    #region variables

    public static int dungeonSize = 20; // Total Rooms
    public int numRoomTries; // Keeps track of how many rooms have been made

    private int extraConnectorChance = 20; // Allows the chance of a room/path having multiple connections

    //Room variabls
    public int roomMaxSize = 20;
    public int roomMinSize = 4;
    public int roomExtraSize = 0; // Can allow for larger room sizes
    //private int windingPercent = 0;

    private List<GameObject> rooms = new List<GameObject>(); // Hold all of the gameobjects for rooms
    private List<Vector3> roomPositions = new List<Vector3>(); // Holds the world positions of each room

    private List<int> regions = new List<int>(); //For each open position in the dungeon, index of the connected region its part of
    private int currentRegion = -1; // Index of the current region    
    #endregion  



    // Use this for initialization
    void Start () {
        //TODO 
        //Create dungeon
        //Add Rooms
        //Add paths between rooms
        CreateDungeon();
		
	}
	

    void CreateDungeon()
    {

        AddRooms();
        
    }

    void AddRooms()
    {
        List<int> roomSize = new List<int>();
        int averageSize = 1; // Average room size
        //While we don't have enough rooms to reach dungeon size
        while (rooms.Count < dungeonSize)
        {
            //Create a room of random Size
            int size = Random.Range(roomMinSize, roomMinSize * averageSize);
            int rectangularity = Random.Range(0, 1 + size / 2) * 2;
            int width = size;
            int height = size;
            
        }
    }

}
