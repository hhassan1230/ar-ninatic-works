using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangeLogic : MonoBehaviour
{
    public string levelName;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void LoadScene()
    {
        Debug.Log("sceneName to load: " + levelName);
        // SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}
