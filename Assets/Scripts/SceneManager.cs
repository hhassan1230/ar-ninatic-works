//Standard Unity/C# functionality
using UnityEngine;

//These tell our project to use pieces from the Lightship ARDK
using Niantic.ARDK.AR;
using Niantic.ARDK.AR.HitTest;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.Utilities;
using Niantic.ARDK.Utilities.Input.Legacy;
using System.Numerics;
using Niantic.ARDK.Networking;

//Define our main class
public class SceneManager : MonoBehaviour
{
    public bool statuePlaced = false;
    public bool keyPlaced = false;
   
    private GameObject statue;
    private GameObject key;
    private GameObject lightForest;
    private GameObject flower;


    //private GameObject _preview;
    //private UnityEngine.Vector3 target;
    //float _speed = 1.0f;
    //public GameObject _statuePreview;

    private RaycastHit rayHit;
    public GameObject _statuePrefab;
    public GameObject _keyPrefab;
    public GameObject _lightForestPrefab;
    public AudioClip _forestMusic;
    public GameObject flowerPrefab;
    
    public Camera _mainCamera;  //This will reference the MainCamera in the scene, so the ARDK can leverage the device camera
    IARSession _ARsession;  //An ARDK ARSession is the main piece that manages the AR experience

    // Start is called before the first frame update
    void Start()
    {
        //ARSessionFactory helps create our AR Session. Here, we're telling our 'ARSessionFactory' to listen to when a new ARSession is created, then call an 'OnSessionInitialized' function when we get notified of one being created
        ARSessionFactory.SessionInitialized += OnSessionInitialized;
        statuePlaced = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyPlaced == true)
        {
            if (lightForest != null)
            {
                return;
            }
            else
            {
                lightForest = Instantiate(_lightForestPrefab, statue.transform.position, statue.transform.rotation);
                flower = Instantiate(flowerPrefab, statue.transform.position - new UnityEngine.Vector3(-2.0f, -1.3f, 1.0f), transform.rotation);
                PlayMusic();
            }
        }

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

    private void PlayMusic()
    {
        // Play Music
        AudioSource _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _forestMusic;
        _audioSource.Play();
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
           statue = Instantiate(_statuePrefab, rayHit.point, transform.rotation);
           statue.transform.Rotate(0, 180, 0);

           key = Instantiate(_keyPrefab, statue.transform.position - new UnityEngine.Vector3(2f, -1.3f, 1.0f), transform.rotation);
 
           statuePlaced = true;
        }
    }

     /*private void StatuePreview()
     {
        if(Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out rayHit, 4.0f))
         {
            target += rayHit.point;

            if (_preview == null)
            {
                _preview = Instantiate(_statuePreview, rayHit.point, transform.rotation);
            }

            else
            {
                float step = _speed * Time.deltaTime;
                _preview.transform.position = UnityEngine.Vector3.MoveTowards(_preview.transform.position, target, step);
            }
         }
     }
     */
    
}