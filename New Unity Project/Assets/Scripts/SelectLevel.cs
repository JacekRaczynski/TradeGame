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
    private Sprite[] images = new Sprite[16];
    [SerializeField]
    private string[] description = new string[16];
    public static int selected;
    public TradeManager tradeManager;
    public Button start;

    public Image star;
    public Image star1;
    public Image star2;

    public enum enumProvince
    {
        POMORSKIE = 0,
        WARMIÑSKO_MAZURSKIE = 1,
        PODLASKIE = 2,
        MAZOWIECKIE = 3,
        LUBELSKIE = 4,
        PODKARPACKIE = 5,
        ŒWIÊTOKRZYSKIE = 6,
        MA£OPOLSKIE = 7,
        £ÓDZKIE = 8,
        ŒL¥SKIE = 9,
        OPOLSKIE = 10,
        DOLNOŒL¥SKIE = 11,
        WIELKOPOLSKIE = 12,
        ZACHODNIOPOMORSKIE = 13,
        KUJAWSKO_POMORSKIE = 14,
        LUBUSKIE = 15,
    }
   
    public void Awake()
    {
        start.interactable = false;
        star.enabled = false;
        star1.enabled = false;
        star2.enabled = false;
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
        colorUnlocked.color = GameManager.instance.getlevelUnclockedPlayer()[index] ? Color.green : Color.black;
        selected = index;
        tradeManager.exchange();
        tradeManager.BtoS.interactable = GameManager.instance.getlevelUnclockedPlayer()[index];
        tradeManager.BtoG.interactable = GameManager.instance.getlevelUnclockedPlayer()[index];
        tradeManager.StoB.interactable = GameManager.instance.getlevelUnclockedPlayer()[index];
        tradeManager.StoG.interactable = GameManager.instance.getlevelUnclockedPlayer()[index];
        tradeManager.GtoB.interactable = GameManager.instance.getlevelUnclockedPlayer()[index];
        tradeManager.GtoS.interactable = GameManager.instance.getlevelUnclockedPlayer()[index];
        if (index < GameManager.instance.getlevelPlayer() + 2)
            start.interactable = true;
        else start.interactable = false;
        if (GameManager.instance.getHighestScore()[index] > 0)
            star.enabled = true;
        else star.enabled = false;
        if (GameManager.instance.getHighestScore1()[index] > 0)
            star1.enabled = true;
        else star1.enabled = false;
        if (GameManager.instance.getHighestScore2()[index] > 0)
            star2.enabled = true;
        else star2.enabled = false;
    }
    public IEnumerator StartGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
        yield return new WaitForSeconds(0.12f);
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
    private void Update()
    {
       // if(selected = 0)
    }
}


public class Province
{
    int id;
    string name;
    string describe;
    bool isUnlocked;
}