using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    //UI Elements

    //Coin TextMeshPro

    [SerializeField] private TextMeshProUGUI coinText;

    //Default Coin Text

    [SerializeField] private string defaultCoinText;

    [SerializeField] private int totalCoins;

    //Health Slider

    [SerializeField] private Slider healthSlider;

    //StartButton

    [SerializeField] private Button startButton;

    //ExitButton

    [SerializeField] private Button exitButton;

    //RestartButton

    [SerializeField] private Button restartButton;

    //GameOverExitButton

    [SerializeField] private Button gameOverExitButton;

    //Resume Button

    [SerializeField] private Button resumeButton;

    //GameOverExitButton

    [SerializeField] private Button pauseExitButton;

    //Pause panel

    [SerializeField] private GameObject pausePanel;

    //StartMenu Panel

    [SerializeField] private GameObject startMenuPanel;

    //GameOver panel

    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameObject youWinPanel;

    [SerializeField] private Button youWinRestartButton;

    [SerializeField] private Button youWinExitButton;






    //Set Up Events

    public delegate void AudioChange(SoundController.Channel channel, float volume, bool isMuted);
    public static event AudioChange AudioChangeEvent;


    public void UpdateAudio(SoundController.Channel channel, float value, bool isOn)
    {
        if (AudioChangeEvent != null)
        {
            AudioChangeEvent(channel, value, !isOn);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        //Deactivate YouWIn Panel
        youWinPanel.SetActive(false);

        //Deactivate GameOver Panel

        gameOverPanel.SetActive(false);

        //Activate the StartMenu Panel

        startMenuPanel.SetActive(true);

        //set the coin text to 0

        coinText.text = defaultCoinText + 0;

        //Setup Button Behaviours

        startButton.onClick.AddListener(StartGame);

        exitButton.onClick.AddListener(ExitGame);

        gameOverExitButton.onClick.AddListener(ExitGame);

        restartButton.onClick.AddListener(RestartGame);

        pauseExitButton.onClick.AddListener(ExitGame);

        resumeButton.onClick.AddListener(Resume);

        youWinRestartButton.onClick.AddListener(RestartGame);

        youWinExitButton.onClick.AddListener(ExitGame);

        //Subscribe to the Gamemanager's CoinChanged event

        GameManager.instance.CoinChangedEvent += UpdateCoinText;

        //Subscribe to the Gamemanager's HealthChanged event

        GameManager.instance.HealthChangedEvent += UpdateHealthSlider;

        //Subscribe to the Gamemanager's GameOver event

        GameManager.instance.GameOverEvent += GameOver;

        //Subscribe to the Gamemanager's Pause event

        GameManager.instance.PauseGameEvent += OnPause;

        //Subscribe to the Gamemanager's YouWin event

        GameManager.instance.YouWinEvent += YouWin;



    }

    //Pause Screen

    private void OnPause(bool isPaused)

    {

        if (isPaused)

        {

            //Release the mouse

            Cursor.lockState = CursorLockMode.None;

            //Activate the GameOver Panel

            pausePanel.SetActive(true);

        }

        else

        {

            //Release the mouse

            Cursor.lockState = CursorLockMode.Locked;

            //Activate the GameOver Panel

            pausePanel.SetActive(false);

        }

    }

    // Update is called once per frame
    //Start Game

    private void StartGame()

    {

        //Lock the cursor

        Cursor.lockState = CursorLockMode.Locked;

        //Deactivate the StartMenu Panel

        startMenuPanel.SetActive(false);

        //Activate the GameManager

        GameManager.instance.StartGame();

        //Set the Max Health

        healthSlider.maxValue = GameManager.instance.playerController.GetHealth().GetMaxHealth();

        //Set the health value

        healthSlider.value = GameManager.instance.playerController.GetHealth().GetCurrentHealth();






        GameManager.instance.StartGame();

    }

    //ResumeGame

    private void Resume()

    {

        GameManager.instance.GamePaused(false);

    }

    //Game Over Sequence

    private void GameOver()

    {

        //Release the mouse

        Cursor.lockState = CursorLockMode.None;

        //Activate the GameOver Panel

        gameOverPanel.SetActive(true);

    }

    private void YouWin()

    {

        //Release the mouse

        Cursor.lockState = CursorLockMode.None;

        //Activate the YouWin Panel

        youWinPanel.SetActive(true);

    }
    //Exit Game

    private void ExitGame()

    {

        GameManager.instance.Quitgame();

    }

    //Restart Game

    private void RestartGame()

    {


        GameManager.instance.RestartGame();

    }

    private void UpdateCoinText(int coinAmount)

    {

        coinText.text = defaultCoinText + coinAmount + " / " + totalCoins.ToString();


    }

    //Update the Health Slider

    private void UpdateHealthSlider(int healthNormalized)

    {

        healthSlider.value = healthNormalized;

    }
}
