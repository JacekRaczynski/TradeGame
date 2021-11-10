using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public IEnumerator StartGame(string levelName)
    {
        yield return new WaitForSeconds(0.12f);
        SceneManager.LoadScene(levelName);
    }
    public void onLevelButton1Pressed()
    {
        StartCoroutine(StartGame("Level1"));
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
