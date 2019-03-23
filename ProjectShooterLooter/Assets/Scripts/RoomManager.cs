using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Room room;
    public Color normalColor, enterColor;
    Color mainColor;
    SpriteRenderer mainRend;
    SpriteRenderer northRend, eastRend, southRend, westRend;

    // Start is called before the first frame update
    void Start()
    {
        mainRend = GetComponent<SpriteRenderer>();

        northRend = transform.GetChild(0).GetComponent<SpriteRenderer>();
        eastRend = transform.GetChild(1).GetComponent<SpriteRenderer>();
        southRend = transform.GetChild(2).GetComponent<SpriteRenderer>();
        westRend = transform.GetChild(3).GetComponent<SpriteRenderer>();

        mainColor = normalColor;

        updateDoors();
        updateColors();
    }
    
    void updateDoors()
    {
        if (room.HasFlag(Doors.North))
        {
            northRend.enabled = true;
        }
        if (room.HasFlag(Doors.East))
        {
            eastRend.enabled = true;
        }
        if (room.HasFlag(Doors.South))
        {
            southRend.enabled = true;
        }
        if (room.HasFlag(Doors.West))
        {
            westRend.enabled = true;
        }
    }
    
    void updateColors()
    {
        if(room.type == 0)
        {
            mainColor = normalColor;
        }else if(room.type == 1)
        {
            mainColor = enterColor;
        }

        mainRend.color = mainColor;
        northRend.color = mainColor;
        eastRend.color = mainColor;
        southRend.color = mainColor;
        westRend.color = mainColor;
    }
}
