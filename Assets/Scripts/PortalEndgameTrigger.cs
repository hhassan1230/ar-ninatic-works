using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEndgameTrigger : MonoBehaviour
{
    public SceneManager _sceneManagerRef;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            _sceneManagerRef.StartReload();
        }
    }
}
