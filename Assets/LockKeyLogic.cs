using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockKeyLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (collision.gameObject.tag == "key")
        {
            Debug.Log("Triggered by Key");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
