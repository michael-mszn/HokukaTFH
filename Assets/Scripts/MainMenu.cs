using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject helpMenu;
    
    public void SelectLevel()
    {
        
        SceneManager.LoadSceneAsync("LevelSelection");
    }

    public void Help()
    {
        helpMenu.SetActive(true);
    }

    public void CloseHelpScreen()
    {
        helpMenu.SetActive(false);
    }
}
