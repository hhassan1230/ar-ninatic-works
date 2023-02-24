using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private RaycastHit rayHit;
    public float rayLength = 3.0f;
   

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

                }
            }

        Debug.DrawRay(transform.position, transform.forward, Color.blue);
            
    }
}
