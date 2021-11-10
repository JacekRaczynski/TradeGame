using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private TMPro.TextMeshProUGUI coinsText;
    [SerializeField]
    private TMPro.TextMeshProUGUI timesText;
    private int coins = 0;
    private int lives = 3;
    private int keys = 0;
    [SerializeField]
    private Image[] keysTab;
    [SerializeField]
    private Image[] livesTab;

    private float timer = 0;

    void Start()
    {
        for (int i = keys; i < keysTab.Length; i++)
            keysTab[i].color = Color.grey;
        for (int i = 0; i < lives; i++)
            livesTab[i].enabled = true;
        for (int i = lives; i < livesTab.Length; i++)
            livesTab[i].enabled = false;
    }

    private void Awake()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.GS_PAUSEMENU)
        {
            if (Input.GetKey(KeyCode.S))
                InGame();
        }
        if (currentGameState == GameState.GS_GAME)
        {
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
    }
    public void subTractLives()
    {
        lives--;
    }
    
    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        inGameCanvas.enabled = (newGameState == GameState.GS_GAME);
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
}
