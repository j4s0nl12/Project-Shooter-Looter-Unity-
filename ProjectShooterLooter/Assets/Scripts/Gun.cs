enum GunClass
{
    Pistol,
    SMG,
    Shotgun,
    Assault_Rifle,
    Sniper_Rifle
}

enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public class Gun
{
    private GunClass gunClass;

    private Rarity rarity;

    //Notes: No damage types, just effects:
    // Burn:    Stacking damage over time                  
    // Bleed:   Stacking damage while moving               
    // Chilled: Movement speed reduction                   
    // Frozen:  Actions restricted                         
    // Fear:    Forced movement away from origin           
    // Acid:    Stacking debuff that increases damage taken

    private float damage;   //damage per bullet
    private float fireRate; //bullets per sec
    private float accuracy; //determines spread
    private float reloadSpd;//how long it takes to reload
    private float magazine; //how many bullets can be fired before reloading

    private Gun()
    {

    }

}
