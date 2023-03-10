using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GodOfferingLogic : MonoBehaviour
{
    private SceneManager _sceneManager;
    public GameObject godParticles;
    public GameObject newFlowerObj;
    public Transform flowerLocPos;
    public GameObject godText;


    // Start is called before the first frame update
    void Start()
    {
        _sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>();
        print("Found Manager");
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (collision.gameObject.tag == "Flower")
        {
            //_sceneManager.keyPlaced = true;
            print("I'm colliding with " + collision.gameObject.name);
            godParticles.SetActive(true);
            Destroy(collision.gameObject);
            Instantiate(newFlowerObj, flowerLocPos.position, flowerLocPos.rotation);

            godText.SetActive(true);

            _sceneManager.ReloadDemo();


        }
    }
}
