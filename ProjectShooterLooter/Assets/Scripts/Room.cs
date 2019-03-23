using UnityEngine;

[System.Flags] public enum Doors
{
    None  = 0,
    North = 1,
    South = 2,
    East  = 4,
    West  = 8
}

public class Room
{
    public Vector2 gridPos;

    public int type;

    private Doors doors;

    public Room(Vector2 _gridPos, int _type)
    {
        gridPos = _gridPos;
        type = _type;
    }

    public Doors getDoors()
    {
        return doors;
    }

    public void SetFlag(Doors flag)
    {
        doors |= flag;
    }

    public void UnsetFlag(Doors flag)
    {
        doors &= (~flag);
    }

    public bool HasFlag(Doors flag)
    {
        return (doors & flag) == flag;
    }
}
