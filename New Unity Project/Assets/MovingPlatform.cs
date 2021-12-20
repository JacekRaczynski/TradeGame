using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPointa;
    public float speed =4f;
    public bool shouldMoveRight =true;
    private PlayerControllerLevel1 player;
    // Start is called before the first frame update
    void Start()
    {
        startPoint.position = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
   
        if (shouldMoveRight) {
            transform.position = Vector3.MoveTowards(transform.position, endPointa.position, Time.deltaTime * speed);
            //  player.transform.position = Vector3.MoveTowards(transform.position, endPoint.position, Time.deltaTime * speed);
            if (player != null && !player.IsLock)
            {
                Debug.Log("!!!!!!!!````````1````!!!!!!!!");
                player.transform.position = Vector3.MoveTowards(player.transform.position, endPointa.position, Time.deltaTime * speed);
            }
        }
        else { 
        transform.position = Vector3.MoveTowards(transform.position,startPoint.position,  Time.deltaTime * speed);
            //     player.transform.position = Vector3.MoveTowards(transform.position,startPoint.position,  Time.deltaTime * speed);
            if (player != null && !player.IsLock)
            {
                Debug.Log("!!!!!!!!!!!``````````2```````!!!!!");
                player.transform.position = Vector3.MoveTowards(player.transform.position, startPoint.position, Time.deltaTime * speed);
            }


        }
       if (transform.position == startPoint.position || transform.position == endPointa.position) shouldMoveRight = !shouldMoveRight;
        if (player != null && !player.IsLock)
        {   // Debug.Log("!!!!!!!!!!!!!!!!");
          //  player.transform.position = transform.position;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        var playerTrigger = other.GetComponent<PlayerControllerLevel1>();
        if (playerTrigger != null)
        {
            var height = transform.GetComponentInChildren<MeshRenderer>().bounds.extents.y;
            var transformPlatformY = other.transform.position.y +  height - 0.12f;
            if (other.transform.position.y < transformPlatformY)
            {
                player = playerTrigger;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        var playerTrigger = other.GetComponent<PlayerControllerLevel1>();
        if (playerTrigger != null && player != null)
        {
            player = null; 
        }

    }


}
