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
    public Canvas gameOverCanvas;
    [SerializeField]
    private TMPro.TextMeshProUGUI coinsText;  // during gamne (BronzeCoin)
    [SerializeField]
    private TMPro.TextMeshProUGUI timesText; //during game
    [SerializeField]
    private TMPro.TextMeshProUGUI highScoreText; //highest score on this lvl
    [SerializeField]
    private TMPro.TextMeshProUGUI ScoreText; //score by lvl
    [SerializeField]
    private HandleSystem handleSystem;
    private int coinBronze = 0;
    private int coinSilver = 0;
    private int coinGold = 0;
    private int lives = 3;
    private int keys = 0;
    private int maxKey = 3;
    private int score = 0 ;
    private string nick;
    private string Id;
    private int generalBronzeCoins;
    private int generalSilverCoins;
    private int generalGoldCoins;
    private int levelPlayer;
   
    private bool[] levelUnclocked = new bool[levelNumber];
    private float[] time = new float[levelNumber];
    private int[] controlSelected = new int[levelNumber];
    private int[] highestScore = new int[levelNumber];


    public static int levelNumber = 10;
    public bool keysCompleted;
    [SerializeField]
    private Image[] keysTab;
    [SerializeField]
    private Image[] livesTab;



    private float timer = 0;
    void Start()
    {
        handleSystem.setUp();
        SaveSystem.LoadPlayer();
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

    public void addBronzeCoin()
    {
        coinBronze++;
        coinsText.text = coinBronze.ToString();
    }
    public void addSilverCoin()
    {
        coinSilver++;

    }
    public void addGoldCoin()
    {
        coinGold ++;
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
        livesTab[lives].enabled = false;
        if (lives == 0) GameOver();
    }
    public void setData(int bronzeCoin, int silverCoin, int goldCoin, int levelPlayer, bool[] levelCompleted, float[] time, string nick,int[] controlSelected, int[] highestScore, string id)
    {
        Debug.Log("SetData");
        this.generalBronzeCoins = bronzeCoin+100;
        this.generalSilverCoins = silverCoin+100;
        this.generalGoldCoins = goldCoin+100;
        this.levelPlayer = levelPlayer;
        this.levelUnclocked = levelCompleted;
        this.time = time;
        this.nick = nick;
        this.controlSelected = controlSelected;
        this.highestScore = highestScore;
        this.Id = id;
}

    public int getGeneralBronzeCoins()
    {
        return generalBronzeCoins;
    }   
    public void setGenrealBronzeCoins(int setCoin)
    {
        generalBronzeCoins = setCoin;
    }
    public int getGeneralSilverCoins()
    {
        return generalSilverCoins;
    }
    public void setGenrealSilverCoins(int setCoin)
    {
        generalSilverCoins = setCoin;
    }
    public int getGeneralGoldCoins()
    {
        return generalGoldCoins;
    }
    public void setGenrealGoldCoins(int setCoin)
    {
        generalGoldCoins = setCoin;
    }
    public int getlevelPlayer()
    {
        return levelPlayer;
    }
    public bool[] getlevelUnclockedPlayer()
    {
        return levelUnclocked;
    }
    public float[] getTime()
    {
        return time;
    }
    public string getNick()
    { 
        return nick;
    }
    public void setNick(string nick)
    {
        this.nick = nick;
    }
    public string getId()
    {
        return Id;
    }
    public void setId(string id)
    {
        this.Id = id;
    }
    public int[] getControlerSelected()
    {
        return controlSelected;
    }
    public int[] getHighestScore()
    {
        return highestScore;
    }



    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        inGameCanvas.enabled = (currentGameState == GameState.GS_GAME);
        pauseCanvas.enabled = (currentGameState == GameState.GS_PAUSEMENU);
        levelCompletedCanvas.enabled = (currentGameState == GameState.GS_LEVELCOMPLETED);
        gameOverCanvas.enabled = (currentGameState == GameState.GS_GAME_OVER);
        if(currentGameState == GameState.GS_LEVELCOMPLETED)
        {
            Scene currentScene = SceneManager.GetActiveScene();
       
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
        Debug.Log("By³o bronze: " + generalBronzeCoins + " silver: " + generalSilverCoins + "gold: " + generalGoldCoins);
        generalBronzeCoins += coinBronze;
        generalSilverCoins += coinSilver;
        generalGoldCoins += coinGold;
        Debug.Log("Dodano: " + coinBronze + " " + coinSilver + " " + coinGold);
        Debug.Log("Jest bronze: " + generalBronzeCoins + " silver: " + generalSilverCoins + "gold: " + generalGoldCoins);
        score = coinBronze + coinSilver * 10 + coinGold * 100 + lives * 100 +(int) Mathf.Floor(100 - timer);
        if (levelPlayer < SelectLevel.selected) levelPlayer = SelectLevel.selected;
        if (highestScore[SelectLevel.selected] <= score)
        {
            Debug.Log("RECORD! Aktualnie zdobyte punkty: " + score + " poprzednio: " + highestScore[SelectLevel.selected]);
            time[SelectLevel.selected] = timer;
            controlSelected[SelectLevel.selected] = PlayerPrefs.GetInt("Control", 0);
            highestScore[SelectLevel.selected] = 123;
            highScoreText.text = "Highscore:" + score;
            ScoreText.text = "score:" + score;
            levelUnclocked[SelectLevel.selected] = true;
        }


        SaveSystem.SavePlayer(this);
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
