using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed =4f;
    public bool shouldMoveRight =true;
    // Start is called before the first frame update
    void Start()
    {
        startPoint.position = transform.position;
        Debug.Log(startPoint.position.x+"   "+ startPoint.position.y + "   " + startPoint.position.z);
     
        Debug.Log(endPoint.position.x + "   " + endPoint.position.y + "   " + endPoint.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (shouldMoveRight)
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, Time.deltaTime * speed);
        else { 
        transform.position = Vector3.MoveTowards(transform.position,startPoint.position,  Time.deltaTime * speed);
            Debug.Log("ASASD"+startPoint.position.z+ " +"+transform.position.z);
    }
        if (transform.position == endPoint.position) shouldMoveRight = false;
    //    if (transform.position == startPoint.position) shouldMoveRight = !shouldMoveRight;
    }
}
