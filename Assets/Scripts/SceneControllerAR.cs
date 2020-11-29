using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class SceneControllerAR : MonoBehaviour
{
    private static SceneControllerAR _instance = null;

    const int MENU = 0;
    const int VUFORIA_SCENE = 1;
    const int ARFOUNDATION_SCENE = 2;

    public static SceneControllerAR Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneControllerAR>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadARFoundationScene()
    {
        SceneManager.LoadSceneAsync(ARFOUNDATION_SCENE);
    }

    public void LoadVuforiaScene()
    {
        VuforiaRuntime.Instance.InitVuforia();
        SceneManager.LoadSceneAsync(VUFORIA_SCENE);
    }

    public void BackToMenu()
    {
        /*if (VuforiaRuntime.Instance.InitializationState == VuforiaRuntime.InitState.INITIALIZED)
        {
            VuforiaRuntime.Instance.Deinit();
        }*/
        SceneManager.LoadSceneAsync(MENU);
        
    }
}
