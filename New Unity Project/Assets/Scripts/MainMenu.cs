using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas registerCanvas;
    public Canvas settingsCanvas;
    public Canvas controlCanvas;
    public Canvas selectLevelCanvas;
    public Canvas highestScoreCanvas;
    public Canvas sureCanvas;
    public TMP_Dropdown dropDown;
    public Image imageControlArrows;
    public Image imageControlJoystick;
    public Image imageControlArea;
    public TMPro.TextMeshProUGUI textSelectedControl;
    [SerializeField]
    private TMPro.TextMeshProUGUI generalBronzeCoinsText;
    [SerializeField]
    private TMPro.TextMeshProUGUI generalSilverCoinsText;
    [SerializeField]
    private TMPro.TextMeshProUGUI generalGoldCoinsText;
    public TMPro.TextMeshProUGUI nick;
    public TMPro.TextMeshProUGUI lvl;
    public TMP_InputField nickInput;


    private void Awake()
    {
        if (GameManager.instance == null)
            GameManager.instance = new GameManager();

        load();
        selectControl();
        if (GameManager.instance.getId() == "" || GameManager.instance.getId() == null)
        {
            registerCanvas.enabled = true;
            mainMenuCanvas.enabled = false;
        }
        else
        {
            nick.text = GameManager.instance.getNick();
            lvl.text = GameManager.instance.getlevelPlayer().ToString();

            registerCanvas.enabled = false;
        }
        settingsCanvas.enabled = false;
        controlCanvas.enabled = false;
        highestScoreCanvas.enabled = false;
        selectLevelCanvas.enabled = false;
        sureCanvas.enabled = false;
        Debug.Log("Loaded data from memory/aaaaaa/");// + highestScoreCanvas.enabled + controlCanvas.enabled + highestScoreCanvas.enabled);
        for (int i = 0; i < GameManager.instance.getlevelUnclockedPlayer().Length; i++)
            Debug.Log(i + " " + GameManager.instance.getlevelUnclockedPlayer()[i]);
    }


    private void Start()
    {

        dropDown.onValueChanged.AddListener(delegate { selectControl(); });
    }
    public IEnumerator StartGame(string levelName)
    {
        yield return new WaitForSeconds(0.12f);
        SceneManager.LoadScene(levelName);
    }
    public void onLevelButton1Pressed()
    {
        StartCoroutine(StartGame("Level1"));
    }
    public void showSettings()
    {
        mainMenuCanvas.enabled = settingsCanvas.isActiveAndEnabled;
        settingsCanvas.enabled = !settingsCanvas.isActiveAndEnabled; 
    }
    public void showSure()
    {
        sureCanvas.enabled = !sureCanvas.isActiveAndEnabled;
    }
    public void showControl()
    {
        mainMenuCanvas.enabled = controlCanvas.isActiveAndEnabled;
        controlCanvas.enabled = !controlCanvas.isActiveAndEnabled; 
    }
    public void showHighestScore()
    {
        mainMenuCanvas.enabled = highestScoreCanvas.isActiveAndEnabled;
        highestScoreCanvas.enabled = !highestScoreCanvas.isActiveAndEnabled;

    }
    public void showSelectLevel()
    {
        updateGeneralCoins();
        mainMenuCanvas.enabled = selectLevelCanvas.isActiveAndEnabled;
        selectLevelCanvas.enabled = !selectLevelCanvas.isActiveAndEnabled;
       
    }

    public void selectControl()
    {
        PlayerPrefs.SetInt("Control", dropDown.value);

             if (dropDown.value == 0)
             {
                imageControlArrows.enabled = true;
                imageControlJoystick.enabled = false;
                imageControlArea.enabled = false;
                textSelectedControl.text = dropDown.options[0].text;
            }
            else if (dropDown.value == 1)
            {
            imageControlArrows.enabled = false;
                imageControlArea.enabled = false;
                imageControlJoystick.enabled = true;
                textSelectedControl.text = dropDown.options[1].text;
            }
            else if (dropDown.value == 2)
            {
            imageControlArrows.enabled = false;
                imageControlJoystick.enabled = false;
                imageControlArea.enabled = true;
            textSelectedControl.text = dropDown.options[2].text;
            }
    }
    public void updateGeneralCoins()
    {
        
            generalBronzeCoinsText.text = GameManager.instance.getGeneralBronzeCoins().ToString();
            generalSilverCoinsText.text = GameManager.instance.getGeneralSilverCoins().ToString();
            generalGoldCoinsText.text = GameManager.instance.getGeneralGoldCoins().ToString();
     }
    

    public void onLevelButton2Pressed()
    {
        StartCoroutine(StartGame("Level2"));
    }
    public void register()
    {
        if (nickInput.text != "")
        {
            registerCanvas.enabled = false;
            mainMenuCanvas.enabled = true;
            GameManager.instance.setNick(nickInput.text);
            Debug.Log(Random.Range(0, 10000000));
            GameManager.instance.setId(Random.Range(0, 10000000).ToString());
            nick.text = GameManager.instance.getNick();
            SaveSystem.SavePlayer(GameManager.instance);
        }
    }
    public void load()
    {
        SaveSystem.LoadPlayer();
        updateGeneralCoins();
    }
    public void reset()
    {
        SaveSystem.resetPlayer();
        updateGeneralCoins();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
    public void OnEnable()
    {
        updateGeneralCoins();
    }
    public void OnExit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif

        Application.Quit();
    }


}
