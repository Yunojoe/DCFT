using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CorridorDirectionEnum {
    North,
    East,
    South,
    West
}

public static class CorridorDirections
{
    public const int Count = 4;

    //Chooses a random direction for the corridor to go from the enum above
    //TODO add other directions for stairs/up/down
    public static CorridorDirectionEnum RandomValue
    {
        get { return (CorridorDirectionEnum)Random.Range(0, Count); }
    }

    //an array of coordinates to hold the directions
    private static IntVector2[] vectors =
    {
        new IntVector2(0,1),
        new IntVector2(1,0),
        new IntVector2(0,-1),
        new IntVector2(-1,0)
    };

    // Returns the value of our chosen direction as a coordinate
    public static IntVector2 ToIntVector2 (CorridorDirectionEnum direction)
    {
        return vectors[(int)direction];
    }

}
