using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    float time = 1f;
    Rigidbody rigidbody;
    void Start()
    {
        rigidbody =GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("waitToFall");
    
    }

    IEnumerator waitToFall()
    {
       
        yield return new WaitForSeconds(time);
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }

}
