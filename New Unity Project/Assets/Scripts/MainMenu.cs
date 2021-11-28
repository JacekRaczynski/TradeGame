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
    public TMP_Dropdown dropDown;
    public Image imageControlArrows;
    public Image imageControlJoystick;
    public TMPro.TextMeshProUGUI textSelectedControl;

    private void Start()
    {
        selectControl();
        settingsCanvas.enabled = false;
        controlCanvas.enabled = false;
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

    public void onLevelButton2Pressed()
    {
        StartCoroutine(StartGame("Level2"));
    }
    public void OnExit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif

        Application.Quit();
    }


}
