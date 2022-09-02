using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager sharedInstance;
    public Canvas gameOverCanvas;

    private void Awake()
    {
        if (!sharedInstance) sharedInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ShowGameOver()
    {
        gameOverCanvas.enabled = true;

    }

    public void HideGameOver()
    {
        gameOverCanvas.enabled = false;

    }
}
