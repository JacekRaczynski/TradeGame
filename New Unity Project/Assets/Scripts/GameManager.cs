using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        GS_PAUSEMENU,
        GS_GAME,
        GS_LEVELCOMPLETED,
        GS_GAME_OVER
    }
    public GameState currentGameState;
    public static GameManager instance;
    public Canvas inGameCanvas;
    public Canvas menuCanvas;
    public Canvas pauseCanvas;
    public Canvas levelCompletedCanvas;
    public Canvas settingsCanvas;
    [SerializeField]
    private TMPro.TextMeshProUGUI coinsText;
    [SerializeField]
    private TMPro.TextMeshProUGUI timesText;
    [SerializeField]
    private TMPro.TextMeshProUGUI highScoreText;
    [SerializeField]
    private TMPro.TextMeshProUGUI ScoreText;
    private int coins = 0;
    private int lives = 3;
    private int keys = 0;
    private int maxKey = 3;
    public bool keysCompleted;
    [SerializeField]
    private Image[] keysTab;
    [SerializeField]
    private Image[] livesTab;



    private float timer = 0;
    public bool asda = false;
    void Start()
    {
        for (int i = keys; i < keysTab.Length; i++)
            keysTab[i].color = Color.grey;
        for (int i = 0; i < lives; i++)
            livesTab[i].enabled = true;
        for (int i = lives; i < livesTab.Length; i++)
            livesTab[i].enabled = false;
        if (PlayerPrefs.GetInt("Control", 0) == 0)
        {
            GameObject.Find("ControlArrows").active = true;
            GameObject.Find("ControlJoystick").active = false;
        } else if (PlayerPrefs.GetInt("Control", 0) == 1)
        {
            GameObject.Find("ControlArrows").active = false;
            GameObject.Find("ControlJoystick").active = true;
        }
    }

    private void Awake()
    {
        instance = this;
        InGame();
        if (!PlayerPrefs.HasKey("HighscoreLevel1"))
            PlayerPrefs.SetInt("HighscoreLevel1", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.GS_PAUSEMENU)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                InGame();
        }
        if (currentGameState == GameState.GS_GAME)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseMenu();
            timer += Time.deltaTime;
            timesText.text = string.Format("{00:00}:{1:00}", (int)(timer / 59), timer % 59);

        }
    }

    public void addCoins()
    {
        coins++;
        coinsText.text = coins.ToString();
    }
    public void addLives()
    {
        lives++;
    }
    public void addKeys()
    {
        keysTab[keys++].color = Color.white;
        if (keys == keysTab.Length) keysCompleted = true;
    }
    public void subTractLives()
    {
        lives--;
    }
    
    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        inGameCanvas.enabled = (currentGameState == GameState.GS_GAME);
        pauseCanvas.enabled = (currentGameState == GameState.GS_PAUSEMENU);
        levelCompletedCanvas.enabled = (currentGameState == GameState.GS_LEVELCOMPLETED);
        if(currentGameState == GameState.GS_LEVELCOMPLETED)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if(currentScene.name == "Level1")
            {
                int score = 100;
                if(score> PlayerPrefs.GetInt("HighscoreLevel1"))
                    PlayerPrefs.SetInt("HighscoreLevel1", score);
                highScoreText.text = "Highscore:" + PlayerPrefs.GetInt("HighscoreLevel1");
                ScoreText.text = "score:" + score;
            }
        }
    }    

    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
    }    
    public void GameOver()
    {
        SetGameState(GameState.GS_GAME_OVER);
    }
    public void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSEMENU);
    }
  
    public void LevelCompleted()
    {
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    public void OnResumeButtonClick()
    {
        InGame();
    }
    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnNextLevelButtonClicked()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
