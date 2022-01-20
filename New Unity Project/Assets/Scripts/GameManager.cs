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
    private Button NextLevel;
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
    private float[] time1 = new float[levelNumber];
    private int[] controlSelected1 = new int[levelNumber];
    private int[] highestScore1 = new int[levelNumber];
    private float[] time2 = new float[levelNumber];
    private int[] controlSelected2 = new int[levelNumber];
    private int[] highestScore2 = new int[levelNumber];

    public Image star;
    public Image star1;
    public Image star2;

    public static int levelNumber = 10;
    public bool keysCompleted;
    [SerializeField]
    private Image[] keysTab;
    [SerializeField]
    private Image[] livesTab;
    public Rest rest;


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
            GameObject.Find("ControlArea").active = false;
        } else if (PlayerPrefs.GetInt("Control", 0) == 1)
        {
            GameObject.Find("ControlArrows").active = false;
            GameObject.Find("ControlJoystick").active = true;
            GameObject.Find("ControlArea").active = false;
        }
        else if (PlayerPrefs.GetInt("Control", 0) == 2)
        {
            GameObject.Find("ControlArrows").active = false;
            GameObject.Find("ControlJoystick").active = false;
            GameObject.Find("ControlArea").active = true;
        }
    }
        public GameManager()
    { 
       int generalBronzeCoins = 0;
       int generalSilverCoins = 0;
       int generalGoldCoins = 0;

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
    public void setData(int bronzeCoin, int silverCoin, int goldCoin, int levelPlayer, bool[] levelCompleted, float[] time, string nick,int[] controlSelected, int[] highestScore, string id
        , float[] time1, int[] controlSelected1, int[] highestScore1, float[] time2, int[] controlSelected2, int[] highestScore2)
    {
        Debug.Log("SetData");
        this.generalBronzeCoins = bronzeCoin+100;
        this.generalSilverCoins = silverCoin+100;
        this.generalGoldCoins = goldCoin+100;
        this.levelPlayer = levelPlayer;
        this.levelUnclocked = levelCompleted;
     
        this.nick = nick;
        this.time = time;
        this.controlSelected = controlSelected;
        this.highestScore = highestScore;
        this.time1 = time1;
        this.controlSelected1 = controlSelected1;
        this.highestScore1 = highestScore1;
        this.time2 = time2;
        this.controlSelected2 = controlSelected2;
        this.highestScore2 = highestScore2;
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
    public float[] getTime1()
    {
        return time1;
    }
    public float[] getTime2()
    {
        return time2;
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
    public int[] getControlerSelected1()
    {
        return controlSelected1;
    }
    public int[] getControlerSelected2()
    {
        return controlSelected2;
    }
    public int[] getHighestScore()
    {
        return highestScore;
    }
    public int[] getHighestScore1()
    {
        return highestScore1;
    }
    public int[] getHighestScore2()
    {
        return highestScore2;
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
        if (currentGameState == GameState.GS_PAUSEMENU || currentGameState == GameState.GS_GAME_OVER)
        {
            Time.timeScale = 0f;
        }
        else Time.timeScale = 1f;
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

    

      switch (PlayerPrefs.GetInt("Control", 0))
        {
            case 0:
                if (highestScore[SelectLevel.selected] ==null || highestScore[SelectLevel.selected] <= score)
                {
                    Debug.Log("RECORD! Aktualnie zdobyte punkty: " + score + " poprzednio: " + highestScore[SelectLevel.selected]);
                    time[SelectLevel.selected] = timer;
                    controlSelected[SelectLevel.selected] = PlayerPrefs.GetInt("Control", 0);
                    highestScore[SelectLevel.selected] = score;
               
                }
                ScoreText.text = "score:" + score;
                highScoreText.text = "Highscore:" + highestScore[SelectLevel.selected];
                break;
            case 1:
                if (highestScore1[SelectLevel.selected] == null || highestScore1[SelectLevel.selected] <= score)
                {
                    Debug.Log("RECORD! Aktualnie zdobyte punkty: " + score + " poprzednio: " + highestScore1[SelectLevel.selected]);
                    time1[SelectLevel.selected] = timer;
                    controlSelected1[SelectLevel.selected] = PlayerPrefs.GetInt("Control", 0);
                    highestScore1[SelectLevel.selected] = score;
                    highScoreText.text = "Highscore:" + score;
                }
                ScoreText.text = "score:" + score;
                highScoreText.text = "Highscore:" + highestScore1[SelectLevel.selected];
                break;
            case 2:
                if (highestScore2[SelectLevel.selected] == null || highestScore[SelectLevel.selected] <= score)
                {
                    Debug.Log("RECORD! Aktualnie zdobyte punkty: " + score + " poprzednio: " + highestScore2[SelectLevel.selected]);
                    time2[SelectLevel.selected] = timer;
                    controlSelected2[SelectLevel.selected] = PlayerPrefs.GetInt("Control", 0);
                    highestScore2[SelectLevel.selected] = score;
                    highScoreText.text = "Highscore:" + score;
                }
                ScoreText.text = "score:" + score;
                highScoreText.text = "Highscore:" + highestScore2[SelectLevel.selected];
                break;

        }
        if (highestScore[SelectLevel.selected] > 0) star.enabled = true;
        else star.enabled = false;
        if (highestScore1[SelectLevel.selected] > 0) star1.enabled = true;
        else star1.enabled = false;
        if (highestScore2[SelectLevel.selected] > 0) star2.enabled = true;
        else star2.enabled = false;

        if (highestScore[SelectLevel.selected] > 0 &&
              highestScore1[SelectLevel.selected] > 0 &&
                 highestScore2[SelectLevel.selected] > 0 && 
                     levelUnclocked[SelectLevel.selected] == false)
        {
            levelUnclocked[SelectLevel.selected] = true;
            rest.request(Id, controlSelected[SelectLevel.selected].ToString(), SelectLevel.selected.ToString(), time[SelectLevel.selected], highestScore[SelectLevel.selected].ToString(),
                controlSelected1[SelectLevel.selected].ToString(), SelectLevel.selected.ToString(), time1[SelectLevel.selected].ToString(), highestScore1[SelectLevel.selected].ToString(),
                controlSelected2[SelectLevel.selected].ToString(), SelectLevel.selected.ToString(), time2[SelectLevel.selected].ToString(), highestScore2[SelectLevel.selected].ToString());
            Debug.Log("~``````````` " + time[SelectLevel.selected]);
            if (levelPlayer < SelectLevel.selected) levelPlayer = SelectLevel.selected;
        }
        if (levelUnclocked[SelectLevel.selected])
            NextLevel.interactable = true;


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
