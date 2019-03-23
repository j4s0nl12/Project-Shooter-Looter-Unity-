using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    Vector2 worldSize = new Vector2(4, 4);

    Room[,] rooms;

    List<Vector2> takenPositions = new List<Vector2>();

    int gridSizeX, gridSizeY, numRooms = 20;

    public GameObject roomObj;

    void Start()
    {
        //Make sure there aren't more rooms that you can fit in the grid
        if (numRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        {
            numRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }

        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);

        CreateRooms();
        SetRoomDoors();
        DrawMap();
    }

    void DrawMap()
    {
        foreach(Room room in rooms)
        {
            if (room == null)
                continue;
            Vector2 drawPos = room.gridPos;
            drawPos.x *= 16;
            drawPos.y *= 8;
            RoomManager temp = Object.Instantiate(roomObj, drawPos, Quaternion.identity).GetComponent<RoomManager>();
            temp.room = room;
        }
    }

    void CreateRooms()
    {
        //Initial Setup
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;

        //Randomization numbers
        float randomCompare = 0.2f;
        float randomCompareStart = 0.2f;
        float randomCompareEnd = 0.1f;

        //Add randomized rooms
        for (int i = 0; i < numRooms - 1; i++)
        {
            float randomPerc = ((float)i) / (((float)numRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            //grab new position
            checkPos = NewPosition();
            //test new position
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do{
                    checkPos = NewPosition(true);
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
            }
            //finalize position
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 0);
            takenPositions.Insert(0, checkPos);
        }
    }

    Vector2 NewPosition(bool isSelective = false)
    {
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (isSelective && (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100));

            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            
            int val = 1;
            if (!positive)
                val *= -1;
            
            if (UpDown)
            {
                y += val;
            }
            else
            {
                x += val;
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        return checkingPos;
    }

    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0;
        if (usedPositions.Contains(checkingPos + Vector2.right))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }
        return ret;
    }

    void SetRoomDoors()
    {
        for (int x = 0; x < ((gridSizeX * 2)); x++)
        {
            for (int y = 0; y < ((gridSizeY * 2)); y++)
            {
                if (rooms[x,y] == null)
                {
                    continue;
                }

                //Check above
                if (y - 1 >= 0)
                {
                    rooms[x,y].SetFlag(BoolToDoorInt(Doors.South, (rooms[x,y - 1] != null)));
                }
                if (y + 1 < gridSizeY * 2)
                {
                    rooms[x,y].SetFlag(BoolToDoorInt(Doors.North, (rooms[x, y + 1] != null)));
                }
                if (x - 1 >= 0)
                {
                    rooms[x,y].SetFlag(BoolToDoorInt(Doors.West, (rooms[x - 1, y] != null)));
                }
                if (x + 1 < gridSizeX * 2)
                {
                    rooms[x, y].SetFlag(BoolToDoorInt(Doors.East, (rooms[x + 1, y] != null)));
                }
            }
        }
    }

    Doors BoolToDoorInt(Doors flag, bool test)
    {
        if (test)
            return flag;
        return Doors.None;
    }
}
