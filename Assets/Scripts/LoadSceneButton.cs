using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    public void LoadVuforiaScene()
    {
        SceneControllerAR.Instance.LoadVuforiaScene();
    }

    public void LoadARFoundationScene()
    {
        SceneControllerAR.Instance.LoadARFoundationScene();
    }

    public void Back()
    {
        SceneControllerAR.Instance.BackToMenu();
    }
}
