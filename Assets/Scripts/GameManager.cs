using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{

    public GameState currentGameState;

    public static GameManager sharedInstance;

    private playerController controller;

    public int collectedObject = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    void Start()
    {
        currentGameState = GameState.menu;
        MenuManager.sharedInstance.HideGameOver();
        MenuManager.sharedInstance.HideGame();
        controller = GameObject.FindWithTag("Player").GetComponent<playerController>();

    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void StartGame() 
    {

        SetGameState(GameState.inGame);

    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            MenuManager.sharedInstance.ShowMainMenu();
            
        }
        else if(newGameState == GameState.inGame)
        {
            LevelManager.sharedInstance.RemoveAllLevelBlocks();
            Invoke("ReloadLevel", 0.1f);
            MenuManager.sharedInstance.HideMainMenu();
            MenuManager.sharedInstance.HideGameOver();
            MenuManager.sharedInstance.ShowGame();

        }
        else if(newGameState== GameState.gameOver)
        {
            MenuManager.sharedInstance.ShowGameOver();
            MenuManager.sharedInstance.HideMainMenu();
            MenuManager.sharedInstance.HideGame();
        }

        this.currentGameState = newGameState;
    }

    void ReloadLevel()
    {
        
        LevelManager.sharedInstance.GenerateInitialLevelBlocks();
        controller.StartGame();
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
    }
}
