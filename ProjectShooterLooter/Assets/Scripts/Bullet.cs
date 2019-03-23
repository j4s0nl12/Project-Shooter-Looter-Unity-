using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
