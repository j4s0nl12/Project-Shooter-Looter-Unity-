using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags] enum ObjectState
{
    Still    = 0,
    Moving   = 1,
    Rotating = 2,
    Immobile = 4  //Stunned, Frozen, etc
}

public class DynamicGameObject2D : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;

    private ObjectState state;

    private Vector2 lastTargetPosition;//used for lerping to Vector2 for position;
    private float currentAngle;
    private float angleToRotateTo;

    private float rotateTimer;
    private float rotateMaxTime;

    [SerializeField]
    private float moveSpeed = 0;
    [SerializeField]
    private float distanceThreshold = 0.2f;
    [SerializeField]
    private float rotateSpeed = 0; //Degree per second
    [SerializeField]
    private bool mustRotateBeforeMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        state = ObjectState.Still;
    }

    // Update is called once per frame
    void Update()
    {
        //Handle Rotation
        if (HasFlag(ObjectState.Rotating))
        {
            rotateTimer += Time.deltaTime;
            float temp = Mathf.LerpAngle(currentAngle, angleToRotateTo, rotateTimer/rotateMaxTime);
            transform.rotation = Quaternion.Euler(0, 0, temp);

            if (rotateTimer >= rotateMaxTime)
                UnsetFlag(ObjectState.Rotating);
        }

        //Handle Moving
        if (HasFlag(ObjectState.Moving) && 
           (!mustRotateBeforeMoving || (mustRotateBeforeMoving && !HasFlag(ObjectState.Rotating))))
        {
            Vector2 dir = (lastTargetPosition - getVector2Position()).normalized;
            rb2d.AddForce(dir * moveSpeed * Time.deltaTime);

            float dist = Vector2.Distance(lastTargetPosition, getVector2Position());
            //Debug.Log(dist);
            if (dist <= distanceThreshold)
                UnsetFlag(ObjectState.Moving);
        }
    }

    //mvSpd at 0 means to use previous set moveSpeed
    public void setPositionTarget(Vector2 target, float mvSpd = 0)
    {
        if(mvSpd > 0)
            moveSpeed = mvSpd;

        lastTargetPosition = target;

        if (!HasFlag(ObjectState.Moving))
            SetFlag(ObjectState.Moving);
    }

    //rotSpd at  < 0 means to use previous set rotateSpeed
    //rotSpd at == 0 means immediately set rotation
    public void setRotateToTarget(Vector2 target, float rotSpd = -1)
    {
        if(rotSpd > 0)
            rotateSpeed = rotSpd;

        currentAngle = transform.rotation.eulerAngles.z;
        Vector2 dir = (target - getVector2Position()).normalized;
        angleToRotateTo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        if (angleToRotateTo < 0)
            angleToRotateTo += 360;
        rotateTimer = 0;
        
        float tmp = Mathf.Abs(Mathf.DeltaAngle(currentAngle, angleToRotateTo));
        rotateMaxTime = tmp / rotateSpeed;

        if (rotateSpeed == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, angleToRotateTo);
            UnsetFlag(ObjectState.Rotating);
        } else if (!HasFlag(ObjectState.Rotating))
            SetFlag(ObjectState.Rotating);
    }

    public Vector2 getVector2Position()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        return pos;
    }

    private void SetFlag(ObjectState flag)
    {
        state |= flag;
    }

    private void UnsetFlag(ObjectState flag)
    {
        state &= (~flag);
    }

    private bool HasFlag(ObjectState flag)
    {
        return (state & flag) == flag;
    }
}
