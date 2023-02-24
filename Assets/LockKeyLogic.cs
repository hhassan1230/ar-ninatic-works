using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockKeyLogic : MonoBehaviour
{
    private SceneManager _SceneManager;
     

    // Start is called before the first frame update
    void Start()
    {
        _SceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>();   
    }
    
    void OnCollisionEnter(Collision collision)
    {
        
        //Check to see if the tag on the collider is equal to Enemy
        if (collision.gameObject.tag == "key")
        {
            _SceneManager.KeyUnlock(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
