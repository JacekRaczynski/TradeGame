using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas settingsCanvas;
    public Canvas controlCanvas;
    public Canvas selectLevelCanvas;
    public Canvas highestScoreCanvas;
    public TMP_Dropdown dropDown;
    public Image imageControlArrows;
    public Image imageControlJoystick;
    public TMPro.TextMeshProUGUI textSelectedControl;
    [SerializeField]
    private TMPro.TextMeshProUGUI generalBronzeCoinsText;
    [SerializeField]
    private TMPro.TextMeshProUGUI generalSilverCoinsText;
    [SerializeField]
    private TMPro.TextMeshProUGUI generalGoldCoinsText;

    private void Awake()
    {
   

        load();
        selectControl();
        settingsCanvas.enabled = false;
        controlCanvas.enabled = false;
        highestScoreCanvas.enabled = false;
        selectLevelCanvas.enabled = false;
        Debug.Log("Loaded data from memory/aaaaaa/");// + highestScoreCanvas.enabled + controlCanvas.enabled + highestScoreCanvas.enabled);
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
                textSelectedControl.text = dropDown.options[0].text;
            }
            else if (dropDown.value == 1)
            {
                imageControlArrows.enabled = false;
                imageControlJoystick.enabled = true;
                textSelectedControl.text = dropDown.options[1].text;
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
    public void load()
    {
        SaveSystem.LoadPlayer();
        updateGeneralCoins();
    }
    public void reset()
    {
        SaveSystem.resetPlayer();
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
