using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas settingsCanvas;
    public Canvas controlCanvas;
    public TMP_Dropdown dropDown;
    private void Start()
    {
        settingsCanvas.enabled = false;
        controlCanvas.enabled = false;
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

    void Update()
    {
        
    }
}
