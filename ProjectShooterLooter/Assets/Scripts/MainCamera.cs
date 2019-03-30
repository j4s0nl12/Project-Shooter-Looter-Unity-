using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameObject playerObject;
    private Vector3 temp;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("PlayerManager").GetComponent<PlayerController>().controlledObject;
        temp = new Vector3();
        temp.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        }

    private void LateUpdate()
    {
        temp.x = playerObject.transform.position.x;
        temp.y = playerObject.transform.position.y;
        transform.position = temp;
    }
}
