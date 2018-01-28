using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class will handle most of the code used when generating the dungeons.
public class DungeonGenerator : MonoBehaviour {
    #region Variables
    public int dungeonSizeX, dungeonSizeZ; //X and Y ranges for the dungeon size
    public float generationStopDelay; // Allows us to slow down the generation of the dungeon to watch it happen


    //TODO make this private and add ability to change the prefab item during runtime.
    public DungeonCell cellPrefab; // Holds the cell prefab object

    private DungeonCell[,] cells; //2D array holding coordinates of the cells

    

    #endregion

    #region Functions
    public IEnumerator Generate()
    {
        //Makes sure a dungeon is always generated if the size isn't specified prior
        if (dungeonSizeX <= 0)
            dungeonSizeX = 20;
        if (dungeonSizeZ <= 0)
            dungeonSizeZ = 20;
        if (generationStopDelay <= 0)
            generationStopDelay = 0.01f;

        WaitForSeconds delay = new WaitForSeconds(generationStopDelay);  
        cells = new DungeonCell[dungeonSizeX, dungeonSizeZ]; //Sets the size of our array to the size of the dungeon
        //Loops through the x/z and creates a cell for each coord in our array
        for(int x = 0; x <dungeonSizeX; x++){
            for(int z = 0; z <dungeonSizeZ; z++)
            {
                yield return delay;
                CreateCell(x, z);
            }
        }
    }

    //Function creating the cell
    private void CreateCell(int x, int z)
    {
        //TODO  here is where we'll add a call to a function to determine the prefab being used as the floor tile
        DungeonCell newCell = Instantiate(cellPrefab) as DungeonCell;
        cells[x, z] = newCell;
        newCell.name = "Dungeon Cell " + x + ", " + z;
        newCell.transform.parent = transform; //Adds it to the dungeoncell object created in the hierarchy keeps things tidier and keeps every centred.
        newCell.transform.localPosition = new Vector3(x + 2, 0f, z + 2);
    }

    #endregion
}
