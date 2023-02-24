using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockKeyLogic : MonoBehaviour
{
    private SceneManager _SceneManager;
    public ParticleSystem keyParticles;


    // Start is called before the first frame update
    void Start()
    {
        _SceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>();
        print("found manager");
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (collision.gameObject.tag == "key")
        {
            print("I'm colling with " + collision.gameObject.name);
            keyParticles.Play();
            Destroy(collision.gameObject);
        }
    }
}
