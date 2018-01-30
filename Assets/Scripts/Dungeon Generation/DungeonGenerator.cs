using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will handle most of the code used when generating the dungeons.
public class DungeonGenerator : MonoBehaviour {
    #region Variables
    //public int dungeonSizeX, dungeonSizeZ; //X and Y ranges for the dungeon size
    public float generationStopDelay; // Allows us to slow down the generation of the dungeon to watch it happen


    //TODO make this private and add ability to change the prefab item during runtime.
    public DungeonCell[] cellPrefab = new DungeonCell[2]; // Holds the cell prefab object
    public DungeonRooms roomPrefab;


    private DungeonCell[,] cells; //2D array holding coordinates of the cells
    private List<IntVector2> exitCells; //2D array holding coordinates for cells used as exits
    
    //Variables dealing with rooms 
    public int RoomSize; // Sets the size of the current room being generated
    public int MaxRooms; // Sets the maximum number of rooms
    public int currentRooms; // Keeps track of the number of rooms we've currently made
    public bool isRoomTouching; // Will be used to check if a cell is touching a room or not
    private bool startRoomMade;
    private bool bossRoomMade;

    //Holds the size of the x/z axis for our dungeon
    public IntVector2 size;

    #endregion

    #region Functions
    public IEnumerator Generate()
    {

        CheckValues();
        //Makes sure a dungeon is always generated if the size isn't specified prior
       
        WaitForSeconds delay = new WaitForSeconds(generationStopDelay);

        //GenerateRooms
        cells = new DungeonCell[size.x, size.z]; //Sets the size of our array to the size of the dungeon
        

        //GenerateCorridors

       
        while (currentRooms < MaxRooms)
        {
            IntVector2 coordinates = RandomCoordinates;          
                
            yield return delay;
            DungeonRoomType room = RandomRoom;
            //Debug.Log(room);
            CreateRoom(RandomCoordinates, room);
                
            coordinates.z += 1;
            
            currentRooms += 1;
        }

    }

    public void CheckValues()
    {
        if (size.x == 0)
            size.x = 40;
        if (size.z == 0)
            size.z = 40;
        if (generationStopDelay <= 0)
            generationStopDelay = 0.01f;
        if (MaxRooms == 0)
            MaxRooms = (int)(Random.Range(3, 10));
    }


    public DungeonRoomType RandomRoom
    {
      get { return DungeonRooms.RandomRoom; }
    }

    //Getter/Setter used to get random coordinates based on the size of our dungeon
    public IntVector2 RandomCoordinates
    {
        get { return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z)); }
    }

    //Checks that the coordinates fall within the dungeon
    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    private void CreateRoom(IntVector2 startCoords, DungeonRoomType roomType)
    {

        DungeonRooms newRoom = Instantiate(roomPrefab) as DungeonRooms;
        newRoom.name = "Dungeon Room " + roomType;
        newRoom.transform.parent = transform;
        newRoom.transform.localPosition = new Vector3(startCoords.x, 0f, startCoords.z);
        FillRoom(startCoords, newRoom, RandomRoomSize, roomType);
    }

    //Function that gets us a random room size from anywhere between a 2x2 up to a 10x10
    public IntVector2 RandomRoomSize
    {
        get { return new IntVector2(Random.Range(4, 30), Random.Range(4, 30)); }    
    }

    private void FillRoom(IntVector2 startCoords, DungeonRooms roomType, IntVector2 roomSize, DungeonRoomType test)
    {
        IntVector2 tempCoords = startCoords;
        IntVector2 doorCoords = startCoords;
        

        //CreateCell(startCoords, roomType);
        //Check to see if start coords are too close to edge of map (mapsize - coords) compared to room size
        int exitNum = 1;

        if ((test == DungeonRoomType.Start) || (test == DungeonRoomType.Boss))
            exitNum = 1;        
        else
            exitNum = 2;
        
        Debug.Log("roomType " + roomType.ToString());
        for (int x = 0; x < exitNum; x++)
        {
            doorCoords = SetRoomExit(startCoords, roomSize);
            //exitCells.Add(doorCoords);
        }
        
        IntVector2 roomFits = CheckRoomFits(startCoords, roomSize);
        int prefabType = 0;
        for(int x = 0; x < roomFits.x; x++)
        {
            for (int z = 0; z < roomFits.z; z++)
            {
                if (startCoords.x == doorCoords.x && startCoords.z == doorCoords.z)
                {
                    prefabType = 1;
                    Debug.Log("prefab change?");
                }
                
                startCoords.z++;
                CreateCell(startCoords, roomType, prefabType);
                z++;
                prefabType = 0;


            }
            startCoords.x++;
            startCoords.z = tempCoords.z;
            x++;
            prefabType = 0;
        }
    }

    //Function setting the coords for a room exit
    private IntVector2 SetRoomExit(IntVector2 startCoords, IntVector2 roomSize)
    {
        IntVector2 newCoords = startCoords;
        int randomVal;

        randomVal = Random.Range(newCoords.x, newCoords.x += roomSize.x);

        newCoords.x = randomVal; 


        return newCoords;
    }

    //Function making sure the room doesn't overlap edges of the dungeon. If it does shrink the room
    private IntVector2 CheckRoomFits(IntVector2 startCoords, IntVector2 roomSize)
    {
        IntVector2 newRoomSize = roomSize;
        IntVector2 newDungSize;
        newDungSize.x = size.x;
        newDungSize.z = size.z;

        //Room may not fit into coords
        if ((newDungSize.x - startCoords.x) < roomSize.x)
        {
           
            newRoomSize.x = newDungSize.x - startCoords.x;
        }        
        if ((newDungSize.z - startCoords.z) < roomSize.z)
        {
            newRoomSize.z = newDungSize.z - startCoords.z;
        }
        Debug.Log("OLD ROOM SIZE WAS " + roomSize.x + ", " + roomSize.z);
        Debug.Log("NEW ROOM SIZE " + newRoomSize.x + ", " + newRoomSize.z + "   START COORDS WERE -- " + startCoords.x + ", " + startCoords.z);
        return newRoomSize;
    }
    //Function creating the cell
    private void CreateCell(IntVector2 coordinates, DungeonRooms room, int prefabType)
    {
        //Debug.Log("Creating new Cell");
        //TODO  here is where we'll add a call to a function to determine the prefab being used as the floor tile
        DungeonCell newCell = Instantiate(cellPrefab[prefabType]) as DungeonCell;
        //Debug.Log("Cell Coords " + coordinates.x + "," + coordinates.z); 
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Dungeon Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = room.transform; //Adds it to the dungeoncell object created in the hierarchy keeps things tidier and keeps every centred.
        newCell.transform.localPosition = new Vector3(coordinates.x + 2, 0f, coordinates.z + 2);
    }

    #endregion
}
