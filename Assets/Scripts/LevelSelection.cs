using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void PlayLevel1()
    {
        
        SceneManager.LoadSceneAsync("Ingame");
    }

    public void Return()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
