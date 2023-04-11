
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour

{
    //Set up as a Singleton
    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if(_instance == null)
            {
                 _instance = FindObjectOfType<GameManager>();
                if(_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public delegate void GameStarted();
    public event GameStarted GameStartedEvent;
    public delegate void GameOver();
    public event GameOver GameOverEvent;
    public delegate void YouWin();
    public event YouWin YouWinEvent;
    public delegate void PauseGame(bool isPaused);
    public event PauseGame PauseGameEvent;
    public delegate void CoinChanged(int value);
    public event CoinChanged CoinChangedEvent;
    public delegate void HealthChanged(int value);
    public event HealthChanged HealthChangedEvent;

    //Set Up Callbacks

    //Start Game



    public void StartGame()

    {

        if (GameStartedEvent != null)

        {

            GameStartedEvent();

        }

    }

    //Update Coin Amount

    public void UpdateCoins(int amount)

    {

        if (CoinChangedEvent != null)

        {

            CoinChangedEvent(amount);

        }

    }

    //Update Health

    public void UpdateHealth(int health)

    {

        if (HealthChangedEvent != null)

        {

            HealthChangedEvent(health);

        }

    }




    //Initiate GameOver

    public void GameOverSequence()

    {

        if (GameOverEvent != null)

        {

            GameOverEvent();

        }

    }

    //Pause Game

    public void GamePaused(bool isPaused)

    {

        if (PauseGameEvent != null)

        {

            PauseGameEvent(isPaused);

        }

    }

    public void YouWinSequence()

    {

        if (YouWinEvent != null)

        {

            YouWinEvent();

        }

    }

    private void Awake()
    {
        _instance = this;
    }
    //Reference to the PlayerController
    public PlayerController playerController { get; private set; }

    //Set the player controller via reference
    public void SetPlayerController(PlayerController controller)
    {
        playerController = controller;
    }

    //Application Methods
    //Restart the Game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void Quitgame()
    {
        Application.Quit();
    }

   
}

