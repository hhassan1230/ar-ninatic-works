//Standard Unity/C# functionality
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//These tell our project to use pieces from the Lightship ARDK
using Niantic.ARDK.AR;
using Niantic.ARDK.AR.Configuration;
using Niantic.ARDK.AR.Mesh;
using Niantic.ARDK.Utilities;
using Niantic.ARDK.Utilities.Collections;
using Niantic.ARDK.Utilities.Logging;
using Niantic.ARDK.AR.HitTest;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.Utilities.Input.Legacy;
using System.Numerics;
using Niantic.ARDK.Networking;
using UnityEngine.SceneManagement;
using Niantic.ARDK.Extensions.Meshing;

//Define our main class
public class SceneManager : MonoBehaviour
{
    private GameObject playerCamera;

    public bool statuePlaced = false;
    public bool keyPickedUp = false;
    public bool keyPlaced = false;
    public bool flowerPickedUp = false;
    public bool flowerPlaced = false;
    public bool portalSummoned = false;

    public GameObject portalTrigger;
    public GameObject statueMesh;

    public GameObject rockGroup;
    private GameObject key;
    public GameObject lightForest;
    private GameObject flower;
    private AudioSource _audioSource;
    private Transform statuePos;

    public Transform[] keySpawnpoints;
    public Transform[] flowerSpawnpoints;

    // AUDIO
    public AudioClip _forestMusic;
    public AudioClip _StatuePlacementMusic;
    public AudioClip _itemPickUpSound;
    public AudioClip _keyPlacementSound;
    public AudioClip _flowerPlacementSound;

    private RaycastHit rayHit;

    public GameObject statue;
    public GameObject _keyPrefab;
    public GameObject _lightForestPrefab;
    public GameObject flowerPrefab;
    public ParticleSystem LecturnParticles;
    public GameObject endGamePortal;

    // LECTURN TEXT
    public GameObject findAndPlaceKeyText;
    public GameObject mayYourJourney;
    public GameObject findAnOffering;
    public GameObject chooseYourDeityText;
    public GameObject endingText;

    public Camera _mainCamera;  //This will reference the MainCamera in the scene, so the ARDK can leverage the device camera
    IARSession _ARsession;  //An ARDK ARSession is the main piece that manages the AR experience

    public ARMeshManager _ARMeshManager;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        portalTrigger.SetActive(false);

        //ARSessionFactory helps create our AR Session. Here, we're telling our 'ARSessionFactory' to listen to when a new ARSession is created, then call an 'OnSessionInitialized' function when we get notified of one being created
        ARSessionFactory.SessionInitialized += OnSessionInitialized;
        statuePlaced = false;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //If there is no touch, we're not going to do anything
        if (PlatformAgnosticInput.touchCount <= 0)
        {
            return;
        }

        //If we detect a new touch, call our 'TouchBegan' function
        var touch = PlatformAgnosticInput.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
                TouchBegan(touch);
        }
    }

    public void PlayLecturnParticles()
    {
        LecturnParticles.Play();
    }

    private void PlayMusic()
    {
        _audioSource.Play();
    }

    public void PlayFlowerSound()
    {
        _audioSource.PlayOneShot(_flowerPlacementSound);
    }

    //This function will be called when a new AR Session has been created, as we instructed our 'ARSessionFactory' earlier
        private void OnSessionInitialized(AnyARSessionInitializedArgs args)
    {
        //Now that we've initiated our session, we don't need to do this again so we can remove the callback
        ARSessionFactory.SessionInitialized -= OnSessionInitialized;

        //Here we're saving our AR Session to our '_ARsession' variable, along with any arguments our session contains
        _ARsession = args.Session;
    }

    //This function will be called when the player touches the screen. For us, we'll have this trigger the shooting of our ball from where we touch.
    private void TouchBegan(Touch touch)
    {
        if(statuePlaced == true)
        {
            return;
        }

        //Spawn statue.
        if(Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out rayHit, 4.0f))
        {
            PlaceStatue();
        }
    }

    void PlaceStatue()
    {
        statue.SetActive(true);
        _audioSource.PlayOneShot(_StatuePlacementMusic);
        PlayLecturnParticles();
        statue.transform.position = rayHit.point + new UnityEngine.Vector3(0, -0.015f, 0);
        statue.transform.rotation = new UnityEngine.Quaternion(0, 180, 0, 0);

        //statue.transform.LookAt(_mainCamera.transform);

        //statue.transform.rotation = new UnityEngine.Quaternion(0, statue.transform.rotation.y, statue.transform.rotation.z, 0);


        // USED FOR PORTAL LOCATION LATER.
        statuePos = statue.transform;

        key = Instantiate(_keyPrefab, keySpawnpoints[Random.Range(0, 5)].position, transform.rotation);
        statuePlaced = true;

        _ARMeshManager.UseInvisibleMaterial = true;
    }

    public void PlayItemPickUp()
    {
        _audioSource.PlayOneShot(_itemPickUpSound);
    }

    public void KeyPlaced()
    {
        keyPlaced = true;
        findAndPlaceKeyText.SetActive(false);
        _audioSource.PlayOneShot(_keyPlacementSound);
        mayYourJourney.SetActive(true);
        PlayLecturnParticles();

        StartCoroutine(MayYourJourneyAndFindAnOffering());
    }

    IEnumerator MayYourJourneyAndFindAnOffering()
    {
        yield return new WaitForSeconds(11f);
        mayYourJourney.SetActive(false);
        findAnOffering.SetActive(true);
        flower = Instantiate(flowerPrefab, flowerSpawnpoints[Random.Range(0, 5)].position, transform.rotation);
        yield return new WaitForSeconds(1);
    }

    public void FlowerPickedUp()
    {
        findAnOffering.SetActive(false);
        flowerPickedUp = true;
        PlayLecturnParticles();
        StartCoroutine(ChooseYourDeityAndLightForestAppearance());
    }

    // ON TRIGGER ENTER FOR LECTURN
    IEnumerator ChooseYourDeityAndLightForestAppearance()
    {
        chooseYourDeityText.SetActive(true);
        yield return new WaitForSeconds(6);

        // LIGHT FOREST APPEARS
        rockGroup.SetActive(false);
        lightForest = Instantiate(_lightForestPrefab, statue.transform.position, statue.transform.rotation);
        PlayMusic();
    }

    public void OfferingPlaced()
    {
        _audioSource.PlayOneShot(_flowerPlacementSound);
        chooseYourDeityText.SetActive(false);
        endingText.SetActive(true);
        portalTrigger.SetActive(true);
        PlayLecturnParticles();
    }

    public void EndGameSequence()
    {
        statueMesh.SetActive(false);

        endGamePortal.SetActive(true);
    }

    public void StartReload()
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        // FADE OUT STARTS HERE!
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
    
}