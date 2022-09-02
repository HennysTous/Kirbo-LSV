using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;
    public Canvas menuCanvas, gameOverCanvas, gameCanvas;
   


    private void Awake()
    {
        if (!sharedInstance) sharedInstance = this;
    }
    // Start is called before the first frame update

    public void ShowMainMenu() 
    {
        menuCanvas.enabled=true;

    }

    public void HideMainMenu()
    {
        menuCanvas.enabled = false;

    }

    
    public void ShowGameOver()
    {
        gameOverCanvas.enabled = true;

    }

    public void HideGameOver()
    {
        gameOverCanvas.enabled = false;

    }
    public void ShowGame()
    {
        gameCanvas.enabled = true;

    }

    public void HideGame()
    {
        gameCanvas.enabled = false;

    }
    public void ExitGame()
    {
       #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
       #else
        Application.Quit();
       #endif
    }
}
