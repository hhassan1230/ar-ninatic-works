using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangeLogic : MonoBehaviour
{
    public string levelName;

    public GameObject TitleText;
    public GameObject ARInfoText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SwapTitleCards()
    {
        TitleText.SetActive(false);
        ARInfoText.SetActive(true);
        StartCoroutine(WaitAndChangeLevel());
    }
    
    IEnumerator WaitAndChangeLevel()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        LoadScene();
    }

    public void LoadScene()
    {
        // SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}
