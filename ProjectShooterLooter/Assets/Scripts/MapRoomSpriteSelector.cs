using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRoomSpriteSelector : MonoBehaviour
{
    public Sprite spU, spD, spR, spL,
                  spUD, spRL, spUR, spUL, spDR, spDL,
                  spULD, spRUL, spDRU, spLDR, spUDRL;

    public Doors doors;
    public int type;
    public Color normalColor, enterColor;
    Color mainColor;
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        mainColor = normalColor;
        PickSprite();
        PickColor();
    }

    void PickSprite()
    {

    }

    void PickColor()
    {
        if (type == 0)
            mainColor = normalColor;
        else if (type == 1)
            mainColor = enterColor;

        rend.color = mainColor;
    }
}
