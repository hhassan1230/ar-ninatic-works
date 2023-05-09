using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockKeyLogic : MonoBehaviour
{
    private SceneManager _sceneManager;
    public GameObject keyParticles;

    // Start is called before the first frame update
    void Start()
    {
        _sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (collision.gameObject.tag == "key")
        {
            keyParticles.SetActive(true);
            Destroy(collision.gameObject);
            _sceneManager.PlayItemPickUp();
        }
    }
}
