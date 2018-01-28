using System.Collections;
using UnityEngine;

public class mapGenerator : MonoBehaviour {

    public int sizeX, sizeZ;

    public dungeonCell cellPrefab;

    private dungeonCell[,] cells;

    public void Generate()
    {
        cells = new dungeonCell[sizeX, sizeZ];
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                CreateCell(x, z);
            }
        }
    }

    private void CreateCell(int x, int z)
    {
        dungeonCell newCell = Instantiate(cellPrefab) as dungeonCell;
        cells[x, z] = newCell;
        newCell.name = "Maze Cell " + x + ", " + z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(x - sizeX * 0.5f + 0.5f, 0f, z - sizeZ * 0.5f + 0.5f);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
