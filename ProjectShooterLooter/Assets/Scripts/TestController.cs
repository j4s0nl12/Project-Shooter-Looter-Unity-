using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public GameObject controlledObject;
    private DynamicGameObject2D controlledObjectScript;

    // Start is called before the first frame update
    void Start()
    {
        controlledObjectScript = controlledObject.GetComponent<DynamicGameObject2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 target = new Vector2(mousePos.x, mousePos.y);
            controlledObjectScript.setRotateToTarget(target);
            controlledObjectScript.setPositionTarget(target);
        }
    }
}
