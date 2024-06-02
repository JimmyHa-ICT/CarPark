using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SCENE
{
    START = 0,
    INGAME = 1,
    LOSE = 2,
    WIN = 3
}

public class SceneController : MonoBehaviour
{
    public static SceneController SharedInstance;
    public SCENE CurrentScene;

    private void Awake()
    {
        if (SharedInstance == null)
            SharedInstance = this;
        else if (SharedInstance != this)
            Destroy(gameObject);
        Application.targetFrameRate = 60;
    }

    public void ChangeScene(int s)
    {
        SceneManager.LoadScene(s);
    }
}

