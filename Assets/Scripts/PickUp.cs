using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private RaycastHit rayHit;
    public float rayLength = 3.0f;
    public Transform pickupLoc;
   

    private void Update()
    {
        Observe();
    }

    void Observe()
    {
        if(Physics.Raycast(transform.position, transform.forward, out rayHit, rayLength))
            {
                if(rayHit.collider.gameObject.tag == "key")
                {
                rayHit.collider.transform.parent = pickupLoc;
                }
            }

        Debug.DrawRay(transform.position, transform.forward, Color.blue);
            
    }
}
