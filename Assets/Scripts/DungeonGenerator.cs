using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will handle most of the code used when generating the dungeons.
public class DungeonGenerator : MonoBehaviour {
    #region Variables
    //public int dungeonSizeX, dungeonSizeZ; //X and Y ranges for the dungeon size
    public float generationStopDelay; // Allows us to slow down the generation of the dungeon to watch it happen


    //TODO make this private and add ability to change the prefab item during runtime.
    public DungeonCell cellPrefab; // Holds the cell prefab object

    private DungeonCell[,] cells; //2D array holding coordinates of the cells

    public IntVector2 size;

    #endregion

    #region Functions
    public IEnumerator Generate()
    {
        //Makes sure a dungeon is always generated if the size isn't specified prior
        if (size.x == 0)
            size.x = 20;
        if (size.z == 0)
            size.z = 20;
        if (generationStopDelay <= 0)
            generationStopDelay = 0.01f;

        WaitForSeconds delay = new WaitForSeconds(generationStopDelay);
        cells = new DungeonCell[size.x, size.z]; //Sets the size of our array to the size of the dungeon
        IntVector2 coordinates = RandomCoordinates;
        while (ContainsCoordinates(coordinates))
        {
            yield return delay;
            CreateCell(coordinates);
            coordinates.z += 1;
        }
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

    //Function creating the cell
    private void CreateCell(IntVector2 coordinates)
    {
        //TODO  here is where we'll add a call to a function to determine the prefab being used as the floor tile
        DungeonCell newCell = Instantiate(cellPrefab) as DungeonCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Dungeon Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform; //Adds it to the dungeoncell object created in the hierarchy keeps things tidier and keeps every centred.
        newCell.transform.localPosition = new Vector3(coordinates.x + 2, 0f, coordinates.z + 2);
    }

    #endregion
}
