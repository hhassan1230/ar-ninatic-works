using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockKeyLogic : MonoBehaviour
{
    private SceneManager _sceneManager;
    public GameObject keyParticles;
    public GameObject placeKeyText;
    public GameObject placeFlowerText;

    // Start is called before the first frame update
    void Start()
    {
        _sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>();
        print("found manager");
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (collision.gameObject.tag == "key")
        {
            _sceneManager.keyPlaced = true;
            print("I'm colliding with " + collision.gameObject.name);
            keyParticles.SetActive(true);
            Destroy(collision.gameObject);
            placeKeyText.SetActive(false);
            placeFlowerText.SetActive(true);
        }
    }
}
