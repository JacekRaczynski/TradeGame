using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Camera : MonoBehaviour
{
    private Vector3 initPosition;
    private Vector3 initRotation;

    [SerializeField]
    [Tooltip("Camera rotation is enable")]
    private bool enableRotation = true;
    
    [SerializeField]
    private float movemenSpeed = 4f;

    private float smoothSpeed = 0.125f;

    [SerializeField]
    [Tooltip("Velocity of camera zooming in/out")]
    private float translationSpeed = 55f;

    [SerializeField]
    [Tooltip("Acceleration at camera movement is active")]
    private bool enableSpeedAcceleration = true;

    public Vector3 offset;
    public Vector3 desiredPosition;
    public Vector3 smoothedPosition;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        initRotation = transform.eulerAngles;
      
    }

    void LateUpdate()
    {
        desiredPosition = target.position + offset;
       
         smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed );
        transform.position = smoothedPosition;
    }

}
