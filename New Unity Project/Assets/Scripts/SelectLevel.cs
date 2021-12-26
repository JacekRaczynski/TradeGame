using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class SelectLevel : MonoBehaviour
{
    [SerializeField]
    private Image showImage;
    [SerializeField]
    private TMPro.TextMeshProUGUI showDescription;
    [SerializeField]
    private TMPro.TextMeshProUGUI showID;
    [SerializeField]
    private TMPro.TextMeshProUGUI showTitle;
    [SerializeField]
    private Image colorUnlocked;
    [SerializeField]
    private Sprite[] images = new Sprite[GameManager.levelNumber];
    [SerializeField]
    private string[] description = new string[GameManager.levelNumber];
    public static int selected;
    public enum enumProvince
    {
        POMORSKIE = 0,
        WARMI�SKO_MAZURSKIE = 1,
        PODLASKIE = 2,
        MAZOWIECKIE = 3,
        LUBELSKIE = 4,
        PODKARPACKIE = 5,
        �WI�TOKRZYSKIE = 6,
        MA�OPOLSKIE = 7,
        ��DZKIE = 8,
        �L�SKIE = 9,
        OPOLSKIE = 10,
        DOLNO�L�SKIE = 11,
        WIELKOPOLSKIE = 12,
        ZACHODNIOPOMORSKIE = 13,
        KUJAWSKO_POMORSKIE = 14,
        LUBUSKIE = 15,
    }

    public void Select(int index)
    {
        var province = EventSystem.current.currentSelectedGameObject;

        showTitle.text = province.name;
        showID.text = index.ToString();
        showImage.sprite = images[index];
        Debug.Log(showDescription.text);
        showDescription.text = description[index];
        if(GameManager.instance != null )
        colorUnlocked.color = GameManager.instance.getlevelUnclockedPlayer()[index] ? Color.green : Color.green;
  
        selected = index;

    }
   
    public IEnumerator StartGame(string levelName)
    {
        yield return new WaitForSeconds(0.12f);
        SceneManager.LoadScene(levelName);
    }
    public void StartLevel()
    {
        switch (selected)
        {
            case 0:
                Debug.Log("Start Level1");
                StartCoroutine(StartGame("Level1"));
                break;
            case 1:
                Debug.Log("Start Level2");
                StartCoroutine(StartGame("Level2"));
                break;
            default:
                print("Incorrect number level.");
                break;
        }
    }
}

public class Province
{
    int id;
    string name;
    string describe;
    bool isUnlocked;
}