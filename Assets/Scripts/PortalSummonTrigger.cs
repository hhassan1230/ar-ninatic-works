using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSummonTrigger : MonoBehaviour
{
    public SceneManager _sceneManagerRef;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("MainCamera"))
        {
            if(_sceneManagerRef.portalSummoned == false)
            {
                _sceneManagerRef.lightForest.SetActive(false);
                _sceneManagerRef.portalSummoned = true;
                _sceneManagerRef.EndGameSequence();
            }
        }
    }
}
