using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private RaycastHit rayHit;
    public float rayLength = 1.0f;
    public Transform pickupLoc;
    public SceneManager _sceneManagerRef;
   

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
                    if (_sceneManagerRef.keyPickedUp == false)
                    {
                        rayHit.collider.transform.parent = pickupLoc;
                        rayHit.collider.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
             
                        _sceneManagerRef.PlayItemPickUp();
                        _sceneManagerRef.keyPickedUp = true;
                    }
            }

            if (rayHit.collider.gameObject.tag == "Flower")
            {
                if(_sceneManagerRef.flowerPickedUp == false)
                {
                    rayHit.collider.transform.parent = pickupLoc;
                    rayHit.collider.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
                    _sceneManagerRef.PlayItemPickUp();
                    _sceneManagerRef.FlowerPickedUp();
                }    
            }
        }

        Debug.DrawRay(transform.position, transform.forward, Color.blue);
          
    }
}
