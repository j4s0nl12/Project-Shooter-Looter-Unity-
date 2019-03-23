﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject controlledObject;
    public GameObject equippedGun;
    public GameObject bullet;

    private KeyCode upKey = KeyCode.W;
    private KeyCode downKey = KeyCode.S;
    private KeyCode leftKey = KeyCode.A;
    private KeyCode rightKey = KeyCode.D;
    private KeyCode shootKey = KeyCode.Space;

    private bool isShootKeyDown = false;

    private Vector3 velocity = new Vector3();
    
    private float speed = 15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        handleRotation_Mouse();

        velocity = new Vector3();

        handleKeyInputs();

        controlledObject.transform.position += (velocity.normalized) * speed * Time.deltaTime;
        SimpleBounds();
    }

    void handleRotation_Mouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - controlledObject.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        controlledObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void handleKeyInputs()
    {
        if (Input.GetKeyDown(shootKey))
        {
            isShootKeyDown = true;
        }
        else if (Input.GetKeyUp(shootKey))
        {
            isShootKeyDown = false;
        }

        if (isShootKeyDown)
        {
            //bleh
        }

        if (Input.GetKey(upKey))
        {
            velocity.y = 1f;
        }
        else if (Input.GetKey(downKey))
        {
            velocity.y = -1f;
        }

        if (Input.GetKey(leftKey))
        {
            velocity.x = -1f;
        }
        else if (Input.GetKey(rightKey))
        {
            velocity.x = 1f;
        }
    }

    void SimpleBounds()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(controlledObject.transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);

        controlledObject.transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}