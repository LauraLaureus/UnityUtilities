using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaureusUtils.SceneManagement;

public class SceneManagerBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}


    public void SceneLoadByName(string name, bool async, UnityEngine.SceneManagement.LoadSceneMode mode = UnityEngine.SceneManagement.LoadSceneMode.Single,  ActionOnSceneLoaded onSceneLoadDo = null)
    {
        if (!async)
        {
            SceneManagementController.LoadSceneByName(name, mode, onSceneLoadDo);
        }
        else
        {
            SceneManagementController.LoadSceneAsyncByName(name, mode, onSceneLoadDo);

        }
    }
	
}
