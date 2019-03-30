using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags] public enum BulletType
{
    Single       = 0,  //Nothing special
    Bouncing     = 1,  //Bounces on obstacles
    Piercing     = 2,  //Pierces through enemies hit
    Exploding    = 4,  //AoE explosion on enemies hit
    Burning      = 8,  //Stacking damage over time debuff
    Bleeding     = 16, //Stacking damage over time when moving debuff
    Freezing     = 32, //Stacking movement debuff, then freezes (no action can be taken)
    Acid         = 64, //Stacking debuff that increases damage taken
    Electrifying = 128 //
}

//Defensive Notes:
//

public class Bullet : MonoBehaviour
{

    private Vector3 dir;
    private float damage;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    public void init(Vector3 _dir, float _damage, float _speed)
    {
        dir = _dir;
        damage = _damage;
        speed = _speed;
    }
}
